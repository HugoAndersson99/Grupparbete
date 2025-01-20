using Application.Commands.Users.Add;
using Application.Dtos;
using Domain.Models;
using FakeItEasy;
using Infrastructure.Databases;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TestProject.UsersTests.UsersCommandTests
{
    [TestFixture]
    public class AddUserIntegrationTests
    {
        private Database _database;
        private UserRepository _userRepository;
        private AddNewUserCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            // Skapa en in-memory-databas för test
            var options = new DbContextOptionsBuilder<Database>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unik databas för varje testkörning
                .Options;

            _database = new Database(options);
            _userRepository = new UserRepository(_database, A.Fake<ILogger<UserRepository>>());
            _handler = new AddNewUserCommandHandler(_userRepository, A.Fake<ILogger<AddNewUserCommandHandler>>());
        }
        [TearDown]
        public void Teardown()
        {
            _database?.Dispose();
        }

        [Test]
        public async Task AddUser_ShouldAddUserToDatabase_WhenValidDataIsProvided()
        {
            // Arrange
            var newUserDto = new UserDto
            {
                Email = "test@example.com",
                PasswordHash = "TestPassword123"
            };
            var command = new AddNewUserCommand(newUserDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("test@example.com", result.Data.Email);

            var userInDb = await _database.Users.FirstOrDefaultAsync(u => u.Email == "test@example.com");
            Assert.IsNotNull(userInDb);
            Assert.IsTrue(BCrypt.Net.BCrypt.Verify("TestPassword123", userInDb.PasswordHash));
        }

        [Test]
        public async Task AddUser_ShouldReturnFailure_WhenUserWithSameEmailExists()
        {
            // Arrange
            var existingUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "duplicate@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("ExistingPassword")
            };

            await _database.Users.AddAsync(existingUser);
            await _database.SaveChangesAsync();

            var newUserDto = new UserDto
            {
                Email = "duplicate@example.com",
                PasswordHash = "NewPassword123"
            };
            var command = new AddNewUserCommand(newUserDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("A user with this Email already exists.", result.ErrorMessage);
        }

        [Test]
        public async Task AddUser_ShouldHandleException_WhenDatabaseFails()
        {
            // Arrange
            var newUserDto = new UserDto
            {
                Email = "test@example.com",
                PasswordHash = "TestPassword123"
            };
            var command = new AddNewUserCommand(newUserDto);

            await _database.DisposeAsync();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.StartsWith("An error occurred:"));
        }

        [Test]
        public async Task AddUser_ShouldPersistUser_WhenCalledMultipleTimes()
        {
            // Arrange
            var users = new List<UserDto>
        {
            new UserDto { Email = "user1@example.com", PasswordHash = "Password1" },
            new UserDto { Email = "user2@example.com", PasswordHash = "Password2" },
            new UserDto { Email = "user3@example.com", PasswordHash = "Password3" }
        };

            // Act
            foreach (var userDto in users)
            {
                var command = new AddNewUserCommand(userDto);
                var result = await _handler.Handle(command, CancellationToken.None);

                Assert.IsTrue(result.IsSuccess);
            }

            // Assert
            var usersInDb = await _database.Users.ToListAsync();
            Assert.AreEqual(3, usersInDb.Count);
            Assert.IsTrue(usersInDb.Any(u => u.Email == "user1@example.com"));
            Assert.IsTrue(usersInDb.Any(u => u.Email == "user2@example.com"));
            Assert.IsTrue(usersInDb.Any(u => u.Email == "user3@example.com"));
        }
    }
}
