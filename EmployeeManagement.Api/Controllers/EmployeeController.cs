using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
            var userName = User.Identity?.Name; 
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value; 
            var employees = await _employeeService.GetAllEmployeesAsync();  
            return Ok(employees); 
        }
        [Authorize]
        [HttpGet("GetEmployee/{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpPost("AddEmployee")]
        public async Task<ActionResult<Employee>> AddEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _employeeService.AddEmployeeAsync(employee);
            return Ok(); 
        }

        [Authorize]
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            if (employee.Id == null)
            {
                return BadRequest(); 
            }

            await _employeeService.UpdateEmployeeAsync(employee);
            return NoContent(); 
        }

        [Authorize]
        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            await _employeeService.DeleteEmployeeAsync(id);
            return Ok(employee);
        }       

    }
}
