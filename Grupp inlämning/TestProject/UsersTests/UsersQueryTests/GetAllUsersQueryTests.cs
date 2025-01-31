using Application.Interfaces.RepositoryInterfaces;
using Application.Queries.Users.Get.GetAll;
using Domain.Models;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace TestProject.UsersTests.UsersQueryTests
{
    [TestFixture]
    public class GetAllUsersQueryHandlerTests
    {
        private GetAllUsersQueryHandler _handler;
        private IUserRepository _mockUserRepository;
        private ILogger<GetAllUsersQueryHandler> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = A.Fake<IUserRepository>();
            _mockLogger = A.Fake<ILogger<GetAllUsersQueryHandler>>();
            _handler = new GetAllUsersQueryHandler(_mockUserRepository, _mockLogger);
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenUsersExistInRepository()
        {
            // Arrange
            var query = new GetAllUsersQuery();
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Email = "User 1" },
                new User { Id = Guid.NewGuid(), Email = "User 2" }
            };
    

            var mockResult = OperationResult<List<User>>.Success(users);

            A.CallTo(() => _mockUserRepository.GetAllUsersAsync())
                .Returns(Task.FromResult(mockResult));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result.Data.Count);
            Assert.AreEqual("User 1", result.Data[0].Email);

            A.CallTo(() => _mockUserRepository.GetAllUsersAsync())
                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenNoUsersFoundInRepository()
        {
            // Arrange
            A.CallTo(() => _mockUserRepository.GetAllUsersAsync())
                .Returns(OperationResult<List<User>>.Failure("No users found or error occurred."));

            // Act
            var result = await _handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("No users found or error occurred."));
        }
    }
}
