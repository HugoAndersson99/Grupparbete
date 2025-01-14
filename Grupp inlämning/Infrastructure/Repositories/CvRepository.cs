

using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class CvRepository : ICvRepository
    {
        private readonly Database _database;
        private readonly ILogger<CvRepository> _logger;

        public CvRepository(Database database, ILogger<CvRepository> logger)
        {
            _database = database;
            _logger = logger;
        }

        public async Task<OperationResult<bool>> AddAsync(CV cv)
        {
            try
            {
                await _database.CVs.AddAsync(cv);
                await _database.SaveChangesAsync();

                return OperationResult<bool>.Success(true, "CV added successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure($"An error occurred: {ex.Message}");
            }
        }

        public async Task<OperationResult<bool>> DeleteAsync(Guid id)
        {
            var cv = await _database.CVs.FindAsync(id);
            if (cv == null)
            {
                return OperationResult<bool>.Failure("CV not found.");
            }

            try
            {
                _database.CVs.Remove(cv);
                await _database.SaveChangesAsync();

                return OperationResult<bool>.Success(true, "CV deleted successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure($"An error occurred: {ex.Message}");
            }
        }

        public async Task<OperationResult<IEnumerable<CV>>> GetAllByUserIdAsync(Guid userId)
        {
            var cvs = await _database.CVs
            .Where(c => c.UserId == userId)
            .ToListAsync();

            return OperationResult<IEnumerable<CV>>.Success(cvs);
        }

        public async Task<OperationResult<CV>> GetByIdAsync(Guid id)
        {
            var cv = await _database.CVs
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == id);

            if (cv == null)
            {
                return OperationResult<CV>.Failure("CV not found.");
            }

            return OperationResult<CV>.Success(cv);
        }

        public async Task<OperationResult<bool>> UpdateAsync(CV cv)
        {
            try
            {
                _database.CVs.Update(cv);
                await _database.SaveChangesAsync();

                return OperationResult<bool>.Success(true, "CV updated successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}
