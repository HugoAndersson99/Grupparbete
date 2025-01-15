using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Users.Add
{
    public class AddNewUserCommandHandler : IRequestHandler<AddNewUserCommand, OperationResult<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AddNewUserCommandHandler> _logger;


        public AddNewUserCommandHandler(IUserRepository userRepository, ILogger<AddNewUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<OperationResult<User>> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to add a new user with email: {Email}", request.NewUser.Email);

            var userToCreate = new User
            {
                Id = Guid.NewGuid(),
                Email = request.NewUser.Email,
            };

            userToCreate.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewUser.PasswordHash);

            try
            {
                var result = await _userRepository.AddUserAsync(userToCreate);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Successfully added a new user with email: {Email}", userToCreate.Email);
                    return OperationResult<User>.Success(userToCreate, "User added successfully.");
                }

                _logger.LogWarning("Failed to add user with email: {Email}. Error: {ErrorMessage}",
                                   userToCreate.Email, result.ErrorMessage);

                return OperationResult<User>.Failure(result.ErrorMessage, "Failed to add user.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding user with email: {Email}", userToCreate.Email);
                return OperationResult<User>.Failure($"An error occurred: {ex.Message}", "Database error.");
            }
        }
    }
}
