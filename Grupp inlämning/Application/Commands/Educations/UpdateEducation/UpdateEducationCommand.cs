using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Educations.UpdateEducation
{
    public class UpdateEducationCommand : IRequest<OperationResult<Education>>
    {
        public UpdateEducationCommand(Guid id, EducationDto updatedEducation)
        {
            Id = id;
            UpdatedEducation = updatedEducation;
        }
        public Guid Id { get; set; }
        public EducationDto UpdatedEducation { get; set; }
    }
}
