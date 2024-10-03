using EmployeeManagement.Mvc.Models;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeManagement.Mvc.Services
{
    public class EmployeeServices
    {
        private readonly HttpClient _httpClient;

        public EmployeeServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<EmployeeDTo>> GetAllEmployeesAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:5008/api/employee");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<EmployeeDTo>>(jsonResponse);
        }

        public async Task<int> CreateEmployeeAsync(EmployeeDTo employee)
        {
            var json = JsonConvert.SerializeObject(employee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:5008/api/employee", content);
            response.EnsureSuccessStatusCode();

            var createdId = await response.Content.ReadAsStringAsync();
            return int.Parse(createdId);
        }
    }
}
