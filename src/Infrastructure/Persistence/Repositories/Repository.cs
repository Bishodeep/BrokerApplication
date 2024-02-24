using clean.Application.Contracts.Persistance;
using clean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace clean.Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        public readonly ApplicationDbContext _dbContext;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        public virtual async Task UpdateAsync(T entity)
        {
            EntityEntry dbEntityEntry = _dbContext.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public virtual async Task DeleteAsync(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();

        }
        public IQueryable<T> GetAllAsync()
        {
            return _dbContext.Set<T>().AsNoTracking().AsQueryable();
        }
        public async Task<T> GetSingleAsync(string id)
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);
        }
    }
}
