using Domain.Models;
using MediatR;

namespace Application.Queries.Users.Get.GetAll
{
    public class GetAllUsersQuery : IRequest<OperationResult<List<User>>>
    {
    }
}
