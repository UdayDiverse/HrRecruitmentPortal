using AutoMapper;
using DataAccessLayer.Domain.Masters.Department;
using Models.RequestModels.Masters.Department;
using Models.ResponseModels.Masters.Department;

namespace BusinessLayer.Mappings.Masters
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile() { 
        
            CreateMap<DepartmentEntity, DepartmentReadResponseModel>().ReverseMap();

            CreateMap<DepartmentCreateRequestModel, DepartmentEntity>();

            CreateMap<DeptMemberRequestModel, DepartmentMembersEntity>();

        }
    }
}
