using Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces.Masters
{
    public interface IDepartmentService
    {
        Task<DepartmentResponseModel?> GetByIdAsync(int id);
    }
}
