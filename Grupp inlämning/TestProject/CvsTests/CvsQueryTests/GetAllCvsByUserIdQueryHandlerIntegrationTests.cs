using Application.Interfaces.RepositoryInterfaces;
using Application.Queries.Cvs.GetAllByUserId;
using Domain.Models;
using Infrastructure.Repositories;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;

namespace TestProject.CvsTests.CvsQueryTests
{
    [TestFixture]
    public class GetAllCVsByUserIdQueryHandlerIntegrationTests
    {
        private GetAllCVsByUserIdQueryHandler _handler;
        private ICvRepository _cvRepository;
        private Database _dbContext;
        private ILogger<GetAllCVsByUserIdQueryHandler> _logger;
        private IMemoryCache _memoryCache;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Database>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new Database(options);
            _cvRepository = new CvRepository(_dbContext, NullLogger<CvRepository>.Instance);
            _logger = new LoggerFactory().CreateLogger<GetAllCVsByUserIdQueryHandler>();
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _handler = new GetAllCVsByUserIdQueryHandler(_cvRepository, _logger, _memoryCache);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
            _memoryCache.Dispose();
        }

        [Test]
        public async Task Handle_ShouldReturnCVsFromDatabase_WhenNotInCache()
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

            Assert.IsTrue(_memoryCache.TryGetValue($"CVs_User_{userId}", out _));
        }

        [Test]
        public async Task Handle_ShouldReturnCVsFromCache_WhenAlreadyCached()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var cv1 = new CV { Id = Guid.NewGuid(), UserId = userId, Title = "CV 1", PdfUrl = "http://example.com/cv1.pdf" };
            var cv2 = new CV { Id = Guid.NewGuid(), UserId = userId, Title = "CV 2", PdfUrl = "http://example.com/cv2.pdf" };
            _dbContext.CVs.AddRange(cv1, cv2);
            await _dbContext.SaveChangesAsync();

            var query = new GetAllCVsByUserIdQuery (userId);

            await _handler.Handle(query, CancellationToken.None);

            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result.Data.Count());
            Assert.Contains(cv1, result.Data.ToList());
            Assert.Contains(cv2, result.Data.ToList());

            Assert.IsTrue(_memoryCache.TryGetValue($"CVs_User_{userId}", out _));
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
            Assert.AreEqual("No CVs found.", result.ErrorMessage); 
        }
    }
}
