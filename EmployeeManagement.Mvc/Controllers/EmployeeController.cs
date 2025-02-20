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
            var loginMessage = HttpContext.Session.GetString("LoginMessage");


            if (string.IsNullOrEmpty(loginMessage))
            {
                return RedirectToAction("LogInPage", "User");
            }

            ViewData["LoginMessage"] = loginMessage;
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDTo employee)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeService.CreateEmployeeAsync(employee);

                if (!result.IsSuccess)
                {
                    TempData["ErrorMessage"] = result.ErrorMessage;
                    TempData["ErrorCode"] = result.StatusCode;
                    return RedirectToAction("ErrorPage");
                }

                return RedirectToAction("Index");
            }

            return View(employee);
        }

        public IActionResult ErrorPage()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.ErrorCode = TempData["ErrorCode"];
            return View();
        }
        public async Task<IActionResult> GetEmployeeById(int Id)
        {
            var data = await _employeeService.GetEmployeesAByIdAsync(Id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(EmployeeDTo data)
        {
            var result = await _employeeService.UpdateEmployee(data);
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.ErrorMessage;
                TempData["ErrorCode"] = result.StatusCode;
                return RedirectToAction("ErrorPage");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteEmployee(int Id)
        {
            var result = await _employeeService.DeleteEmployee(Id);
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.ErrorMessage;
                TempData["ErrorCode"] = result.StatusCode;
                return RedirectToAction("ErrorPage");
            }
            return RedirectToAction("Index");
        }
    }
}
