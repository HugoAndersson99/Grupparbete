using Domain.Models;
using MediatR;

namespace Application.Commands.Educations.DeleteEducation
{
    public class DeleteEducationCommand : IRequest<OperationResult<Education>>
    {
        public DeleteEducationCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
