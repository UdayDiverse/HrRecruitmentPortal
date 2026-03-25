using Models;
using Models.RequestModels.Masters.Department;
using Models.ResponseModels.Masters.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces.Masters
{
    public interface IDepartmentService
    {
        Task<DepartmentReadResponseModel?> GetByIdAsync(Guid id);

        Task<CommonResponseModel> CreateFullDepartmentAsync(DepartmentCreateRequestModel DeptModel);

        Task<DeptSearchResponseModel?> SearchDeptAsync(DepartmentSearchRequestModel requestModel,  string? offset, string count);
    }
}
