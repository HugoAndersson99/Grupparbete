using Application.Interfaces.RepositoryInterfaces;
using Application.Queries.Cvs.GetById;
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
    public class GetCvByIdQueryHandlerIntegrationTests
    {
        private GetCvByIdQueryHandler _handler;
        private ICvRepository _cvRepository;
        private Database _dbContext;
        private ILogger<GetCvByIdQueryHandler> _logger;
       //private IMemoryCache _cache;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Database>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new Database(options);
            _cvRepository = new CvRepository(_dbContext, NullLogger<CvRepository>.Instance);
            _logger = new LoggerFactory().CreateLogger<GetCvByIdQueryHandler>();
           // _cache = new MemoryCache(new MemoryCacheOptions());

            _handler = new GetCvByIdQueryHandler(_cvRepository, _logger);

            var testUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "Test User",
                PasswordHash = "123"
            };
            _dbContext.Users.Add(testUser);
            _dbContext.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
            //_cache.Dispose();
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenCvIsInCache()
        {
            // Arrange
            var cvId = Guid.NewGuid();
            var cv = new CV { Id = cvId, Title = "Sample CV", PdfUrl = "http://example.com/sample.pdf", UserId = Guid.NewGuid() };
            _dbContext.CVs.Add(cv);
            await _dbContext.SaveChangesAsync();

            string cacheKey = $"CV_{cvId}";
           // _cache.Set(cacheKey, cv, TimeSpan.FromMinutes(5));

            var query = new GetCvByIdQuery(cvId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(cvId, result.Data.Id);
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenCvIsNotInCache_ButExistsInDatabase()
        {
            // Arrange
            var id = Guid.NewGuid();
            var testUserId = _dbContext.Users.First().Id;
            var cv = new CV { Id = id, UserId = testUserId, Title = "Sample CV", PdfUrl = "http://example.com/sample.pdf" };

            _dbContext.CVs.Add(cv);
            await _dbContext.SaveChangesAsync();

            var query = new GetCvByIdQuery(id);
           
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(id, result.Data.Id);
            Assert.AreEqual("Sample CV", result.Data.Title);
            Assert.AreEqual("http://example.com/sample.pdf", result.Data.PdfUrl);
            Assert.AreEqual(testUserId, result.Data.UserId);

           // var cachedCV = _cache.Get<CV>($"CV_{id}");
          //  Assert.IsNotNull(cachedCV);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenCvDoesNotExist()
        {
            // Arrange
            var query = new GetCvByIdQuery(Guid.NewGuid());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("CV not found.", result.ErrorMessage);
        }
    }
}
