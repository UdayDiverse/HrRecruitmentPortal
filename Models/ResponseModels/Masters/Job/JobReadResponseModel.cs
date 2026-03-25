namespace Models.ResponseModels.Masters.Job
{
    public class JobReadResponseModel
    {
        public Guid Id { get; set; }
        public Guid DeptId { get; set; }
        public string JobName { get; set; }
        public string? Description { get; set; }
        public int HeadCount { get; set; }
        public string? Status { get; set; }
        public string? JobStage { get; set; }
        public Guid? JobOwnerId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? JobOwnerName { get; set; }
        public string? CreatedByName { get; set; }
        public Guid? DeptOwnerId { get; set; }
        public string? DeptOwnerName { get; set; }
    }
}
