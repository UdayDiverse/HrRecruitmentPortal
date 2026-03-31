using AutoMapper;
using DataAccessLayer.Domain.Common.Note;
using Models.RequestModels.Common.Note;
using Models.ResponseModels.Common.Note;

namespace BusinessLayer.Mappings.Common
{
    public class NoteMappingProfile : Profile
    {
        public NoteMappingProfile()
        {
            CreateMap<NoteEntity, NoteReadResponseModel>().ReverseMap();
            CreateMap<NoteCreateRequestModel, NoteEntity>().ReverseMap();
            CreateMap<NoteSearchResponseEntity, NoteSearchResponseModel>().ReverseMap();
        }
    }
}
