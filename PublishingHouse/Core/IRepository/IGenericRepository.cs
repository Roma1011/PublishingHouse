namespace PublishingHouse.Core.IRepository
{
    public interface IGenericRepository<T> where T : class,new()
    {
        public Task<IQueryable<T?>> GetAll();
        Task<T?> GetById(long id);
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<int> Save();
    }
}
