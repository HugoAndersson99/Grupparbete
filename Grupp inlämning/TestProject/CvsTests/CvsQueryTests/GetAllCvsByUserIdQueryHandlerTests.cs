using Application.Interfaces.RepositoryInterfaces;
using Application.Queries.Cvs.GetAllByUserId;
using Domain.Models;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace TestProject.CvsTests.CvsQueryTests
{
    [TestFixture]
    public class GetAllCVsByUserIdQueryHandlerTests
    {
        private GetAllCVsByUserIdQueryHandler _handler;
        private ICvRepository _mockCvRepository;
        private ILogger<GetAllCVsByUserIdQueryHandler> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockCvRepository = A.Fake<ICvRepository>();
            _mockLogger = A.Fake<ILogger<GetAllCVsByUserIdQueryHandler>>();
            _handler = new GetAllCVsByUserIdQueryHandler(_mockCvRepository, _mockLogger);
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

            var mockResult = OperationResult<IEnumerable<CV>>.Failure("Cvs not found.", "Database error.");

            A.CallTo(() => _mockCvRepository.GetAllByUserIdAsync(query.UserId))
                .Returns(Task.FromResult(mockResult));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Cvs not found.", result.ErrorMessage);

            A.CallTo(() => _mockCvRepository.GetAllByUserIdAsync(query.UserId))
                .MustHaveHappenedOnceExactly();
        }
    }
}

