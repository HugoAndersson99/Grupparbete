using Domain.Models;
using MediatR;

namespace Application.Commands.Abouts.DeleteAbout
{
    public class DeleteAboutCommand: IRequest<OperationResult<About>>
    {
        public DeleteAboutCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
