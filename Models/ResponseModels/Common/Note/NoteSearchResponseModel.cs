using Models.ResponseModels.BaseResponseSetup;

namespace Models.ResponseModels.Common.Note
{
    public class NoteSearchResponseModel : SearchResponseBase<NoteReadResponseModel>
    {
        public List<NoteReadResponseModel> Notes => Results;
    }
}
