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
        private readonly IMemoryCache _cache;

        public GetUserByIdQueryHandler(IUserRepository userRepository, ILogger<GetUserByIdQueryHandler> logger, IMemoryCache cache)
        {
            _userRepository = userRepository;
            _logger = logger;
            _cache = cache;
        }

        public async Task<OperationResult<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = $"User_{request.Id}";

            if (_cache.TryGetValue(cacheKey, out User cachedUser))
            {
                _logger.LogInformation("Returning cached user with Id: {UserId}", request.Id);
                return OperationResult<User>.Success(cachedUser);
            }

            _logger.LogInformation("Attempting to get user by Id: {UserId}", request.Id);

            try
            {
                var result = await _userRepository.GetUserByIdAsync(request.Id);

                if (result.IsSuccess && result.Data != null)
                {
                    _logger.LogInformation("Successfully retrieved user with Id: {UserId}", request.Id);

                    _cache.Set(cacheKey, result.Data, TimeSpan.FromMinutes(5));
                    _logger.LogInformation("User cached with Id: {UserId}", request.Id);

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
