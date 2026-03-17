using AutoMapper;
using BusinessLayer.Interfaces.Masters;
using DataAccessLayer.Interfaces.Masters;
using Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Masters
{
    internal class DepartmentService(IDepartmentRepository deaprtmentRepository, IMapper mapper) : IDepartmentService
    {
        public async Task<DepartmentResponseModel?> GetByIdAsync(int id)
        {
            var entity = await deaprtmentRepository.FindAsync(id);

            if (entity == null)
                return null;

            return new DepartmentResponseModel
            {
               
            };

        }
    }
}
