using EmployeeManagement.Mvc.Models;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeManagement.Mvc.Services
{
    public class EmployeeServices
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EmployeeServices(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        //public async Task<IEnumerable<EmployeeDTo>> GetAllEmployeesAsync()
        //{
        //    var response = await _httpClient.GetAsync("http://localhost:5008/api/employee");
        //    response.EnsureSuccessStatusCode();

        //    var jsonResponse = await response.Content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject<IEnumerable<EmployeeDTo>>(jsonResponse);
        //}
        public async Task<IEnumerable<EmployeeDTo>> GetAllEmployeesAsync()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("http://localhost:5008/api/employee");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<EmployeeDTo>>(jsonResponse);
        }
        public async Task<EmployeeDTo> GetEmployeesAByIdAsync(int id)
        {
            try
            {
                var token = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");
                if (string.IsNullOrEmpty(token))
                {
                    throw new UnauthorizedAccessException("User is not authenticated");
                }

                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync($"http://localhost:5008/api/employee/GetEmployee/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API Error: {response.StatusCode} - {response.ReasonPhrase} | {errorMessage}");
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EmployeeDTo>(jsonResponse);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Network error while calling the API.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }


        public async Task<(bool IsSuccess, string ErrorMessage, int StatusCode)> CreateEmployeeAsync(EmployeeDTo employee)
        {
            try
            {
                var token = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");
                if (string.IsNullOrEmpty(token))
                {
                    throw new UnauthorizedAccessException("User is not authenticated");
                }
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var json = JsonConvert.SerializeObject(employee);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("http://localhost:5008/api/employee/AddEmployee", content);

                if (!response.IsSuccessStatusCode)
                {
                    var ReasonPhrase =  response.ReasonPhrase;
                    return (false, ReasonPhrase, (int)response.StatusCode);
                }

                return (true, string.Empty, 200);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, 500);
            }
        }
        public async Task<(bool IsSuccess, string ErrorMessage, int StatusCode)> UpdateEmployee(EmployeeDTo employee)
        {
            try
            {
                var token = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");
                if (string.IsNullOrEmpty(token))
                {
                    throw new UnauthorizedAccessException("User is not authenticated");
                }
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var json = JsonConvert.SerializeObject(employee);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync("http://localhost:5008/api/employee/UpdateEmployee", content);

                if (!response.IsSuccessStatusCode)
                {
                    var ReasonPhrase = response.ReasonPhrase;
                    return (false, ReasonPhrase, (int)response.StatusCode);
                }

                return (true, string.Empty, 200);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, 500);
            }
        }
        public async Task<(bool IsSuccess, string ErrorMessage, int StatusCode)> DeleteEmployee(int Id)
        {
            try
            {
                var token = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");
                if (string.IsNullOrEmpty(token))
                {
                    throw new UnauthorizedAccessException("User is not authenticated");
                }
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                
                var response = await _httpClient.DeleteAsync($"http://localhost:5008/api/employee/DeleteEmployee/{Id}");

                if (!response.IsSuccessStatusCode)
                {
                    var ReasonPhrase = response.ReasonPhrase;
                    return (false, ReasonPhrase, (int)response.StatusCode);
                }

                return (true, string.Empty, 200);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, 500);
            }
        }
    }
}
