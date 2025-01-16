
using Application.Commands.Cvs.Add;
using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace TestProject.CvsTests.CvsCommandTests
{
    [TestFixture]
    public class AddCvCommandHandlerTests
    {
        private AddCvCommandHandler _handler;
        private ICvRepository _mockCvRepository;
        private ILogger<AddCvCommandHandler> _mockLogger;
        private IUserRepository _mockUserRepository;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = A.Fake<IUserRepository>();
            _mockCvRepository = A.Fake<ICvRepository>();
            _mockLogger = A.Fake<ILogger<AddCvCommandHandler>>();
            _handler = new AddCvCommandHandler(_mockCvRepository, _mockUserRepository ,_mockLogger);
        }

        [Test]
        public async Task Handle_ShouldAddCvSuccessfully_WhenUserExistsAndRepositoryReturnsSuccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var cvDto = new CvDto
            {
                Title = "Test CV",
                UserId = userId,
                PdfUrl = "http://example.com/test.pdf"
            };
            var command = new AddCvCommand(cvDto);

            var user = new User { Id = userId, Email = "Test User" };
            A.CallTo(() => _mockUserRepository.GetUserByIdAsync(userId))
                .Returns(Task.FromResult(OperationResult<User>.Success(user)));

            A.CallTo(() => _mockCvRepository.AddAsync(A<CV>.Ignored))
                .Returns(Task.FromResult(OperationResult<bool>.Success(true, "CV added successfully.")));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("CV added successfully.", result.Message);
            Assert.AreEqual(cvDto.Title, result.Data.Title);

            A.CallTo(() => _mockUserRepository.GetUserByIdAsync(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _mockCvRepository.AddAsync(A<CV>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var cvDto = new CvDto
            {
                Title = "Test CV",
                UserId = userId,
                PdfUrl = "http://example.com/test.pdf"
            };
            var command = new AddCvCommand(cvDto);

            // Mockar att användaren inte finns
            A.CallTo(() => _mockUserRepository.GetUserByIdAsync(userId))
                .Returns(Task.FromResult<OperationResult<User>>(null)); // Simulerar att användaren inte hittas

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("User not found.", result.ErrorMessage);

            A.CallTo(() => _mockUserRepository.GetUserByIdAsync(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _mockCvRepository.AddAsync(A<CV>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenRepositoryFailsToAddCv()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var cvDto = new CvDto
            {
                Title = "Test CV",
                UserId = userId,
                PdfUrl = "http://example.com/test.pdf"
            };
            var command = new AddCvCommand(cvDto);

            var user = new User { Id = userId, Email = "Test User" };
            A.CallTo(() => _mockUserRepository.GetUserByIdAsync(userId))
                .Returns(Task.FromResult(OperationResult<User>.Success(user)));

            A.CallTo(() => _mockCvRepository.AddAsync(A<CV>.Ignored))
                .Returns(Task.FromResult(OperationResult<bool>.Failure("Error while adding CV.")));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Error while adding CV.", result.ErrorMessage);

            A.CallTo(() => _mockUserRepository.GetUserByIdAsync(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _mockCvRepository.AddAsync(A<CV>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}
