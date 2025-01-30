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

        public GetAllCVsByUserIdQueryHandler(ICvRepository cvRepository,ILogger<GetAllCVsByUserIdQueryHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
        }

        public async Task<OperationResult<IEnumerable<CV>>> Handle(GetAllCVsByUserIdQuery request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Fetching CVs from database for user {UserId}", request.UserId);
            try
            {


                var result = await _cvRepository.GetAllByUserIdAsync(request.UserId);

                if (result.IsSuccess && result.Data != null)
                {
                    _logger.LogInformation("Successfully retrieved cvs from user Id: {UserId}", request.UserId);
                    return result;
                }
                else
                {
                    _logger.LogWarning("Failed to fetch CVs for user {UserId}: {Error}", request.UserId, result.ErrorMessage);
                    return OperationResult<IEnumerable<CV>>.Failure("Cvs not found.", "Database error.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting CV from user Id: {UserId}", request.UserId);
                return OperationResult<IEnumerable<CV>>.Failure($"An error occurred: {ex.Message}", "Database error.");
            }

            
        }
    }
}
