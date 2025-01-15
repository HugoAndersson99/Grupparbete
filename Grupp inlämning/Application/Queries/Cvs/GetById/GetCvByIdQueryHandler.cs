using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Application.Queries.Cvs.GetById
{
    public class GetCvByIdQueryHandler : IRequestHandler<GetCvByIdQuery, OperationResult<CV>>
    {
        private readonly ICvRepository _cvRepository;
        private readonly ILogger<GetCvByIdQueryHandler> _logger;
        private readonly IMemoryCache _cache;

        public GetCvByIdQueryHandler(ICvRepository cvRepository, ILogger<GetCvByIdQueryHandler> logger, IMemoryCache cache)
        {
            _cvRepository = cvRepository;
            _logger = logger;
            _cache = cache;
        }

        public async Task<OperationResult<CV>> Handle(GetCvByIdQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = $"CV_{request.Id}";

            if (_cache.TryGetValue(cacheKey, out CV cachedCV))
            {
                _logger.LogInformation("Returning cached CV with Id: {CVId}", request.Id);
                return OperationResult<CV>.Success(cachedCV);
            }

            _logger.LogInformation("Attempting to get CV by Id: {CVId}", request.Id);

            try
            {
                var result = await _cvRepository.GetByIdAsync(request.Id);

                if (result.IsSuccess && result.Data != null)
                {
                    _logger.LogInformation("Successfully retrieved CV with Id: {CVId}", request.Id);

                    _cache.Set(cacheKey, result.Data, TimeSpan.FromMinutes(5));
                    _logger.LogInformation("CV cached with Id: {CVId}", request.Id);

                    return result;
                }

                _logger.LogWarning("Failed to retrieve CV with Id: {CVId}", request.Id);
                return OperationResult<CV>.Failure("CV not found.", "Database error.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting CV by Id: {CVId}", request.Id);
                return OperationResult<CV>.Failure($"An error occurred: {ex.Message}", "Database error.");
            }
        }
    }
}
