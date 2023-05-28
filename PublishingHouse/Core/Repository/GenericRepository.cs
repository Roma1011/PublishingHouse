using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using PublishingHouse.Core.IRepository;
using PublishingHouse.Data.Context;

namespace PublishingHouse.Core.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class,new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T?> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context= context;
            _dbSet = context.Set<T>();
        }
        public virtual async Task<bool>Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await Save()!=0;
        }

        public virtual async Task<IQueryable<T?>> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public virtual async Task<bool> Delete(T entity)
        {
            _dbSet.Remove(entity);
            return true;
        }

        public virtual async Task<T?> GetById(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
        public virtual async Task<bool> Update(T entity)
        {

                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                await Save();
                return true;
        }
    }
}
