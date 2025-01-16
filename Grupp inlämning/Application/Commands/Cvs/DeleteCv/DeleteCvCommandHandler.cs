using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Cvs.DeleteCv
{
    public class DeleteCvCommandHandler : IRequestHandler<DeleteCvCommand, OperationResult<bool>>
    {
        private readonly ICvRepository _cvRepository;
        private readonly ILogger<DeleteCvCommandHandler> _logger;

        public DeleteCvCommandHandler(ICvRepository cvRepository, ILogger<DeleteCvCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
        }

        public async Task<OperationResult<bool>> Handle(DeleteCvCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to delete CV with Id: {CvId}", request.CvId);

            try
            {
                var deleteResult = await _cvRepository.DeleteAsync(request.CvId);

                if (!deleteResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to delete CV with Id: {CvId}. Reason: {Reason}", request.CvId, deleteResult.ErrorMessage);
                    return OperationResult<bool>.Failure(deleteResult.ErrorMessage);
                }

                _logger.LogInformation("Successfully deleted CV with Id: {CvId}", request.CvId);
                return OperationResult<bool>.Success(true, "CV deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting CV with Id: {CvId}", request.CvId);
                return OperationResult<bool>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}
