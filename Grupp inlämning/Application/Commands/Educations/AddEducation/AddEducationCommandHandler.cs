using Application.Interfaces.RepositoryInterfaces;
using MediatR;
using Domain.Models;

namespace Application.Commands.Educations.AddEducation
{
    public class AddEducationCommandHandler : IRequestHandler<AddEducationCommand, OperationResult<Education>>
    {
        private readonly IRepository<Education> _educationRepository;

        public AddEducationCommandHandler(IRepository<Education> educationRepository)
        {
            _educationRepository = educationRepository;
        }

        public async Task<OperationResult<Education>> Handle(AddEducationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var educationToAdd = new Education
                {
                    Id = Guid.NewGuid(),
                    SchoolName = request.EducationToAdd.SchoolName,
                    Degree = request.EducationToAdd.Degree,
                    FieldOfStudy = request.EducationToAdd.FieldOfStudy,
                    City = request.EducationToAdd.City,
                    Description = request.EducationToAdd.Description,
                    StartDate = request.EducationToAdd.StartDate,
                    EndDate = request.EducationToAdd.EndDate,
                };

                await _educationRepository.Add(educationToAdd);

                return OperationResult<Education>.Success(educationToAdd);
            }
            catch (Exception ex)
            {
                return OperationResult<Education>.Failure($"An error occurred while adding education: {ex.Message}");
            }
        }
    }
}
