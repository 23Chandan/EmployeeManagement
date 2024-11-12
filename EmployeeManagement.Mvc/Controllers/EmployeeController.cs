using EmployeeManagement.Mvc.Models;
using EmployeeManagement.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Mvc.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeServices _employeeService;

        public EmployeeController(EmployeeServices employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDTo employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.CreateEmployeeAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
    }
}
