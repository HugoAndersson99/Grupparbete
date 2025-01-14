using Application.Commands.Users.Add;
using Application.Dtos;
using Application.Queries.Users.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        internal readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;

        }

        [HttpGet]
        [Route("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await mediator.Send(new GetAllUsersQuery());

            if (!result.IsSuccessfull)
            {
                return BadRequest(new { message = result.Message, errors = result.ErrorMessage });
            }
            return Ok(new { message = result.Message, data = result.Data });
        }

        [HttpPost]
        [Route("addNewUser")]
        public async Task<IActionResult> RegisterNewUser([FromBody] UserDto newUser)
        {
            var command = new AddNewUserCommand(newUser);
            var result = await mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.Message, errors = result.ErrorMessage });
            }
            return Ok(new { message = result.Message, data = result.Data });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserDto loginUser)
        {
            var query = new LoginUserQuery(loginUser);
            var result = await mediator.Send(query);

            if (!result.IsSuccess)
            {
                return Unauthorized(new { message = result.Message, errors = result.ErrorMessage });
            }

            return Ok(new { message = result.Message, token = result.Data });
        }
    }
}
