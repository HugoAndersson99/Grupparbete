using FakeItEasy;
using Application.Queries.Cvs.GetById;
using Domain.Models;
using Application.Interfaces.RepositoryInterfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Application.Dtos;
using NUnit.Framework;

namespace TestProject.CvsTests.CvsQueryTests
{
    [TestFixture] 
    public class GetCvByIdQueryHandlerTests
    {
        private GetCvByIdQueryHandler _handler;
        private ICvRepository _mockCvRepository;
        private ILogger<GetCvByIdQueryHandler> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockCvRepository = A.Fake<ICvRepository>();
            _mockLogger = A.Fake<ILogger<GetCvByIdQueryHandler>>();
            _handler = new GetCvByIdQueryHandler(_mockCvRepository, _mockLogger );
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenCvExistsInRepository()
        {
            // Arrange
            var queryId = Guid.NewGuid();
            var query = new GetCvByIdQuery(queryId);
            var cv = new CV { Id = queryId, Title = "Test CV", PdfUrl = "http://example.com/cv.pdf" };
            var mockResult = OperationResult<CV>.Success(cv);
       
            A.CallTo(() => _mockCvRepository.GetByIdAsync(query.Id)).Returns(Task.FromResult(mockResult));
       
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
       
            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Test CV", result.Data.Title);
        }
       
        
       
        [Test]
        public async Task Handle_ShouldReturnFailure_WhenCvDoesNotExistInRepository()
        {
            // Arrange
            var queryId = Guid.NewGuid();
            var query = new GetCvByIdQuery(queryId);
            var mockResult = OperationResult<CV>.Failure("CV not found.", "No CV with this ID.");

            A.CallTo(() => _mockCvRepository.GetByIdAsync(query.Id)).Returns(Task.FromResult(mockResult));
       
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
       
            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("CV not found.", result.ErrorMessage);
        }
    }
    
}

