namespace Models.RequestModels.Masters.Department
{
    public class DepartmentCreateRequestModel
    {
        public string Name { get; set; } 
        public string? Location { get; set; }
        public string? Description { get; set; }
        public List<DeptMemberRequestModel> Members { get; set; } = new();
    }

    public class DeptMemberRequestModel
    {
        public Guid UserId { get; set; }
        public Guid MemberTypeId { get; set; } 
    }
}
