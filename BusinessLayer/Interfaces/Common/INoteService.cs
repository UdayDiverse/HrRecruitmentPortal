using Models;
using Models.RequestModels.Common.Note;
using Models.ResponseModels.Common.Note;

namespace BusinessLayer.Interfaces.Common
{
    public interface INoteService
    {
        Task<NoteReadResponseModel?> GetByIdAsync(Guid id);
        Task<CommonResponseModel> CreateNoteAsync(NoteCreateRequestModel requestModel);
        Task<NoteSearchResponseModel?> SearchNoteAsync(NoteSearchRequestModel requestModel, string? offset, string count);
        Task<CommonResponseModel> UpdateNoteAsync(Guid id, NoteUpdateRequestModel requestModel);
    }
}
