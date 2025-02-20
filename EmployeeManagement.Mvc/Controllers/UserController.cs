using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Mvc.Services;
using System.Threading.Tasks;
using EmployeeManagement.Mvc.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly UserServices _userServices;

        public UserController(UserServices userServices)
        {
            _userServices = userServices;
        }

        public IActionResult LogInPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogInPage(LogInApplicationUserDto formData)
        {
            if (!ModelState.IsValid)
            {
                return View(formData);
            }
            var loginResponse = await _userServices.LogInResponse(formData);
            var responseData = JsonConvert.DeserializeObject<dynamic>(loginResponse);
            if (responseData != null && responseData.token != null)
            {
                if(responseData.message == "Login successful")
                {
                    HttpContext.Session.SetString("LoginMessage", "Login successful");
                }
                HttpContext.Session.SetString("JWTToken", (string)responseData.token);
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                ViewData["ErrorMessage"] = "UserName or password incorrect";
            }
            return View(formData);
        }
        public IActionResult RegisertUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisertUser(SignInApplicationUserDto formData)
        {
            if (!ModelState.IsValid)
            {
                return View(formData);
            }
            var loginResponse = await _userServices.SignInResponse(formData);
            var responseData = JsonConvert.DeserializeObject<dynamic>(loginResponse);
            if (responseData != null && responseData.token != null)
            {
                if (responseData.message == "Login successful")
                {
                    HttpContext.Session.SetString("LoginMessage", "Login successful");
                }
                HttpContext.Session.SetString("JWTToken", (string)responseData.token);
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                ViewData["ErrorMessage"] = "UserName or password incorrect";
            }
            return View(formData);
        }
    }
}
