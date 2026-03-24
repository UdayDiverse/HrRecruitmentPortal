using AutoMapper;
using DataAccessLayer.Domain.Masters.Department;
using Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mappings.Masters
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile() { 
        
            CreateMap<DepartmentEntity, DepartmentReadResponseModel>().ReverseMap();

        }
    }
}
