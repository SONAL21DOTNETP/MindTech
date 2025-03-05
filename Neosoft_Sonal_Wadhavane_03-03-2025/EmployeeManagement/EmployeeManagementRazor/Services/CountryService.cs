using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EmployeeManagementAPI.Models;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagementRazor.Services
{
    public class CountryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public CountryService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"]; // e.g., "https://localhost:7277"
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            // Correct endpoint for countries
            return await _httpClient.GetFromJsonAsync<List<Country>>($"{_apiBaseUrl}/api/Country/GetCountries");
        }
    }
}
