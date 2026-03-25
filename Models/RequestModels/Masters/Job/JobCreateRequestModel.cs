namespace Models.RequestModels.Masters.Job
{
    public class JobCreateRequestModel
    {
        public Guid DeptId { get; set; }
        public string JobName { get; set; }
        public string? Description { get; set; }
        public int HeadCount { get; set; } = 1;
        public string? JobStage { get; set; }
        public Guid? JobOwnerId { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
