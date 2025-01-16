using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface ICvRepository
    {
        Task<OperationResult<CV>> GetByIdAsync(Guid id);
        Task<OperationResult<IEnumerable<CV>>> GetAllByUserIdAsync(Guid userId);
        Task<OperationResult<bool>> AddAsync(CV cv);
        Task<OperationResult<bool>> UpdateAsync(CV cv);
        Task<OperationResult<bool>> DeleteAsync(Guid id);
    }
}
