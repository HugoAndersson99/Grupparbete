using Application.Commands.Cvs.Add;
using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using Infrastructure.Databases;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace TestProject.CvsTests.CvsCommandTests
{
    [TestFixture]
    public class AddCvCommandHandlerIntegrationTest
    {
        private Database _dbContext;
        private ICvRepository _cvRepository;
        private IUserRepository _userRepository;
        private AddCvCommandHandler _handler;
        private ILogger<AddCvCommandHandler> _logger;


        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Database>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new Database(options);
            _cvRepository = new CvRepository(_dbContext, NullLogger<CvRepository>.Instance);
            _userRepository = new UserRepository(_dbContext, NullLogger<UserRepository>.Instance);

            _logger = new LoggerFactory().CreateLogger<AddCvCommandHandler>();

            _handler = new AddCvCommandHandler(_cvRepository, _userRepository, _logger);

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
        public void Teardown()
        {
            _dbContext?.Dispose();
        }

        [Test]
        public async Task Handle_ShouldAddCvSuccessfully_WhenUserExists()
        {
            // Arrange
            var testUserId = _dbContext.Users.First().Id;

            var cvDto = new CvDto
            {
                Title = "Test CV",
                UserId = testUserId,
                PdfUrl = "http://example.com/test.pdf"
            };

            var command = new AddCvCommand(cvDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Test CV", result.Data.Title);

            var addedCv = _dbContext.CVs.FirstOrDefault(c => c.Id == result.Data.Id);
            Assert.IsNotNull(addedCv);
            Assert.AreEqual("Test CV", addedCv.Title);
            Assert.AreEqual(testUserId, addedCv.UserId);
        }
        [Test]
        public async Task Handle_ShouldReturnFailure_WhenUserDoesNotExist()
        {
            // Arrange
            var invalidUserId = Guid.NewGuid(); // Ogiltigt användar-ID

            var cvDto = new CvDto
            {
                Title = "Invalid CV",
                UserId = invalidUserId,
                PdfUrl = "http://example.com/invalid.pdf"
            };

            var command = new AddCvCommand(cvDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("User not found.", result.ErrorMessage);

            var addedCv = _dbContext.CVs.FirstOrDefault(c => c.UserId == invalidUserId);
            Assert.IsNull(addedCv);
        }
    }
}
