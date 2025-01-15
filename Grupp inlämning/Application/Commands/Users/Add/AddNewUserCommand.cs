using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Users.Add
{
    public class AddNewUserCommand : IRequest<OperationResult<User>>
    {
        public AddNewUserCommand(UserDto newUser)
        {
            NewUser = newUser;
        }

        public UserDto NewUser { get; set; }
    }
}
