using Application.Interfaces.RepositoryInterfaces;
using Application.Queries.Users.Get.GetById;
using Domain.Models;
using Infrastructure.Databases;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace TestProject.UsersTests.UsersQueryTests
{
    [TestFixture]
    public class GetUserByIdQueryHandlerIntegrationTests
    {
        private GetUserByIdQueryHandler _handler;
        private IUserRepository _userRepository;
        private Database _dbContext;
        private ILogger<GetUserByIdQueryHandler> _logger;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Database>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new Database(options);
            _userRepository = new UserRepository(_dbContext, NullLogger<UserRepository>.Instance);
            _logger = new LoggerFactory().CreateLogger<GetUserByIdQueryHandler>();

            _handler = new GetUserByIdQueryHandler(_userRepository, _logger);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenUserExistsInDatabase()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Email = "jane.smith@example.com", PasswordHash = "12345" };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            var query = new GetUserByIdQuery(userId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(userId, result.Data.Id);
            Assert.AreEqual("12345", result.Data.PasswordHash);
            Assert.AreEqual("jane.smith@example.com", result.Data.Email);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenUserDoesNotExist()
        {
            // Arrange
            var query = new GetUserByIdQuery(Guid.NewGuid());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("User not found.", result.ErrorMessage);
        }
    }
}
