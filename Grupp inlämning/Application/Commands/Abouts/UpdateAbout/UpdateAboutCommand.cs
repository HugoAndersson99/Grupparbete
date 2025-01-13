using Domain.Models;
using MediatR;
using Application.Dtos;

namespace Application.Commands.Abouts.UpdateAbout
{
    public class UpdateAboutCommand : IRequest<OperationResult<About>>
    {
        public UpdateAboutCommand(Guid id, AboutDto updatedAbout)
        {
            Id = id;
            UpdatedAbout = updatedAbout;
        }
        public Guid Id { get; }
        public AboutDto UpdatedAbout { get; }
    }
}
