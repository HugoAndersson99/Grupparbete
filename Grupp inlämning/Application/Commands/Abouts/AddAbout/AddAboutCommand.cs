using Domain.Models;
using MediatR;
using Application.Dtos;

namespace Application.Commands.Abouts.AddAbout
{
    public class AddAboutCommand : IRequest<OperationResult<About>>
    {
        public AddAboutCommand(AboutDto aboutToAdd)
        {
            AboutToAdd = aboutToAdd;
        }

        public AboutDto AboutToAdd { get; }
    }
}
