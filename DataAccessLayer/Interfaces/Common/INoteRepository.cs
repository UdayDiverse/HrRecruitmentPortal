using DataAccessLayer.Domain.Common.Note;
using DataAccessLayer.Interfaces.CommonInterface;
using Models.RequestModels.Common.Note;

namespace DataAccessLayer.Interfaces.Common
{
    public interface INoteRepository : ICommonRepositoryInterface<NoteEntity>
    {
        Task<NoteSearchResponseEntity> SearchNoteAsync(NoteSearchRequestModel requestModel, string? offset, string count);
    }
}
