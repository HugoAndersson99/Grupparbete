using Domain.Models;
using MediatR;

namespace Application.Queries.Cvs.GetAllByUserId
{
    public class GetAllCVsByUserIdQuery : IRequest<OperationResult<IEnumerable<CV>>>
    {
        public Guid UserId { get; }

        public GetAllCVsByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
