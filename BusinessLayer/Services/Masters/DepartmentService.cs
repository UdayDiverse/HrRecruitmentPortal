using AutoMapper;
using BusinessLayer.Interfaces.Masters;
using DataAccessLayer.Domain.Masters.Department;
using DataAccessLayer.Interfaces.Masters;
using Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Masters
{
    public class DepartmentService(IDepartmentRepository deaprtmentRepository, IMapper mapper) : IDepartmentService
    {
        public async Task<DepartmentReadResponseModel?> GetByIdAsync(Guid id)
        {
            DepartmentEntity? entity = await deaprtmentRepository.FindAsync(id);

            if (entity == null)
                return null;

            DepartmentReadResponseModel response = mapper.Map<DepartmentReadResponseModel>(entity);

            return response;
        }
    }
}
