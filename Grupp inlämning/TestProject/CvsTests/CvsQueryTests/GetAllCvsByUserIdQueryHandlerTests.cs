using Application.Interfaces.RepositoryInterfaces;
using Application.Queries.Cvs.GetAllByUserId;
using Application.Queries.Cvs.GetById;
using Domain.Models;
using FakeItEasy;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;


namespace TestProject.CvsTests.CvsQueryTests
{
    [TestFixture] 
    public class GetAllCVsByUserIdQueryHandlerTests
    {
        private GetAllCVsByUserIdQueryHandler _handler;
        private ICvRepository _mockCvRepository;
        private IMemoryCache _mockCache;
        private ILogger<GetAllCVsByUserIdQueryHandler> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockCvRepository = A.Fake<ICvRepository>();
           // _mockCache = A.Fake<IMemoryCache>();
            _mockLogger = A.Fake<ILogger<GetAllCVsByUserIdQueryHandler>>();
            _handler = new GetAllCVsByUserIdQueryHandler(_mockCvRepository, _mockLogger);
        }

        [TearDown]
        public void TearDown()
        {
            _mockCache?.Dispose(); 
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenCVsExistInRepository()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var query = new GetAllCVsByUserIdQuery(userId);
            var cvs = new List<CV>
            {
                new CV { Id = Guid.NewGuid(), Title = "CV 1" },
                new CV { Id = Guid.NewGuid(), Title = "CV 2" }
            };

            var mockResult = OperationResult<IEnumerable<CV>>.Success(cvs);

            object cacheEntry;
            A.CallTo(() => _mockCache.TryGetValue(A<object>.That.IsEqualTo($"CVs_User_{query.UserId}"), out cacheEntry))
                .Returns(false);

            A.CallTo(() => _mockCvRepository.GetAllByUserIdAsync(query.UserId))
                .Returns(Task.FromResult(mockResult));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result.Data.Count());
            Assert.AreEqual("CV 1", result.Data.First().Title);
            
            A.CallTo(() => _mockCvRepository.GetAllByUserIdAsync(query.UserId))
                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenCVsNotFoundInRepository()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var query = new GetAllCVsByUserIdQuery(userId);

            var mockResult = OperationResult<IEnumerable<CV>>.Failure("No CVs found for this user.", "Database error.");

            object cacheEntry;
            A.CallTo(() => _mockCache.TryGetValue(A<object>.That.IsEqualTo($"CVs_User_{query.UserId}"), out cacheEntry))
                .Returns(false);

            A.CallTo(() => _mockCvRepository.GetAllByUserIdAsync(query.UserId))
                .Returns(Task.FromResult(mockResult));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("No CVs found for this user.", result.ErrorMessage);

            A.CallTo(() => _mockCvRepository.GetAllByUserIdAsync(query.UserId))
                .MustHaveHappenedOnceExactly();
        }
    }
}
