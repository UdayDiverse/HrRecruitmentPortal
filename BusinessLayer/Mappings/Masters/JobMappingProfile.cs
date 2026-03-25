using AutoMapper;
using DataAccessLayer.Domain.Masters.Job;
using Models.RequestModels.Masters.Job;
using Models.ResponseModels.Masters.Job;

namespace BusinessLayer.Mappings.Masters
{
    public class JobMappingProfile : Profile
    {
        public JobMappingProfile()
        {
            CreateMap<JobEntity, JobReadResponseModel>()
                .ForMember(dest => dest.DeptOwnerId, opt => opt.MapFrom(src => src.Department != null ? src.Department.OwnerId : null));
            CreateMap<JobCreateRequestModel, JobEntity>().ReverseMap();
            CreateMap<JobSearchResponseEntity, JobSearchResponseModel>()
                .ForMember(dest => dest.Results, opt => opt.MapFrom(src => src.Jobs));
        }
    }
}
