using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Database _database;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(Database database, ILogger<UserRepository> logger)
        {
            _database = database;
            _logger = logger;
        }

        public async Task<OperationResult<User>> AddUserAsync(User user)
        {
            try
            {
                _logger.LogInformation("Attempting to add user with Email: {Email}", user.Email);

                var existingUser = await _database.Users
                    .FirstOrDefaultAsync(u => u.Email == user.Email);

                if (existingUser != null)
                {
                    _logger.LogWarning("User with Email {Email} already exists.", user.Email);
                    return OperationResult<User>.Failure("A user with this Email already exists.", "Duplicate error.");
                }

                await _database.Users.AddAsync(user);
                await _database.SaveChangesAsync();

                _logger.LogInformation("Successfully added user with Email: {Email}", user.Email);
                return OperationResult<User>.Success(user, "User added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding user with Email: {Email}", user.Email);
                return OperationResult<User>.Failure($"An error occurred: {ex.Message}", "Database error.");
            }
        }

        public async Task<OperationResult<List<User>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _database.Users.ToListAsync();
                if (users == null || !users.Any())
                {
                    _logger.LogWarning("No users found in the database.");
                    return OperationResult<List<User>>.Failure("No users found.");
                }

                _logger.LogInformation("Successfully retrieved {UserCount} users.", users.Count);
                return OperationResult<List<User>>.Success(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving users.");
                return OperationResult<List<User>>.Failure($"An error occurred: {ex.Message}");
            }
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            _logger.LogInformation("Attempting to retrieve user with Id: {UserId}", id);

            try
            {
                var user = await _database.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    _logger.LogWarning("No user found with Id: {UserId}", id);
                }
                else
                {
                    _logger.LogInformation("Successfully retrieved user with Id: {UserId}", id);
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user with Id: {UserId}", id);
                throw;
            }
        }

        public async Task<OperationResult<User>> LoginUserAsync(string email, string password)
        {
            try
            {
                var user = await _database.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    _logger.LogWarning("Login failed: No user found with email {Email}", email);
                    return OperationResult<User>.Failure("Invalid email or password.");
                }

                if (user.PasswordHash != password)
                {
                    _logger.LogWarning("Login failed: Incorrect password for email {Email}", email);
                    return OperationResult<User>.Failure("Invalid email or password.");
                }

                _logger.LogInformation("User {UserName} logged in successfully.", email);
                return OperationResult<User>.Success(user, "Login successful.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in user {email}.", email);
                return OperationResult<User>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}
