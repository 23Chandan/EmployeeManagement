using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentServices _services;
        public DepartmentController(IDepartmentServices departmentServices)
        {
            _services = departmentServices;
        }
        [Route("getDepartment")]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartment()
        {
            var departmentList = await _services.GetAllDepartment();
            return Ok(departmentList);
        }
    }
}
