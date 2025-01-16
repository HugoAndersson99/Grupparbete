using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Cvs.Add
{
    public class AddCvCommand : IRequest<OperationResult<CvDto>>
    {
        public AddCvCommand(CvDto cvToAdd)
        {
            CvToAdd = cvToAdd;
        }

        public CvDto CvToAdd { get; }
    }
}
