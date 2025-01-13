using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Educations.UpdateEducation
{
    public class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand, OperationResult<Education>>
    {
        private readonly IRepository<Education> _educationRepository;
        private readonly ILogger<UpdateEducationCommandHandler> _logger;

        public UpdateEducationCommandHandler(IRepository<Education> educationRepository, ILogger<UpdateEducationCommandHandler> logger)
        {
            _educationRepository = educationRepository;
            _logger = logger;
        }

        public async Task<OperationResult<Education>> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var educationToUpdate = _educationRepository.GetById(request.Id);
                if (educationToUpdate == null)
                {
                    _logger.LogWarning("Attempted to update education with ID: {EducationId}, but no education was found.", request.Id);
                    return OperationResult<Education>.Failure($"Education with ID {request.Id} not found.");
                }

                UpdateEducation(educationToUpdate, request.UpdatedEducation);
                _educationRepository.Update(educationToUpdate);

                _logger.LogInformation("Education with ID: {EducationId} was successfully updated.", request.Id);

                return OperationResult<Education>.Success(educationToUpdate, "Education details updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating education with ID: {EducationId}", request.Id);
                return OperationResult<Education>.Failure("An unexpected error occurred while updating the education details.");
            }
        }

        private void UpdateEducation(Education educationToUpdate, EducationDto updatedEducation)
        {
            educationToUpdate.SchoolName = updatedEducation.SchoolName;
            educationToUpdate.Degree = updatedEducation.Degree;
            educationToUpdate.FieldOfStudy = updatedEducation.FieldOfStudy;
            educationToUpdate.City = updatedEducation.City;
            educationToUpdate.Description = updatedEducation.Description;
            educationToUpdate.StartDate = updatedEducation.StartDate;
            educationToUpdate.EndDate = updatedEducation.EndDate;
        }

    }
}
