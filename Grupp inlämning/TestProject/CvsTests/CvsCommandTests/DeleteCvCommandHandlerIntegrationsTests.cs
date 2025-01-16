using Application.Commands.Cvs.DeleteCv;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using Infrastructure.Databases;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace TestProject.CvTests.CvCommandTests
{
    [TestFixture]
    public class DeleteCvCommandHandlerIntegrationTests
    {
        private DeleteCvCommandHandler _handler;
        private ICvRepository _cvRepository;
        private Database _dbContext;
        private ILogger<DeleteCvCommandHandler> _logger;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Database>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new Database(options);
            _cvRepository = new CvRepository(_dbContext, NullLogger<CvRepository>.Instance);
            _logger = new LoggerFactory().CreateLogger<DeleteCvCommandHandler>();
            _handler = new DeleteCvCommandHandler(_cvRepository, _logger);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenCvIsDeleted()
        {
            //Arrange
            var cvId = Guid.NewGuid();
            var cv = new CV { Id = cvId, Title = "Sample CV", PdfUrl = "http://example.com/sample.pdf", UserId = Guid.NewGuid() };
            _dbContext.CVs.Add(cv);
            await _dbContext.SaveChangesAsync();

            var command = new DeleteCvCommand(cvId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("CV deleted successfully.", result.Message);

            var deletedCv = await _dbContext.CVs.FindAsync(cvId);
            Assert.IsNull(deletedCv);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenCvDoesNotExist()
        {
            // Arrange
            var command = new DeleteCvCommand(Guid.NewGuid());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("CV not found.", result.ErrorMessage);
        }
    }
}


