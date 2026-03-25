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
    public class DepartmentService(IDepartmentRepository departmentRepository, IUserRepository userRepository, IMapper mapper) : IDepartmentService
    {
        public async Task<List<DepartmentReadResponseModel>> GetAllAsync()
        {
            List<DepartmentEntity> entities = await departmentRepository.GetAllWithMembersAsync();
            var response = mapper.Map<List<DepartmentReadResponseModel>>(entities);
            await EnrichDepartmentNamesAsync(response);
            return response;
        }

        public async Task<DepartmentReadResponseModel?> GetByIdAsync(Guid id)
        {
            DepartmentEntity? entity = await departmentRepository.FindAsync(id);

            if (entity == null)
                return null;

            DepartmentReadResponseModel response = mapper.Map<DepartmentReadResponseModel>(entity);
            await EnrichDepartmentNamesAsync(new List<DepartmentReadResponseModel> { response });

            return response;
        }

        public async Task<CommonResponseModel> CreateFullDepartmentAsync(DepartmentCreateRequestModel DeptModel)
        {
            var response = new CommonResponseModel();

            try
            {
                var departmentEntity = mapper.Map<DepartmentEntity>(DeptModel);
                departmentEntity.CreatedOn = DateTime.Now;
                departmentEntity.Status = "Active";

                if (departmentEntity.DepartmentMembers != null)
                {
                    foreach (var member in departmentEntity.DepartmentMembers)
                    {
                        member.CreatedOn = DateTime.Now;
                        member.CreatedBy = DeptModel.CreatedBy;
                        member.Status = "Active";
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
            await EnrichDepartmentNamesAsync(response.Departments);

            return response;
        }

        public async Task<CommonResponseModel> UpdateDepartmentAsync(Guid id, DepartmentUpdateRequestModel requestModel)
        {
            CommonResponseModel responseModel = new CommonResponseModel();

            try
            {
                DepartmentEntity? entity = await departmentRepository.FindAsync(id);

                if (entity == null)
                {
                    responseModel.responseCode = StatusCodes.Status400BadRequest;
                    responseModel.message = "Data Not Found!";
                    return responseModel;
                }

                if (!string.IsNullOrWhiteSpace(requestModel.DeptName))
                    entity.DeptName = requestModel.DeptName;

                if (!string.IsNullOrWhiteSpace(requestModel.Location))
                    entity.Location = requestModel.Location;

                if (!string.IsNullOrWhiteSpace(requestModel.Description))
                    entity.Description = requestModel.Description;

                if (!string.IsNullOrWhiteSpace(requestModel.Status))
                    entity.Status = requestModel.Status;

                entity.OwnerId = requestModel.OwnerId;
                entity.ModifiedOn = DateTime.Now;
                entity.ModifiedBy = requestModel.ActionBy;

                var members = requestModel.DepartmentMembers
                    .Select(member => new DepartmentMembersEntity
                    {
                        DeptId = entity.Id,
                        UserId = member.UserId,
                        CreatedOn = DateTime.Now
                    })
                    .ToList();

                await departmentRepository.ReplaceMembersAsync(entity.Id, members);

                await departmentRepository.UpdateAsync(entity);

                responseModel.responseCode = StatusCodes.Status200OK;
                responseModel.message = "Updated Successfully!";
                responseModel.Id = entity.Id;
            }
            catch (Exception ex)
            {
                responseModel.responseCode = StatusCodes.Status400BadRequest;
                responseModel.message = ex.Message;
            }

            return responseModel;
        }

        private async Task EnrichDepartmentNamesAsync(IEnumerable<DepartmentReadResponseModel> departments)
        {
            var list = departments.ToList();
            if (list.Count == 0)
                return;

            var userIds = list
                .SelectMany(x => new[] { x.OwnerId, x.CreatedBy })
                .Where(x => x.HasValue)
                .Select(x => x!.Value)
                .Distinct()
                .ToList();

            var names = await userRepository.GetUserNamesByIdsAsync(userIds);

            foreach (var item in list)
            {
                if (item.OwnerId.HasValue && names.TryGetValue(item.OwnerId.Value, out var ownerName))
                    item.DeptOwnerName = ownerName;

                if (item.CreatedBy.HasValue && names.TryGetValue(item.CreatedBy.Value, out var createdByName))
                    item.CreatedByName = createdByName;
            }
        }
    }
}
