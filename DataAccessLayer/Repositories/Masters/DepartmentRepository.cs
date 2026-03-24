using DataAccessLayer.Domain.Masters.Department;
using DataAccessLayer.Interfaces.Masters;
using DataAccessLayer.Repositories.CommonRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Masters
{
    public class DepartmentRepository(ApplicationDbContext context)
     : CommonRepository<DepartmentEntity>(context), IDepartmentRepository;
}
