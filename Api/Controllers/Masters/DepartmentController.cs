using BusinessLayer.Interfaces.Masters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController(IDepartmentService departmentService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await departmentService.GetByIdAsync(id);

            if (data == null)
                return NotFound("Department not found");

            return Ok(data);
        }
    }
}
