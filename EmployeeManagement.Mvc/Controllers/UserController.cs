using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Mvc.Services;
using System.Threading.Tasks;
using EmployeeManagement.Mvc.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace EmployeeManagement.Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly UserServices _userServices;

        public UserController(UserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task<IActionResult> LogInPage()
        {
            var roleList = await _userServices.GetRoleList();
            var viewModel = new LogInViewModel
            {
                RoleList = roleList
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> LogInPage(LogInViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.RoleList = await _userServices.GetRoleList();
                return View(viewModel);
            }

            var loginResponse = await _userServices.LogInResponse(viewModel.LogInData);
            var responseData = JsonConvert.DeserializeObject<dynamic>(loginResponse);

            if (responseData != null && responseData.token != null)
            {
                if (responseData.message == "Login successful")
                {
                    HttpContext.Session.SetString("LoginMessage", "Login successful");
                    var userClaims = JwtHelper.DecodeJwtToken($"{responseData.token}");
                    string userId = userClaims.ContainsKey("unique_name") ? userClaims["unique_name"] : "Unknown";
                    string userRole = userClaims.ContainsKey("role") ? userClaims["role"] : "Unknown";
                    HttpContext.Session.SetString("UserName", userId);
                    HttpContext.Session.SetString("UserRole", userRole);

                    string expirationUnixTime = userClaims.ContainsKey("exp") ? userClaims["exp"] : "0";
                    var expirationDateTimeUtc = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expirationUnixTime)).UtcDateTime;
                    HttpContext.Session.SetString("TokenExpiry", expirationDateTimeUtc.ToString("o"));
                }

                HttpContext.Session.SetString("JWTToken", (string)responseData.token);
                return RedirectToAction("Index", "Employee");
            }

            //ViewData["ErrorMessage"] = (string)responseData.message;
            ModelState.AddModelError("LogInData.Role", (string)responseData.message);

            viewModel.RoleList = await _userServices.GetRoleList();
            return View(viewModel);
        }

        public async Task<IActionResult> RegisterUser()
        {
            var model = new RegisterViewModel
            {
                RoleList = await _userServices.GetRoleList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.RoleList = await _userServices.GetRoleList();
                return View(model);
            }

            var loginResponse = await _userServices.SignInResponse(model.RegisterModel);
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
                ModelState.AddModelError("RoleList.Role", (string)responseData.message);
            }

            model.RoleList = await _userServices.GetRoleList();
            return RedirectToAction("LogInPage");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LogInPage");
        }
    }
}
