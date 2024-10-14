using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Route will be 'api/Employee'
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // This method retrieves all employees
        [HttpGet] // Responds to GET requests at 'api/Employee'
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees); // Return a 200 OK response with the list of employees
        }

        // This method retrieves an employee by id
        [HttpGet("{id}")] // Responds to GET requests at 'api/Employee/{id}'
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound(); // Return 404 Not Found if employee does not exist
            }
            return Ok(employee); // Return the employee if found
        }

        // This method adds a new employee
        [HttpPost] // Responds to POST requests at 'api/Employee'
        public async Task<ActionResult<Employee>> AddEmployee([FromBody] Employee employee)
        {
            await _employeeService.AddEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee); // Return 201 Created with location of the new employee
        }

        // This method updates an existing employee
        [HttpPut("{id}")] // Responds to PUT requests at 'api/Employee/{id}'
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest(); // Return 400 Bad Request if id in the URL does not match the employee's id
            }

            await _employeeService.UpdateEmployeeAsync(employee);
            return NoContent(); // Return 204 No Content if the update is successful
        }

        // This method deletes an employee
        [HttpDelete("{id}")] // Responds to DELETE requests at 'api/Employee/{id}'
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent(); // Return 204 No Content if the deletion is successful
        }
    }
}
