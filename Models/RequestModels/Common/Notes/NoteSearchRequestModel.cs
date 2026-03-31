namespace Models.RequestModels.Common.Notes
{
    public class NoteSearchRequestModel
    {
        public int? ReferenceType { get; set; }
        public Guid? ReferenceId { get; set; }
        public string? Header { get; set; }
        public string? Status { get; set; }
    }
}
