using Application.Interfaces.RepositoryInterfaces;
using Application.Queries.Cvs.GetAllByUserId;
using Domain.Models;
using Infrastructure.Databases;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace TestProject.CvsTests.CvsQueryTests
{
    [TestFixture]
    public class GetAllCVsByUserIdQueryHandlerIntegrationTests
    {
        private GetAllCVsByUserIdQueryHandler _handler;
        private ICvRepository _cvRepository;
        private Database _dbContext;
        private ILogger<GetAllCVsByUserIdQueryHandler> _logger;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Database>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new Database(options);
            _cvRepository = new CvRepository(_dbContext, NullLogger<CvRepository>.Instance);
            _logger = new LoggerFactory().CreateLogger<GetAllCVsByUserIdQueryHandler>();
            _handler = new GetAllCVsByUserIdQueryHandler(_cvRepository, _logger);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public async Task Handle_ShouldReturnCVs_WhenExistingInDatabase()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var cv1 = new CV { Id = Guid.NewGuid(), UserId = userId, Title = "CV 1", PdfUrl = "http://example.com/cv1.pdf" };
            var cv2 = new CV { Id = Guid.NewGuid(), UserId = userId, Title = "CV 2", PdfUrl = "http://example.com/cv2.pdf" };
            _dbContext.CVs.AddRange(cv1, cv2);
            await _dbContext.SaveChangesAsync();

            var query = new GetAllCVsByUserIdQuery(userId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result.Data.Count());
            Assert.Contains(cv1, result.Data.ToList());
            Assert.Contains(cv2, result.Data.ToList());
        }

        [Test]
        public async Task Handle_ShouldReturnCVsFromDatabaseOnSubsequentRequest()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var cv1 = new CV { Id = Guid.NewGuid(), UserId = userId, Title = "CV 1", PdfUrl = "http://example.com/cv1.pdf" };
            var cv2 = new CV { Id = Guid.NewGuid(), UserId = userId, Title = "CV 2", PdfUrl = "http://example.com/cv2.pdf" };
            _dbContext.CVs.AddRange(cv1, cv2);
            await _dbContext.SaveChangesAsync();

            var query = new GetAllCVsByUserIdQuery(userId);

            // Act
            var resultFirstRequest = await _handler.Handle(query, CancellationToken.None);
            var resultSecondRequest = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(resultFirstRequest.IsSuccess);
            Assert.AreEqual(2, resultFirstRequest.Data.Count());
            Assert.Contains(cv1, resultFirstRequest.Data.ToList());
            Assert.Contains(cv2, resultFirstRequest.Data.ToList());

            Assert.IsTrue(resultSecondRequest.IsSuccess);
            Assert.AreEqual(2, resultSecondRequest.Data.Count());
            Assert.Contains(cv1, resultSecondRequest.Data.ToList());
            Assert.Contains(cv2, resultSecondRequest.Data.ToList());
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyList_WhenNoCVsExistForUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var query = new GetAllCVsByUserIdQuery(userId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Cvs not found.", result.ErrorMessage);
        }
    }
}

