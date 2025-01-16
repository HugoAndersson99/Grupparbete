using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Application.Queries.Cvs.GetAllByUserId
{
    public class GetAllCVsByUserIdQueryHandler : IRequestHandler<GetAllCVsByUserIdQuery, OperationResult<IEnumerable<CV>>>
    {
        private readonly ICvRepository _cvRepository;
        private readonly ILogger<GetAllCVsByUserIdQueryHandler> _logger;
        private readonly IMemoryCache _cache;

        public GetAllCVsByUserIdQueryHandler(ICvRepository cvRepository,ILogger<GetAllCVsByUserIdQueryHandler> logger,IMemoryCache cache)
        {
            _cvRepository = cvRepository;
            _logger = logger;
            _cache = cache;
        }

        public async Task<OperationResult<IEnumerable<CV>>> Handle(GetAllCVsByUserIdQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = $"CVs_User_{request.UserId}";
            if (_cache.TryGetValue(cacheKey, out IEnumerable<CV> cachedCVs))
            {
                _logger.LogInformation("Fetching CVs from cache for user {UserId}", request.UserId);
                return OperationResult<IEnumerable<CV>>.Success(cachedCVs);
            }

            _logger.LogInformation("Fetching CVs from database for user {UserId}", request.UserId);
            var result = await _cvRepository.GetAllByUserIdAsync(request.UserId);

            if (result.IsSuccess && result.Data != null)
            {
                _cache.Set(cacheKey, result.Data, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(10),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                });

                _logger.LogInformation("CVs cached for user {UserId}", request.UserId);
            }
            else
            {
                _logger.LogWarning("Failed to fetch CVs for user {UserId}: {Error}", request.UserId, result.ErrorMessage);
            }

            return result;
        }
    }
}
