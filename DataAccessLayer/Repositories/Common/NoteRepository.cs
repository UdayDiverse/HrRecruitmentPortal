using DataAccessLayer.Domain.Common.Note;
using DataAccessLayer.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models.RequestModels.Common.Note;

namespace DataAccessLayer.Repositories.Common
{
    public class NoteRepository(ApplicationDbContext _context) : INoteRepository
    {
        public async Task<Guid> AddAsync(NoteEntity entity)
        {
            _context.NoteEntity.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<NoteEntity?> FindAsync(Guid id)
        {
            return await _context.NoteEntity.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<NoteSearchResponseEntity> SearchNoteAsync(NoteSearchRequestModel requestModel, string? offset, string count)
        {
            var response = new NoteSearchResponseEntity();
            try
            {
                var query = _context.NoteEntity.AsQueryable();

                if (requestModel.ReferenceType.HasValue)
                {
                    query = query.Where(x => x.ReferenceType == requestModel.ReferenceType.Value);
                }

                if (requestModel.ReferenceId.HasValue)
                {
                    query = query.Where(x => x.ReferenceId == requestModel.ReferenceId.Value);
                }

                if (!string.IsNullOrWhiteSpace(requestModel.Header))
                {
                    query = query.Where(x => x.Header.ToLower().Contains(requestModel.Header.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(requestModel.Status))
                {
                    query = query.Where(x => x.Status != null && x.Status.ToLower().Equals(requestModel.Status.ToLower()));
                }

                response.Paging.Total = await query.AsNoTracking().CountAsync();

                int parsedOffset = int.TryParse(offset, out int tempOffset) ? tempOffset : 0;
                int parsedCount = int.TryParse(count, out int tempCount) ? tempCount : 10;

                if (parsedCount == 0)
                {
                    response.Notes = await query.OrderByDescending(x => x.CreatedOn).ToListAsync();

                    response.Paging.TotalPages = 0;
                    response.Paging.CurrentPage = 0;
                    response.Paging.Results = 0;
                    response.Paging.NextOffset = null;
                    response.Paging.NextPage = null;
                    response.Paging.PrevPage = null;
                }
                else
                {
                    response.Notes = await query
                        .OrderByDescending(x => x.CreatedOn)
                        .Skip(parsedOffset)
                        .Take(parsedCount)
                        .ToListAsync();

                    response.Paging.TotalPages = (int)Math.Ceiling((double)response.Paging.Total / parsedCount);
                    response.Paging.CurrentPage = (parsedOffset / parsedCount) + 1;
                    response.Paging.Results = response.Notes.Count();

                    int nextOffset = parsedOffset + parsedCount;
                    response.Paging.NextOffset = (response.Paging.Total > nextOffset) ? nextOffset.ToString() : null;

                    response.Paging.NextPage = response.Paging.NextOffset != null
                        ? $"?offset={nextOffset}&count={parsedCount}"
                        : null;

                    response.Paging.PrevPage = response.Paging.CurrentPage > 1
                        ? $"?offset={(parsedOffset - parsedCount)}&count={parsedCount}"
                        : null;

                    response.Filters = new Dictionary<string, List<string>>
                    {
                        { "ReferenceType", await _context.NoteEntity.Select(a => a.ReferenceType.ToString()).Distinct().ToListAsync() },
                        { "Status", await _context.NoteEntity.Where(a => a.Status != null).Select(a => a.Status!).Distinct().ToListAsync() }
                    };
                }

                response.responseCode = StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                response.responseCode = StatusCodes.Status400BadRequest;
                response.message = ex.Message;
            }

            return response;
        }

        public async Task<NoteEntity> UpdateAsync(NoteEntity entity)
        {
            _context.NoteEntity.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
