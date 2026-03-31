using AutoMapper;
using BusinessLayer.Interfaces.Common;
using DataAccessLayer.Domain.Common.Note;
using DataAccessLayer.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using Models;
using Models.RequestModels.Common.Note;
using Models.ResponseModels.Common.Note;

namespace BusinessLayer.Services.Common
{
    public class NoteService(INoteRepository noteRepository, IMapper mapper) : INoteService
    {
        public async Task<NoteReadResponseModel?> GetByIdAsync(Guid id)
        {
            NoteEntity? entity = await noteRepository.FindAsync(id);

            if (entity == null)
                return null;

            NoteReadResponseModel response = mapper.Map<NoteReadResponseModel>(entity);
            return response;
        }

        public async Task<CommonResponseModel> CreateNoteAsync(NoteCreateRequestModel requestModel)
        {
            var response = new CommonResponseModel();

            try
            {
                if (requestModel.ReferenceType is not 1 and not 2)
                {
                    response.responseCode = StatusCodes.Status400BadRequest;
                    response.message = "ReferenceType must be 1 (DEPT) or 2 (JOB).";
                    return response;
                }

                var entity = mapper.Map<NoteEntity>(requestModel);
                entity.CreatedOn = DateTime.Now;
                entity.Status = "Active";

                var result = await noteRepository.AddAsync(entity);

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

        public async Task<NoteSearchResponseModel?> SearchNoteAsync(NoteSearchRequestModel requestModel, string? offset, string count)
        {
            if (requestModel.ReferenceType.HasValue && requestModel.ReferenceType.Value is not 1 and not 2)
            {
                return new NoteSearchResponseModel
                {
                    responseCode = StatusCodes.Status400BadRequest,
                    message = "ReferenceType must be 1 (DEPT) or 2 (JOB)."
                };
            }

            NoteSearchResponseEntity entityResponse = await noteRepository.SearchNoteAsync(requestModel, offset, count);
            NoteSearchResponseModel response = mapper.Map<NoteSearchResponseModel>(entityResponse);
            response.Results = mapper.Map<List<NoteReadResponseModel>>(entityResponse.Notes ?? new List<NoteEntity>());

            return response;
        }

        public async Task<CommonResponseModel> UpdateNoteAsync(Guid id, NoteUpdateRequestModel requestModel)
        {
            CommonResponseModel responseModel = new CommonResponseModel();

            try
            {
                NoteEntity? entity = await noteRepository.FindAsync(id);

                if (entity == null)
                {
                    responseModel.responseCode = StatusCodes.Status400BadRequest;
                    responseModel.message = "Data Not Found!";
                    return responseModel;
                }

                if (requestModel.ReferenceType.HasValue)
                {
                    if (requestModel.ReferenceType.Value is not 1 and not 2)
                    {
                        responseModel.responseCode = StatusCodes.Status400BadRequest;
                        responseModel.message = "ReferenceType must be 1 (DEPT) or 2 (JOB).";
                        return responseModel;
                    }

                    entity.ReferenceType = requestModel.ReferenceType.Value;
                }

                if (requestModel.ReferenceId.HasValue)
                    entity.ReferenceId = requestModel.ReferenceId.Value;

                if (!string.IsNullOrWhiteSpace(requestModel.Header))
                    entity.Header = requestModel.Header;

                if (!string.IsNullOrWhiteSpace(requestModel.Description))
                    entity.Description = requestModel.Description;

                if (!string.IsNullOrWhiteSpace(requestModel.Status))
                    entity.Status = requestModel.Status;

                entity.ModifiedOn = DateTime.Now;
                entity.ModifiedBy = requestModel.ActionBy;

                await noteRepository.UpdateAsync(entity);

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
