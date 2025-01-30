using Application.Interfaces.RepositoryInterfaces;
using Application.Queries.Users.Get.GetById;
using Domain.Models;
using FakeItEasy;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace TestProject.UsersTests.UsersQueryTests
{
    [TestFixture] 
    public class GetUserByIdQueryHandlerTests
    {
        private GetUserByIdQueryHandler _handler;
        private IUserRepository _mockUserRepository;
       // private IMemoryCache _mockCache;
        private ILogger<GetUserByIdQueryHandler> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = A.Fake<IUserRepository>();
           // _mockCache = A.Fake<IMemoryCache>();
            _mockLogger = A.Fake<ILogger<GetUserByIdQueryHandler>>();
            _handler = new GetUserByIdQueryHandler(_mockUserRepository, _mockLogger);
        }

        [TearDown]
        public void TearDown()
        {
            //_mockCache?.Dispose(); 
        }

       // [Test]
       // public async Task Handle_ShouldReturnSuccess_WhenUserExistsInRepository()
       // {
       //     // Arrange
       //     var query = new GetUserByIdQuery(Guid.NewGuid());
       //     var user = new User { Id = query.Id, Email = "Test User" };
       //
       //     var mockResult = OperationResult<User>.Success(user);
       //
       //     object cacheEntry;
       //     A.CallTo(() => _mockCache.TryGetValue(A<object>.That.IsEqualTo($"User_{query.Id}"), out cacheEntry))
       //         .Returns(false);
       //
       //     A.CallTo(() => _mockUserRepository.GetUserByIdAsync(query.Id))
       //         .Returns(Task.FromResult(mockResult));
       //
       //     // Act
       //     var result = await _handler.Handle(query, CancellationToken.None);
       //
       //     // Assert
       //     Assert.IsTrue(result.IsSuccess);
       //     Assert.AreEqual("Test User", result.Data.Email);
       //
       //     A.CallTo(() => _mockUserRepository.GetUserByIdAsync(query.Id))
       //         .MustHaveHappenedOnceExactly();
       // }
       //
       // [Test]
       // public async Task Handle_ShouldReturnFailure_WhenUserNotFoundInRepository()
       // {
       //     // Arrange
       //     var query = new GetUserByIdQuery(Guid.NewGuid());
       //     var mockResult = OperationResult<User>.Failure("User not found.", "No user with this ID.");
       //
       //     object cacheEntry;
       //     A.CallTo(() => _mockCache.TryGetValue(A<object>.That.IsEqualTo($"User_{query.Id}"), out cacheEntry))
       //         .Returns(false); 
       //
       //     A.CallTo(() => _mockUserRepository.GetUserByIdAsync(query.Id))
       //         .Returns(Task.FromResult(mockResult)); 
       //
       //     // Act
       //     var result = await _handler.Handle(query, CancellationToken.None);
       //
       //     // Assert
       //     Assert.IsFalse(result.IsSuccess);
       //     Assert.AreEqual("User not found.", result.ErrorMessage);
       //
       //     A.CallTo(() => _mockUserRepository.GetUserByIdAsync(query.Id))
       //         .MustHaveHappenedOnceExactly();
       // }
       //
       // [Test]
       // public async Task Handle_ShouldReturnFromCache_WhenUserIsInCache()
       // {
       //     // Arrange
       //     var query = new GetUserByIdQuery(Guid.NewGuid());
       //     var cachedUser = new User { Id = query.Id, Email = "Cached User" };
       //
       //     object cacheEntry = cachedUser;
       //     A.CallTo(() => _mockCache.TryGetValue(A<object>.That.IsEqualTo($"User_{query.Id}"), out cacheEntry))
       //         .Returns(true); 
       //
       //     // Act
       //     var result = await _handler.Handle(query, CancellationToken.None);
       //
       //     // Assert
       //     Assert.IsTrue(result.IsSuccess);
       //     Assert.AreEqual("Cached User", result.Data.Email);
       //
       //     A.CallTo(() => _mockUserRepository.GetUserByIdAsync(query.Id))
       //         .MustNotHaveHappened();
       // }
    }
}
