using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Cvs.UpdateCv
{
    public class UpdateCvCommand : IRequest<OperationResult<CvDto>>
    {
        public UpdateCvCommand(CvDto cvToUpdate)
        {
            CvToUpdate = cvToUpdate;
        }

        public CvDto CvToUpdate { get; }
    }
}
