using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<OperationResult<User>> AddUserAsync(User user);
        Task<OperationResult<List<User>>> GetAllUsersAsync();
        Task<OperationResult<User>> LoginUserAsync(string email, string password);
        Task<User> GetUserByEmailAsync(string email);
    }
}
