using Application.Commands.Cvs.UpdateCv;
using Application.Dtos;
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
    public class UpdateCvCommandHandlerIntegrationTests
    {
        private UpdateCvCommandHandler _handler;
        private ICvRepository _cvRepository;
        private Database _dbContext;
        private ILogger<UpdateCvCommandHandler> _logger;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Database>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new Database(options);
            _cvRepository = new CvRepository(_dbContext, NullLogger<CvRepository>.Instance);
            _logger = new LoggerFactory().CreateLogger<UpdateCvCommandHandler>();
            _handler = new UpdateCvCommandHandler(_cvRepository, _logger);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenCvIsUpdated()
        {
            // Arrange
            var testUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "Test User",
                PasswordHash = "123"
            };

            var cvId = Guid.NewGuid();
            var existingCv = new CV
            {
                Id = cvId,
                Title = "Old Title",
                PdfUrl = "http://example.com/old.pdf",
                UserId = testUser.Id,
                User = testUser
            };
            _dbContext.CVs.Add(existingCv);
            await _dbContext.SaveChangesAsync();

            var command = new UpdateCvCommand(new CvDto
            {
                Id = cvId,
                Title = "Updated Title",
                PdfUrl = "http://example.com/updated.pdf",
                UserId = existingCv.UserId
            });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("CV updated successfully.", result.Message);
            Assert.AreEqual("Updated Title", result.Data.Title);
            Assert.AreEqual("http://example.com/updated.pdf", result.Data.PdfUrl);

            var updatedCv = await _dbContext.CVs.FindAsync(cvId);
            Assert.IsNotNull(updatedCv);
            Assert.AreEqual("Updated Title", updatedCv.Title);
            Assert.AreEqual("http://example.com/updated.pdf", updatedCv.PdfUrl);
        }


        [Test]
        public async Task Handle_ShouldReturnFailure_WhenCvDoesNotExist()
        {
            // Arrange
            var command = new UpdateCvCommand(new CvDto
            {
                Id = Guid.NewGuid(),
                Title = "Non-existent CV",
                PdfUrl = "http://example.com/nonexistent.pdf",
                UserId = Guid.NewGuid()
            });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("CV not found.", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenUpdateFails()
        {
            // Arrange
            var cvId = Guid.NewGuid();
            var existingCv = new CV { Id = cvId, Title = "Old Title", PdfUrl = "http://example.com/old.pdf", UserId = Guid.NewGuid() };
            _dbContext.CVs.Add(existingCv);
            await _dbContext.SaveChangesAsync();

            var command = new UpdateCvCommand(new CvDto
            {
                Id = cvId,
                Title = "",
                PdfUrl = "http://example.com/updated.pdf",
                UserId = existingCv.UserId
            });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("CV not found.", result.ErrorMessage);
        }
    }
}

