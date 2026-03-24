using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Domain.Masters.Department
{
    [Table("Departments")]
    public class DepartmentEntity :EntityBase
    {
        [Column("Name")]
        public string DeptName { get; set; }

        [Column("Location")]
        public string Location { get; set; }

        [Column("JobCount")]
        public int JobCount { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [Column("CreatedBy")]
        public string CreatedBy { get; set; }

        [Column("ModifiedOn")]
        public DateTime? ModifiedOn { get; set; }

        [Column("ModifiedBy")]
        public string? ModifiedBy { get; set; }
    }
}
