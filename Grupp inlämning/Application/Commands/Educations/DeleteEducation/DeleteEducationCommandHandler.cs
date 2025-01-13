using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Educations.DeleteEducation
{
    public class DeleteEducationCommandHandler : IRequestHandler<DeleteEducationCommand, OperationResult<Education>>
    {
        private readonly IRepository<Education> _educationRepository;
        private readonly ILogger<DeleteEducationCommandHandler> _logger;
        public DeleteEducationCommandHandler(IRepository<Education> educationRepository, ILogger<DeleteEducationCommandHandler> logger)
        {
            _educationRepository = educationRepository;
            _logger = logger;
        }
        public async Task<OperationResult<Education>> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var educationToDelete = _educationRepository.GetById(request.Id);

                if (educationToDelete != null)
                {
                    _educationRepository.Delete(educationToDelete);

                    _logger.LogInformation("Education with ID: {EducationId} was successfully deleted.", request.Id);

                    return OperationResult<Education>.Success(educationToDelete, "Education deleted successfully.");
                }

                _logger.LogWarning("Attempted to delete education with ID: {EducationId}, but no education was found.", request.Id);

                return OperationResult<Education>.Failure($"Education with ID: {request.Id} was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while attempting to delete education with ID: {EducationId}", request.Id);
                return OperationResult<Education>.Failure("An unexpected error occurred while deleting education.");
            }
        }
    }
}
