using AutoMapper;
using BusinessLayer.Interfaces.Masters;
using DataAccessLayer.Domain.Masters.Department;
using DataAccessLayer.Interfaces.Masters;
using Microsoft.AspNetCore.Http;
using Models;
using Models.RequestModels.Masters.Department;
using Models.ResponseModels.Masters.Department;

namespace BusinessLayer.Services.Masters
{
    public class DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper) : IDepartmentService
    {
        public async Task<DepartmentReadResponseModel?> GetByIdAsync(Guid id)
        {
            DepartmentEntity? entity = await departmentRepository.FindAsync(id);

            if (entity == null)
                return null;

            DepartmentReadResponseModel response = mapper.Map<DepartmentReadResponseModel>(entity);

            return response;
        }

        public async Task<CommonResponseModel> CreateFullDepartmentAsync(DepartmentCreateRequestModel DeptModel)
        {
            var response = new CommonResponseModel();

            try
            {
                var departmentEntity = mapper.Map<DepartmentEntity>(DeptModel);
                departmentEntity.CreatedOn = DateTime.Now;

                if (departmentEntity.DepartmentMembers != null)
                {
                    foreach (var member in departmentEntity.DepartmentMembers)
                    {
                        member.CreatedOn = DateTime.Now;
                        member.CreatedBy = "System_User";                       
                    }
                }

                var result = await departmentRepository.AddAsync(departmentEntity);

                response.responseCode = 200;
                response.message = "Successfully Created";
                response.Id = result;

            }
            catch (Exception ex)
            {
                response.responseCode = StatusCodes.Status400BadRequest;
                response.message += ex.Message;
            }

            return response;
        }

        public async Task<DeptSearchResponseModel?> SearchDeptAsync(DepartmentSearchRequestModel requestModel, string? offset, string count)
        {
            DepartmentSearchResponseEntity entityResponse = await departmentRepository.SearchDeptAsync(requestModel, offset, count);
            DeptSearchResponseModel response = mapper.Map<DeptSearchResponseModel>(entityResponse);

            return response;
        }
    }
}
