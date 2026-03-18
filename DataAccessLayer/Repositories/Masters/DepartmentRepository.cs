using DataAccessLayer.Domain.Masters.Department;
using DataAccessLayer.Interfaces.Masters;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Masters
{
    public class DepartmentRepository(ApplicationDbContext _context) : IDepartmentRepository
    {
        public Task<decimal> AddAsync(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<DepartmentEntity?> FindAsync(int id)
        {
           return await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<DepartmentEntity>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public Task<DepartmentEntity> UpdateAsync(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
