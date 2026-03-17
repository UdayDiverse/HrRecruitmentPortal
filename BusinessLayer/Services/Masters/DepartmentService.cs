using AutoMapper;
using BusinessLayer.Interfaces.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Masters
{
    internal class DepartmentService(IDepartmentRepository deaprtmentRepository, IMapper mapper) : IDepartmentService
    {
    }
}
