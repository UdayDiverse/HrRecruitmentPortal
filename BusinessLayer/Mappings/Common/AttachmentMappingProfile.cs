using AutoMapper;
using DataAccessLayer.Domain.Common.Attachments;
using Models.RequestModels.Common.Attachments;
using Models.ResponseModels.Common.Attachments;

namespace BusinessLayer.Mappings.Common
{
    public class AttachmentMappingProfile : Profile
    {
        public AttachmentMappingProfile()
        {
            CreateMap<AttachmentEntity, AttachmentReadResponseModel>().ReverseMap();
            CreateMap<AttachmentCreateRequestModel, AttachmentEntity>();
            CreateMap<AttachmentSearchResponseEntity, AttachmentSearchResponseModel>().ReverseMap();
        }
    }
}
