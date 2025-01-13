using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Educations.AddEducation
{
    public class AddEducationCommand : IRequest<OperationResult<Education>>
    {
        public AddEducationCommand(EducationDto educationToAdd)
        {
            EducationToAdd = educationToAdd;
        }

        public EducationDto EducationToAdd { get; }
    }
}
