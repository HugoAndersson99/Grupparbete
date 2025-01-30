using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Application.Queries.Users.Get.GetById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, OperationResult<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetUserByIdQueryHandler> _logger;

        public GetUserByIdQueryHandler(IUserRepository userRepository, ILogger<GetUserByIdQueryHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<OperationResult<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
           
            _logger.LogInformation("Attempting to get user by Id: {UserId}", request.Id);

            try
            {
                var result = await _userRepository.GetUserByIdAsync(request.Id);

                if (result.IsSuccess && result.Data != null)
                {
                    _logger.LogInformation("Successfully retrieved user with Id: {UserId}", request.Id);

                    return result;
                }

                _logger.LogWarning("Failed to retrieve user with Id: {UserId}", request.Id);
                return OperationResult<User>.Failure("User not found.", "Database error.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting user by Id: {UserId}", request.Id);
                return OperationResult<User>.Failure($"An error occurred: {ex.Message}", "Database error.");
            }
        }
    }

}
