using Domain.Models;
using MediatR;

namespace Application.Queries.Users.Get.GetById
{
    public class GetUserByIdQuery : IRequest<OperationResult<User>>
    {
        public Guid Id { get; }

        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }

}
