using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Application.Queries.Users.Login.Helpers;
using Application.Queries.Users.Login;
using Domain.Models;
using Infrastructure.Repositories;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Configuration;

namespace TestProject.UsersTests.LogInUserTests
{
    [TestFixture]
    public class LoginUserQueryHandlerIntegrationTests
    {
            private LoginUserQueryHandler _handler;
            private IUserRepository _userRepository;
            private Database _dbContext;
            private ILogger<LoginUserQueryHandler> _logger;
            private TokenHelper _tokenHelper;

            [SetUp]
            public void Setup()
            { 
                var options = new DbContextOptionsBuilder<Database>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                    .Options;

                _dbContext = new Database(options); 

                _userRepository = new UserRepository(_dbContext, NullLogger<UserRepository>.Instance);
                _logger = new LoggerFactory().CreateLogger<LoginUserQueryHandler>();

                var configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(new Dictionary<string, string>
                    {
                    { "JwtSettings:SecretKey", "your-very-long-secret-key-12345678" }
                    })
                    .Build();

                _tokenHelper = new TokenHelper(configuration);

                _handler = new LoginUserQueryHandler(_userRepository, _logger, _tokenHelper);

                SeedDatabase();
            }

            private void SeedDatabase()
            {
                _dbContext.Users.Add(new User
                {
                    Email = "test@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password")
                });
                _dbContext.SaveChanges();
            }

            [TearDown]
            public void TearDown()
            {
                _dbContext.Dispose();
            }

            [Test]
            public async Task Handle_ShouldReturnToken_WhenLoginIsSuccessful()
            {
                // Arrange
                var query = new LoginUserQuery(new UserDto { Email = "test@example.com", PasswordHash = "password" });

                // Act
                var result = await _handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.IsTrue(result.IsSuccess);
                Assert.IsNotNull(result.Data);
                Assert.IsNotEmpty(result.Data);
            }

            [Test]
        public async Task Handle_ShouldReturnFailure_WhenEmailIsInvalid()
        {
            // Arrange
            var query = new LoginUserQuery(new UserDto { Email = "invalid@example.com", PasswordHash = "password" });

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
            var query = new LoginUserQuery(new UserDto { Email = "test@example.com", PasswordHash = "wrongpassword" });

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Invalid password.", result.ErrorMessage);
        }

    }
}
