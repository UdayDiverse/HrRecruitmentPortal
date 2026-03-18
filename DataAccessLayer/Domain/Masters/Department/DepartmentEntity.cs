using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Domain.Masters.Department
{
    public class DepartmentEntity : EntityBase
    {
            public string DepartmentName { get; set; }
            public string? DepartmentCode { get; set; }
            public long? LocationId { get; set; }    
            public string? Description { get; set; }
            public string Status { get; set; }
            public bool IsActive { get; set; }        
            public DateTime CreatedAt { get; set; }   
            public DateTime? UpdatedAt { get; set; }
        
    }
}
