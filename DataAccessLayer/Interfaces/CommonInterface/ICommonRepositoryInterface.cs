using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces.CommonInterface
{
    public interface ICommonRepositoryInterface<TEntity>
        where TEntity : EntityBase
    {
        Task<TEntity?> FindAsync(int id);
        Task<decimal> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
    }
}
