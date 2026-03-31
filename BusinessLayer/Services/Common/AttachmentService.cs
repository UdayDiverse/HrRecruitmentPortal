using AutoMapper;
using BusinessLayer.Interfaces.Common;
using DataAccessLayer.Domain.Common.Attachments;
using DataAccessLayer.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using Models;
using Models.RequestModels.Common.Attachments;
using Models.ResponseModels.Common.Attachments;

namespace BusinessLayer.Services.Common
{
    public class AttachmentService(IAttachmentRepository attachmentRepository, IMapper mapper) : IAttachmentService
    {
        public async Task<AttachmentReadResponseModel?> GetByIdAsync(Guid id)
        {
            AttachmentEntity? entity = await attachmentRepository.FindAsync(id);

            if (entity == null)
                return null;

            AttachmentReadResponseModel response = mapper.Map<AttachmentReadResponseModel>(entity);
            return response;
        }

        public async Task<CommonResponseModel> CreateAttachmentAsync(AttachmentCreateRequestModel requestModel)
        {
            var response = new CommonResponseModel();

            try
            {
                var entity = mapper.Map<AttachmentEntity>(requestModel);
                entity.CreatedOn = DateTime.Now;
                entity.Status = "Active";

                var result = await attachmentRepository.AddAsync(entity);

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

        public async Task<AttachmentSearchResponseModel?> SearchAttachmentAsync(AttachmentSearchRequestModel requestModel, string? offset, string count)
        {
            AttachmentSearchResponseEntity entityResponse = await attachmentRepository.SearchAttachmentAsync(requestModel, offset, count);
            AttachmentSearchResponseModel response = mapper.Map<AttachmentSearchResponseModel>(entityResponse);

            return response;
        }

        public async Task<CommonResponseModel> UpdateAttachmentAsync(Guid id, AttachmentUpdateRequestModel requestModel)
        {
            CommonResponseModel responseModel = new CommonResponseModel();

            try
            {
                AttachmentEntity? entity = await attachmentRepository.FindAsync(id);

                if (entity == null)
                {
                    responseModel.responseCode = StatusCodes.Status400BadRequest;
                    responseModel.message = "Data Not Found!";
                    return responseModel;
                }

                if (!string.IsNullOrWhiteSpace(requestModel.FilePath))
                    entity.FilePath = requestModel.FilePath;

                if (!string.IsNullOrWhiteSpace(requestModel.FileName))
                    entity.FileName = requestModel.FileName;

                if (!string.IsNullOrWhiteSpace(requestModel.Status))
                    entity.Status = requestModel.Status;

                entity.ModifiedOn = DateTime.Now;
                entity.ModifiedBy = requestModel.ActionBy;

                await attachmentRepository.UpdateAsync(entity);

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
    }
}
