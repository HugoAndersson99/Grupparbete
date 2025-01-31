using Application.Interfaces.RepositoryInterfaces;
using Application.Queries.Cvs.GetById;
using Domain.Models;
using Infrastructure.Databases;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace TestProject.CvsTests.CvsQueryTests
{
    [TestFixture]
    public class GetCvByIdQueryHandlerIntegrationTests
    {
        private GetCvByIdQueryHandler _handler;
        private ICvRepository _cvRepository;
        private Database _dbContext;
        private ILogger<GetCvByIdQueryHandler> _logger;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Database>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new Database(options);
            _cvRepository = new CvRepository(_dbContext, NullLogger<CvRepository>.Instance);
            _logger = new LoggerFactory().CreateLogger<GetCvByIdQueryHandler>();
            _handler = new GetCvByIdQueryHandler(_cvRepository, _logger);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenCvExistsInDatabase()
        {
            // Arrange
            var testUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "Test User",
                PasswordHash = "123"
            };
            _dbContext.Users.Add(testUser);
            _dbContext.SaveChanges();

            var cvId = Guid.NewGuid();
            var cv = new CV { Id = cvId, Title = "Sample CV", PdfUrl = "http://example.com/sample.pdf", UserId = testUser.Id };
            _dbContext.CVs.Add(cv);
            await _dbContext.SaveChangesAsync();

            var query = new GetCvByIdQuery(cvId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(cvId, result.Data.Id);
            Assert.AreEqual("Sample CV", result.Data.Title);
            Assert.AreEqual("http://example.com/sample.pdf", result.Data.PdfUrl);
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

