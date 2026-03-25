using DataAccessLayer.Domain.Masters.Department;
using DataAccessLayer.Interfaces.Masters;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models.RequestModels.Masters.Department;

namespace DataAccessLayer.Repositories.Masters
{
    public class DepartmentRepository(ApplicationDbContext _context) : IDepartmentRepository
    {
        public async Task<Guid> AddAsync(DepartmentEntity entity)
        {
            _context.DepartmentEntity.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<DepartmentEntity?> FindAsync(Guid id)
        {
            return await _context.DepartmentEntity.Include(x=>x.DepartmentMembers).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<DepartmentSearchResponseEntity> SearchDeptAsync(DepartmentSearchRequestModel requestModel, string? offset, string count)
        {
            var response = new DepartmentSearchResponseEntity();
            try
            {
                var query = _context.DepartmentEntity.AsQueryable();

                if (!string.IsNullOrWhiteSpace(requestModel.DeptName))
                {
                    query = query.Where(t => t.DeptName.ToLower().Equals(requestModel.DeptName.ToLower()));
                }
                if (!string.IsNullOrWhiteSpace(requestModel.Status))
                {
                    query = query.Where(t => t.Status.ToLower().Equals(requestModel.Status.ToLower()));
                }

                response.Paging.Total = await query.AsNoTracking().CountAsync();

                // Try parsing offset and count
                int parsedOffset = int.TryParse(offset, out int tempOffset) ? tempOffset : 0;
                int parsedCount = int.TryParse(count, out int tempCount) ? tempCount : 10; // Default count if parsing fails

                if (parsedCount == 0)
                {
                    response.Departments = await query.ToListAsync();

                    // Set pagination values
                    response.Paging.TotalPages = 0;
                    response.Paging.CurrentPage = 0;
                    response.Paging.Results = 0;
                    response.Paging.NextOffset = null;
                    response.Paging.NextPage = null;
                    response.Paging.PrevPage = null;
                }
                else
                {
                    response.Departments = await query.Skip(parsedOffset).Take(parsedCount).ToListAsync();

                    response.Paging.TotalPages = (int)Math.Ceiling((double)response.Paging.Total / parsedCount);
                    response.Paging.CurrentPage = (parsedOffset / parsedCount) + 1;
                    response.Paging.Results = response.Departments.Count();

                    int nextOffset = parsedOffset + parsedCount;
                    response.Paging.NextOffset = (response.Paging.Total > nextOffset) ? nextOffset.ToString() : null;

                    response.Paging.NextPage = response.Paging.NextOffset != null
                        ? $"?offset={nextOffset}&count={parsedCount}"
                        : null;

                    response.Paging.PrevPage = response.Paging.CurrentPage > 1
                        ? $"?offset={(parsedOffset - parsedCount)}&count={parsedCount}"
                        : null;

                    // Fetch distinct filter values
                    response.Filters = new Dictionary<string, List<string>>
            {
                { "DeptName", await _context.DepartmentEntity.Select(a => a.DeptName).Distinct().ToListAsync() },
                { "Status", await _context.DepartmentEntity.Select(a => a.Status).Distinct().ToListAsync() },
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

        public Task<DepartmentEntity> UpdateAsync(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
