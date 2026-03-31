namespace Models.RequestModels.Common.Note
{
    public class NoteUpdateRequestModel
    {
        public int? ReferenceType { get; set; }
        public Guid? ReferenceId { get; set; }
        public string? Header { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public Guid? ActionBy { get; set; }
    }
}
