using DataAccess.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Domain.Masters.Department
{
    [Table("DeptMembers")]
    public class DepartmentMembersEntity : EntityBase
    {
        [Column("DeptId")]
        public Guid DeptId { get; set; }

        [Column("UserId")]
        public Guid UserId { get; set; }

        [Column("CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [Column("CreatedBy")]
        public string CreatedBy { get; set; }

        public DepartmentEntity? Department { get; set; }
    }
}

