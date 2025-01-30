using Application.Interfaces.RepositoryInterfaces;
using Application.Queries.Users.Get.GetAll;
using Domain.Models;
using Infrastructure.Repositories;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;

namespace TestProject.UsersTests.UsersQueryTests
{
    [TestFixture]
    public class GetAllUsersQueryHandlerIntegrationTests
    {
        private GetAllUsersQueryHandler _handler;
        private IUserRepository _userRepository;
        private Database _dbContext;
        private ILogger<GetAllUsersQueryHandler> _logger;
       // private IMemoryCache _cache;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Database>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new Database(options);
            _userRepository = new UserRepository(_dbContext, NullLogger<UserRepository>.Instance);
            _logger = new LoggerFactory().CreateLogger<GetAllUsersQueryHandler>();

            _handler = new GetAllUsersQueryHandler(_userRepository, _logger);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenUsersExistInDatabase()
        {
            // Arrange
            var user1 = new User { Id = Guid.NewGuid(), Email = "john.doe@example.com", PasswordHash = "12345" };
            var user2 = new User { Id = Guid.NewGuid(), Email = "jane.smith@example.com", PasswordHash = "123456" };

            _dbContext.Users.Add(user1);
            _dbContext.Users.Add(user2);
            await _dbContext.SaveChangesAsync();

            var query = new GetAllUsersQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result.Data.Count);
            Assert.AreEqual("john.doe@example.com", result.Data[0].Email);
            Assert.AreEqual("jane.smith@example.com", result.Data[1].Email);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenNoUsersExist()
        {
            // Arrange
            var query = new GetAllUsersQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("No users found or error occurred.", result.ErrorMessage);
        }
    }
}
