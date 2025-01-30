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
                _logger.LogInformation("Attempting to add CV for UserId: {UserId}", cv.UserId);

                await _database.CVs.AddAsync(cv);
                await _database.SaveChangesAsync();

                _logger.LogInformation("Successfully added CV with Id: {CVId} for UserId: {UserId}", cv.Id, cv.UserId);
                return OperationResult<bool>.Success(true, "CV added successfully.");
                _database.Update(cv);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding CV for UserId: {UserId}", cv.UserId);
                return OperationResult<bool>.Failure($"An error occurred: {ex.Message}");
            }
        }

        public async Task<OperationResult<bool>> DeleteAsync(Guid id)
        {
            _logger.LogInformation("Attempting to delete CV with Id: {CVId}", id);

            var cv = await _database.CVs.FindAsync(id);
            if (cv == null)
            {
                _logger.LogWarning("CV with Id: {CVId} not found for deletion.", id);
                return OperationResult<bool>.Failure("CV not found.");
            }

            try
            {
                _database.CVs.Remove(cv);
                await _database.SaveChangesAsync();

                _logger.LogInformation("Successfully deleted CV with Id: {CVId}", id);
                return OperationResult<bool>.Success(true, "CV deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting CV with Id: {CVId}", id);
                return OperationResult<bool>.Failure($"An error occurred: {ex.Message}");
            }
        }

        public async Task<OperationResult<IEnumerable<CV>>> GetAllByUserIdAsync(Guid userId)
        {
            _logger.LogInformation("Retrieving all CVs for UserId: {UserId}", userId);

            try
            {
                var cvs = await _database.CVs.Where(c => c.UserId == userId).ToListAsync();

                if (!cvs.Any())
                {
                    _logger.LogWarning("No CVs found for UserId: {UserId}", userId);
                    return OperationResult<IEnumerable<CV>>.Failure("No CVs found.");
                }

                _logger.LogInformation("Successfully retrieved {CVCount} CVs for UserId: {UserId}", cvs.Count, userId);
                return OperationResult<IEnumerable<CV>>.Success(cvs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving CVs for UserId: {UserId}", userId);
                return OperationResult<IEnumerable<CV>>.Failure($"An error occurred: {ex.Message}");
            }
        }

        public async Task<OperationResult<CV>> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Retrieving CV with Id: {CVId}", id);

            try
            {
                var cv = await _database.CVs.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id);

                if (cv == null)
                {
                    _logger.LogWarning("CV with Id: {CVId} not found.", id);
                    return OperationResult<CV>.Failure("CV not found.");
                }

                _logger.LogInformation("Successfully retrieved CV with Id: {CVId}", id);
                return OperationResult<CV>.Success(cv);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving CV with Id: {CVId}", id);
                return OperationResult<CV>.Failure($"An error occurred: {ex.Message}");
            }
        }

        public async Task<OperationResult<bool>> UpdateAsync(CV cv)
        {
            _logger.LogInformation("Attempting to update CV with Id: {CVId}", cv.Id);

            try
            {
                _database.CVs.Update(cv);
                await _database.SaveChangesAsync();

                _logger.LogInformation("Successfully updated CV with Id: {CVId}", cv.Id);
                return OperationResult<bool>.Success(true, "CV updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating CV with Id: {CVId}", cv.Id);
                return OperationResult<bool>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}
