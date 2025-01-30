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


        public GetCvByIdQueryHandler(ICvRepository cvRepository, ILogger<GetCvByIdQueryHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
        }

        public async Task<OperationResult<CV>> Handle(GetCvByIdQuery request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Attempting to get CV by Id: {CVId}", request.Id);

            try
            {
                var result = await _cvRepository.GetByIdAsync(request.Id);

                if (result.IsSuccess && result.Data != null)
                {
                    _logger.LogInformation("Successfully retrieved CV with Id: {CVId}", request.Id);

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
