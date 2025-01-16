using Domain.Models;
using MediatR;

namespace Application.Commands.Cvs.DeleteCv
{
    public class DeleteCvCommand : IRequest<OperationResult<bool>>
    {
        public DeleteCvCommand(Guid cvId)
        {
            CvId = cvId;
        }

        public Guid CvId { get; }
    }
}
