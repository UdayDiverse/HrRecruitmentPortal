using DataAccess.Domain;
using DataAccessLayer.Interfaces.CommonInterface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.CommonRepository
{
    public class CommonRepository<TEntity>(ApplicationDbContext context) : ICommonRepositoryInterface<TEntity>
    where TEntity : EntityBase
    {
        protected readonly ApplicationDbContext _context = context;
        protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        public async Task<TEntity?> FindAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<decimal> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
