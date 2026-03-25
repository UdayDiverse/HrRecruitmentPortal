using AutoMapper;
using BusinessLayer.Interfaces.Masters;
using DataAccessLayer.Domain.Masters.Job;
using DataAccessLayer.Interfaces.Masters;
using Microsoft.AspNetCore.Http;
using Models;
using Models.RequestModels.Masters.Job;
using Models.ResponseModels.Masters.Job;

namespace BusinessLayer.Services.Masters
{
    public class JobService(IJobRepository jobRepository, IUserRepository userRepository, IMapper mapper) : IJobService
    {
        public async Task<JobReadResponseModel?> GetByIdAsync(Guid id)
        {
            JobEntity? entity = await jobRepository.FindAsync(id);

            if (entity == null)
                return null;

            JobReadResponseModel response = mapper.Map<JobReadResponseModel>(entity);
            await EnrichNamesAsync(new List<JobReadResponseModel> { response });
            return response;
        }

        public async Task<CommonResponseModel> CreateJobAsync(JobCreateRequestModel requestModel)
        {
            var response = new CommonResponseModel();

            try
            {
                var entity = mapper.Map<JobEntity>(requestModel);
                entity.CreatedOn = DateTime.Now;
                entity.Status = "Active";
                if (entity.HeadCount <= 0)
                    entity.HeadCount = 1;

                var result = await jobRepository.AddAsync(entity);

                response.responseCode = StatusCodes.Status200OK;
                response.message = "Successfully Created";
                response.Id = result;
            }
            catch (Exception ex)
            {
                response.responseCode = StatusCodes.Status400BadRequest;
                response.message = ex.Message;
            }

            return response;
        }

        public async Task<JobSearchResponseModel?> SearchJobAsync(JobSearchRequestModel requestModel, string? offset, string count)
        {
            JobSearchResponseEntity entityResponse = await jobRepository.SearchJobAsync(requestModel, offset, count);
            JobSearchResponseModel response = mapper.Map<JobSearchResponseModel>(entityResponse);
            await EnrichNamesAsync(response.Jobs);
            return response;
        }

        public async Task<CommonResponseModel> UpdateJobAsync(Guid id, JobUpdateRequestModel requestModel)
        {
            CommonResponseModel responseModel = new CommonResponseModel();

            try
            {
                JobEntity? entity = await jobRepository.FindAsync(id);

                if (entity == null)
                {
                    responseModel.responseCode = StatusCodes.Status400BadRequest;
                    responseModel.message = "Data Not Found!";
                    return responseModel;
                }

                if (requestModel.DeptId.HasValue)
                    entity.DeptId = requestModel.DeptId.Value;

                if (!string.IsNullOrWhiteSpace(requestModel.JobName))
                    entity.JobName = requestModel.JobName;

                if (!string.IsNullOrWhiteSpace(requestModel.Description))
                    entity.Description = requestModel.Description;

                if (requestModel.HeadCount.HasValue && requestModel.HeadCount.Value > 0)
                    entity.HeadCount = requestModel.HeadCount.Value;

                if (!string.IsNullOrWhiteSpace(requestModel.JobStage))
                    entity.JobStage = requestModel.JobStage;

                if (!string.IsNullOrWhiteSpace(requestModel.Status))
                    entity.Status = requestModel.Status;

                if (requestModel.JobOwnerId.HasValue)
                    entity.JobOwnerId = requestModel.JobOwnerId;

                entity.ModifiedOn = DateTime.Now;
                entity.ModifiedBy = requestModel.ActionBy;

                await jobRepository.UpdateAsync(entity);

                responseModel.responseCode = StatusCodes.Status200OK;
                responseModel.message = "Updated Successfully!";
                responseModel.Id = entity.Id;
            }
            catch (Exception ex)
            {
                responseModel.responseCode = StatusCodes.Status400BadRequest;
                responseModel.message = ex.Message;
            }

            return responseModel;
        }

        private async Task EnrichNamesAsync(IEnumerable<JobReadResponseModel> jobs)
        {
            var list = jobs.ToList();
            if (list.Count == 0)
                return;

            var userIds = list
                .SelectMany(x => new[] { x.CreatedBy, x.JobOwnerId, x.DeptOwnerId })
                .Where(x => x.HasValue)
                .Select(x => x!.Value)
                .Distinct()
                .ToList();

            var names = await userRepository.GetUserNamesByIdsAsync(userIds);

            foreach (var item in list)
            {
                if (item.CreatedBy.HasValue && names.TryGetValue(item.CreatedBy.Value, out var createdByName))
                    item.CreatedByName = createdByName;

                if (item.JobOwnerId.HasValue && names.TryGetValue(item.JobOwnerId.Value, out var ownerName))
                    item.JobOwnerName = ownerName;

                if (item.DeptOwnerId.HasValue && names.TryGetValue(item.DeptOwnerId.Value, out var deptOwnerName))
                    item.DeptOwnerName = deptOwnerName;
            }
        }
    }
}
