

using Application.Commands.Users.Add;
using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace TestProject.UsersTests.UsersCommandTests
{
    [TestFixture]
    public class AddNewUserCommandHandlerTests
    {
        private IUserRepository _userRepository;
        private ILogger<AddNewUserCommandHandler> _logger;
        private AddNewUserCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _userRepository = A.Fake<IUserRepository>();
            _logger = A.Fake<ILogger<AddNewUserCommandHandler>>();

            _handler = new AddNewUserCommandHandler(_userRepository, _logger);
        }

        [Test]
        public async Task Handle_ShouldAddUserSuccessfully_WhenRepositoryReturnsSuccess()
        {
            // Arrange
            var newUserDto = new UserDto
            {
                Email = "test@example.com",
                PasswordHash = "TestPassword123"
            };
            var command = new AddNewUserCommand(newUserDto);

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = newUserDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(newUserDto.PasswordHash)
            };

            A.CallTo(() => _userRepository.AddUserAsync(A<User>.That.Matches(u => u.Email == newUser.Email)))
                .Returns(Task.FromResult(OperationResult<User>.Success(newUser, "User added successfully.")));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("test@example.com", result.Data.Email);
            A.CallTo(() => _userRepository.AddUserAsync(A<User>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenUserAlreadyExists()
        {
            // Arrange
            var newUserDto = new UserDto
            {
                Email = "duplicate@example.com",
                PasswordHash = "TestPassword123"
            };
            var command = new AddNewUserCommand(newUserDto);

            A.CallTo(() => _userRepository.AddUserAsync(A<User>.Ignored))
                .Returns(Task.FromResult(OperationResult<User>.Failure("A user with this Email already exists.", "Duplicate error.")));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("A user with this Email already exists.", result.ErrorMessage);
            A.CallTo(() => _userRepository.AddUserAsync(A<User>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenRepositoryThrowsException()
        {
            // Arrange
            var newUserDto = new UserDto
            {
                Email = "error@example.com",
                PasswordHash = "TestPassword123"
            };
            var command = new AddNewUserCommand(newUserDto);

            A.CallTo(() => _userRepository.AddUserAsync(A<User>.Ignored))
                .Throws(new Exception("Database connection failed."));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("An error occurred: Database connection failed.", result.ErrorMessage);
            A.CallTo(() => _userRepository.AddUserAsync(A<User>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Handle_ShouldHashPasswordCorrectly_WhenAddingUser()
        {
            // Arrange
            var newUserDto = new UserDto
            {
                Email = "hashcheck@example.com",
                PasswordHash = "PlainTextPassword"
            };
            var command = new AddNewUserCommand(newUserDto);

            A.CallTo(() => _userRepository.AddUserAsync(A<User>.Ignored))
                .ReturnsLazily((User user) =>
                {
                    Assert.IsTrue(BCrypt.Net.BCrypt.Verify("PlainTextPassword", user.PasswordHash));
                    return Task.FromResult(OperationResult<User>.Success(user, "User added successfully."));
                });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("hashcheck@example.com", result.Data.Email);
            A.CallTo(() => _userRepository.AddUserAsync(A<User>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}
