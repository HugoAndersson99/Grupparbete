using Application.Commands.Cvs.DeleteCv;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace TestProject.CvTests.CvCommandTests
{
    [TestFixture]
    public class DeleteCvCommandHandlerTests
    {
        private DeleteCvCommandHandler _handler;
        private ICvRepository _mockCvRepository;
        private ILogger<DeleteCvCommandHandler> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockCvRepository = A.Fake<ICvRepository>();
            _mockLogger = A.Fake<ILogger<DeleteCvCommandHandler>>();
            _handler = new DeleteCvCommandHandler(_mockCvRepository, _mockLogger);
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenCvIsDeleted()
        {
            // Arrange
            var command = new DeleteCvCommand(Guid.NewGuid());

            A.CallTo(() => _mockCvRepository.DeleteAsync(command.CvId))
                .Returns(OperationResult<bool>.Success(true, "CV deleted successfully."));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("CV deleted successfully.", result.Message);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenCvDoesNotExist()
        {
            // Arrange
            var command = new DeleteCvCommand(Guid.NewGuid());

            A.CallTo(() => _mockCvRepository.DeleteAsync(command.CvId))
                .Returns(OperationResult<bool>.Failure("CV not found."));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("CV not found.", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenAnErrorOccursDuringDeletion()
        {
            // Arrange
            var command = new DeleteCvCommand(Guid.NewGuid());
            var exceptionMessage = "Database error occurred.";

            A.CallTo(() => _mockCvRepository.DeleteAsync(command.CvId))
                .Throws(new Exception(exceptionMessage));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"An error occurred: {exceptionMessage}", result.ErrorMessage);
        }
    }
}

