using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using EmployeeManagementAPI.Models; // ✅ Import Employee model from API
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagementRazor.Services;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;


namespace EmployeeManagementRazor.Pages.Employees
{
   

    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public List<Employee> Employees { get; set; } = new List<Employee>();

        public IndexModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"]; // Fetch API URL from appsettings.json
        }

        public async Task OnGetAsync()
        {
            try
            {
                Employees = await _httpClient.GetFromJsonAsync<List<Employee>>($"{_apiBaseUrl}/api/Employee");
            }
            catch (HttpRequestException ex)
            {
                // Log error (optional)
                Console.WriteLine($"Error fetching data: {ex.Message}");
            }
        }
    }

}
