using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Mvc.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/AccessDenied")]
        public IActionResult AccessDenied()
        {
            ViewBag.ErrorMessage = "You do not have permission to access this resource.";
            return View();
        }

        [Route("Error/GeneralError")]
        public IActionResult GeneralError()
        {
            ViewBag.ErrorMessage = "An unexpected error occurred. Please try again later.";
            return View();
        }
    }
}
