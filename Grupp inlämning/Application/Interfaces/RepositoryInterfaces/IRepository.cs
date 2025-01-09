namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IRepository<T> where T : class
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
