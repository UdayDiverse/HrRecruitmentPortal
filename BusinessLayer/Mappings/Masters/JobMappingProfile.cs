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
            CreateMap<JobEntity, JobReadResponseModel>().ReverseMap();
            CreateMap<JobCreateRequestModel, JobEntity>()
                .ForMember(dest => dest.JobMembers, opt => opt.MapFrom(src => src.JobMembers));
            CreateMap<JobMemberRequestModel, JobMembersEntity>().ReverseMap();
            CreateMap<JobSearchResponseEntity, JobSearchResponseModel>().ReverseMap();
        }
    }
}
