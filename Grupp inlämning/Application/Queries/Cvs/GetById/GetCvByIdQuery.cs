using Domain.Models;
using MediatR;

namespace Application.Queries.Cvs.GetById
{
    public class GetCvByIdQuery : IRequest<OperationResult<CV>>
    {
        public Guid Id { get; }
        public GetCvByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
