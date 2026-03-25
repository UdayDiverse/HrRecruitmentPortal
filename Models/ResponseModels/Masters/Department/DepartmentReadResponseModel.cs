using Models.RequestModels.Masters.Department;

namespace Models.ResponseModels.Masters.Department
{
    public class DepartmentReadResponseModel
    {
        public Guid id { get; set; }
        public string DeptName { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public Guid? OwnerId { get; set; }
        public string? DeptOwnerName { get; set; }
        public Guid? CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public int? JobCount { get; set; }
        public List<DeptMemberRequestModel> DepartmentMembers { get; set; } = new();
    }
}
