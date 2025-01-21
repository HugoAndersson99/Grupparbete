using Application.Interfaces.RepositoryInterfaces;
using Application.Queries.Users.Login.Helpers;
using Application.Queries.Users.Login;
using Domain.Models;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace TestProject.UsersTests.LogInUserTests
{
    [TestFixture]
    public class LoginUserQueryHandlerTests
    {
        private IUserRepository _fakeUserRepository;
        private ILogger<LoginUserQueryHandler> _fakeLogger;
        private TokenHelper _fakeTokenHelper;
        private LoginUserQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _fakeUserRepository = A.Fake<IUserRepository>();
            _fakeLogger = A.Fake<ILogger<LoginUserQueryHandler>>();
            _fakeTokenHelper = A.Fake<TokenHelper>();
            _handler = new LoginUserQueryHandler(_fakeUserRepository, _fakeLogger, _fakeTokenHelper);
        }

        [Test]
        public async Task Handle_ShouldReturnToken_WhenLoginIsSuccessful()
        {
            // Arrange
            var user = new User { Email = "test@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password") };
            var query = new LoginUserQuery(new UserDto { Email = "test@example.com", PasswordHash = "password" });

            A.CallTo(() => _fakeUserRepository.GetUserByEmailAsync("test@example.com"))
                .Returns(OperationResult<User>.Success(user));
            A.CallTo(() => _fakeTokenHelper.GenerateJwtToken(user))
                .Returns("mockToken");

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("mockToken", result.Data);

            // Verify GenerateJwtToken was called with correct user
            A.CallTo(() => _fakeTokenHelper.GenerateJwtToken(user)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenEmailIsInvalid()
        {
            // Arrange
            var query = new LoginUserQuery(new UserDto { Email = "invalid@example.com", PasswordHash = "password" });

            A.CallTo(() => _fakeUserRepository.GetUserByEmailAsync("invalid@example.com"))
                .Returns(OperationResult<User>.Failure("User not found"));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Invalid email or password", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenPasswordIsInvalid()
        {
            // Arrange
            var user = new User { Email = "test@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("correctpassword") };
            var query = new LoginUserQuery(new UserDto { Email = "test@example.com", PasswordHash = "wrongpassword" });

            A.CallTo(() => _fakeUserRepository.GetUserByEmailAsync("test@example.com"))
                .Returns(OperationResult<User>.Success(user));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Invalid password.", result.ErrorMessage);
        }
    }
}

