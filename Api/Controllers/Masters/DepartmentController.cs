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
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await departmentService.GetByIdAsync(id);

            if (data == null)
                return BadRequest(new { code = 400, message = "Data Not Found!" });

            return Ok(data);
        }
    }
}
