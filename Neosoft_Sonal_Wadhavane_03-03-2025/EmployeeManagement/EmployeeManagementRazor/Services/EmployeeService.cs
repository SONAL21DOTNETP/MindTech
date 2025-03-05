using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EmployeeManagementAPI.Models;
using Microsoft.Extensions.Configuration;
namespace EmployeeManagementRazor.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public EmployeeService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"];  // Read API base URL from appsettings.json
        }

        // ✅ Fetch Employee List from API
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>($"{_apiBaseUrl}/api/employees");
        }

        // ✅ Create Employee
        public async Task<bool> CreateEmployeeAsync(Employee employee)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/employees", employee);
            return response.IsSuccessStatusCode;
        }

        // ✅ Get Employee by ID
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Employee>($"{_apiBaseUrl}/api/employees/{id}");
        }

        // ✅ Update Employee
        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/api/employees/{employee.Id}", employee);
            return response.IsSuccessStatusCode;
        }

        // ✅ Delete Employee
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/employees/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
