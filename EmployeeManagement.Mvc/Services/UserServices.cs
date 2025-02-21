using EmployeeManagement.Core.Entities.ApplicationUserEntities;
using EmployeeManagement.Mvc.Models;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeManagement.Mvc.Services
{
    public class UserServices
    {
        private readonly HttpClient _httpClient;
        public UserServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> LogInResponse(LogInApplicationUserDto modalData)
        {
            var content = new StringContent(JsonConvert.SerializeObject(modalData), Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync("http://localhost:5008/api/ApplicationUser/Login", content);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();            
            return responseData;

        }
        public async Task<string> SignInResponse(SignInApplicationUserDto modalData)
        {
            var content = new StringContent(JsonConvert.SerializeObject(modalData), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:5008/api/ApplicationUser/Register", content);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();

            return responseData;

        }

    }
}
