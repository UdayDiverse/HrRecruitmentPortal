using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ResponseModels
{
    public class DepartmentResponseModel
    {
        public long id { get; set; }
        public string departmentName { get; set; }
        public string? departmentCode { get; set; }
    }
}
