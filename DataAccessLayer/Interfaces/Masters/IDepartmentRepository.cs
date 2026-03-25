using DataAccessLayer.Domain.Masters.Department;
using DataAccessLayer.Interfaces.CommonInterface;
using Models.RequestModels.Masters.Department;

namespace DataAccessLayer.Interfaces.Masters
{
    public interface IDepartmentRepository : ICommonRepositoryInterface<DepartmentEntity>
    {
        Task<DepartmentSearchResponseEntity> SearchDeptAsync(DepartmentSearchRequestModel requestModel, string? offset, string count);
    }
}
