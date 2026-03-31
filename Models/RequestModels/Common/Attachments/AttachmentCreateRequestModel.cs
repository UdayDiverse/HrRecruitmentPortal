using Models.Enums;

namespace Models.RequestModels.Common.Attachments
{
    public class AttachmentCreateRequestModel
    {
        public ReferenceType ReferenceType { get; set; }
        public Guid ReferenceId { get; set; }
        public string FilePath { get; set; }
        public string? FileName { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
