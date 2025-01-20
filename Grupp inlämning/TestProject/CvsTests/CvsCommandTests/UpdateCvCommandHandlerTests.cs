using Application.Commands.Cvs.UpdateCv;
using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace TestProject.CvTests.CvCommandTests
{
    [TestFixture]
    public class UpdateCvCommandHandlerTests
    {
        private UpdateCvCommandHandler _handler;
        private ICvRepository _mockCvRepository;
        private ILogger<UpdateCvCommandHandler> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockCvRepository = A.Fake<ICvRepository>();
            _mockLogger = A.Fake<ILogger<UpdateCvCommandHandler>>();
            _handler = new UpdateCvCommandHandler(_mockCvRepository, _mockLogger);
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenCvIsUpdated()
        {
            // Arrange
            var command = new UpdateCvCommand(new CvDto
            {
                Id = Guid.NewGuid(),
                Title = "Updated CV Title",
                PdfUrl = "http://example.com/updated.pdf",
                UserId = Guid.NewGuid()
            });

            var updatedCv = new CV
            {
                Id = command.CvToUpdate.Id,
                Title = "Updated CV Title",
                PdfUrl = "http://example.com/updated.pdf",
                UserId = command.CvToUpdate.UserId
            };

            A.CallTo(() => _mockCvRepository.GetByIdAsync(command.CvToUpdate.Id))
                .Returns(OperationResult<CV>.Success(updatedCv));

            A.CallTo(() => _mockCvRepository.UpdateAsync(A<CV>.That.Matches(c =>
                c.Id == updatedCv.Id && c.Title == updatedCv.Title && c.PdfUrl == updatedCv.PdfUrl)))
                .Returns(OperationResult<bool>.Success(true, "CV updated successfully."));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("CV updated successfully.", result.Message);
            Assert.AreEqual(updatedCv.Title, result.Data.Title);
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

            A.CallTo(() => _mockCvRepository.GetByIdAsync(command.CvToUpdate.Id))
                .Returns(OperationResult<CV>.Failure("CV not found."));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("CV not found.", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenCvDataIsInvalid()
        {
            // Arrange
            var command = new UpdateCvCommand(new CvDto
            {
                Id = Guid.NewGuid(),
                Title = string.Empty,
                PdfUrl = "http://example.com/invalid.pdf",
                UserId = Guid.NewGuid()
            });

            var existingCv = new CV
            {
                Id = command.CvToUpdate.Id,
                Title = "Old CV Title",
                PdfUrl = "http://example.com/old.pdf",
                UserId = command.CvToUpdate.UserId
            };

            A.CallTo(() => _mockCvRepository.GetByIdAsync(command.CvToUpdate.Id))
                .Returns(OperationResult<CV>.Success(existingCv));

            A.CallTo(() => _mockCvRepository.UpdateAsync(A<CV>.Ignored))
                .Returns(OperationResult<bool>.Failure("Invalid CV data."));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Invalid CV data.", result.ErrorMessage);
        }
    }
}


