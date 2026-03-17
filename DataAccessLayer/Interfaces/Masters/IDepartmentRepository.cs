using DataAccessLayer.Domain.Masters.Department;
using DataAccessLayer.Interfaces.CommonInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces.Masters
{
    public interface IDepartmentRepository : ICommonRepositoryInterface<DepartmentEntity>
    {

    }
}
