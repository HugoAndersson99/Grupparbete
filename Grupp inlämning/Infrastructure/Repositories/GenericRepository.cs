using Application.Interfaces.RepositoryInterfaces;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly Database _database;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(Database database)
        {
            _database = database;
            _dbSet = _database.Set<T>();
        }

        public T GetById(Guid id)
        {
            try
            {
                return _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the entity with ID {id}.", ex);
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return _dbSet.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all entities.", ex);
            }
        }

        public async Task Add(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                await _database.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the entity.", ex);
            }
        }


        public void Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                _database.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the entity.", ex);
            }
        }

        public void Delete(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                _database.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the entity.", ex);
            }
        }
    }
}
