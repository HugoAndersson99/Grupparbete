using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.Users.Get.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, OperationResult<List<User>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetAllUsersQueryHandler> _logger;

        public GetAllUsersQueryHandler(IUserRepository userRepository, ILogger<GetAllUsersQueryHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<OperationResult<List<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Attempting to get all users.");

            try
            {
                var result = await _userRepository.GetAllUsersAsync();

                if (result.IsSuccess && result.Data != null && result.Data.Count > 0)
                {
                    _logger.LogInformation("Successfully retrieved {UserCount} users.", result.Data.Count);

                    return result;
                }

                _logger.LogWarning("No users found or failed to retrieve users.");
                return OperationResult<List<User>>.Failure("No users found or error occurred.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all users.");
                return OperationResult<List<User>>.Failure($"An error occurred: {ex.Message}", "Database error.");
            }
        }
    }
}
