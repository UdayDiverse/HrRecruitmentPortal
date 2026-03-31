using AutoMapper;
using BusinessLayer.Interfaces.Common;
using DataAccessLayer.Domain.Common.Attachments;
using DataAccessLayer.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using Models;
using Models.Enums;
using Models.RequestModels.Common.Attachments;
using Models.ResponseModels.Common.Attachments;

namespace BusinessLayer.Services.Common
{
    public class AttachmentService(IAttachmentRepository attachmentRepository, IReferenceValidationRepository referenceValidationRepository, IMapper mapper) : IAttachmentService
    {
        private static bool IsValidReferenceType(ReferenceType referenceType)
            => Enum.IsDefined(typeof(ReferenceType), referenceType);

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
                if (!IsValidReferenceType(requestModel.ReferenceType))
                {
                    response.responseCode = StatusCodes.Status400BadRequest;
                    response.message = "Invalid ReferenceType. Allowed values: Department or Job.";
                    return response;
                }

                var referenceType = requestModel.ReferenceType;
                bool isValidReference = await referenceValidationRepository.IsReferenceValidAsync(referenceType, requestModel.ReferenceId);
                if (!isValidReference)
                {
                    response.responseCode = StatusCodes.Status400BadRequest;
                    response.message = $"ReferenceId not found for {referenceType}.";
                    return response;
                }

                var entity = mapper.Map<AttachmentEntity>(requestModel);
                entity.ReferenceType = (int)requestModel.ReferenceType;
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
            if (requestModel.ReferenceType.HasValue && !IsValidReferenceType(requestModel.ReferenceType.Value))
            {
                return new AttachmentSearchResponseModel
                {
                    responseCode = StatusCodes.Status400BadRequest,
                    message = "Invalid ReferenceType. Allowed values: Department or Job."
                };
            }

            if (requestModel.ReferenceType.HasValue && requestModel.ReferenceId.HasValue)
            {
                var referenceType = requestModel.ReferenceType.Value;
                bool isValidReference = await referenceValidationRepository.IsReferenceValidAsync(referenceType, requestModel.ReferenceId.Value);
                if (!isValidReference)
                {
                    return new AttachmentSearchResponseModel
                    {
                        responseCode = StatusCodes.Status400BadRequest,
                        message = $"ReferenceId not found for {referenceType}."
                    };
                }
            }

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
