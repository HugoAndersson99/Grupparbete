using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Queries.Users.Login
{
    public class LoginUserQuery : IRequest<OperationResult<string>>
    {
        public LoginUserQuery(UserDto loginUser)
        {
            LoginUser = loginUser;
        }

        public UserDto LoginUser { get; set; }
    }
}
