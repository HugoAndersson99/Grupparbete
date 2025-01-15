using Application.Interfaces.RepositoryInterfaces;
using Application.Queries.Users.Login.Helpers;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.Users.Login
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, OperationResult<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<LoginUserQueryHandler> _logger;
        private readonly TokenHelper _tokenHelper;

        public LoginUserQueryHandler(IUserRepository userRepository, ILogger<LoginUserQueryHandler> logger, TokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _tokenHelper = tokenHelper;
        }
        public async Task<OperationResult<string>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userResult = await _userRepository.GetUserByEmailAsync(request.LoginUser.Email);

                if (!userResult.IsSuccess || userResult.Data == null)
                {
                    _logger.LogWarning("Invalid login attempt for email: {Email}", request.LoginUser.Email);
                    return OperationResult<string>.Failure("Invalid email or password");
                }

                var user = userResult.Data;

                if (!BCrypt.Net.BCrypt.Verify(request.LoginUser.PasswordHash, user.PasswordHash))
                {
                    _logger.LogWarning("Invalid password for user: {Email}", request.LoginUser.Email);
                    return OperationResult<string>.Failure("Invalid password.", "Authentication error.");
                }

                string token = _tokenHelper.GenerateJwtToken(user);

                _logger.LogInformation("User {Email} successfully logged in.", request.LoginUser.Email);

                return OperationResult<string>.Success(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while attempting to log in user: {Email}", request.LoginUser.Email);
                return OperationResult<string>.Failure($"An error occurred: {ex.Message}");
            }
        }

    }
}
