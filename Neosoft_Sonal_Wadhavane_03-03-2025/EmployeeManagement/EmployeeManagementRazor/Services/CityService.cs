using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EmployeeManagementAPI.Models;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagementRazor.Services
{
    public class CityService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public CityService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"];
        }

        public async Task<List<City>> GetCitiesByStateAsync(int stateId)
        {
            // Correct endpoint for cities based on state
            return await _httpClient.GetFromJsonAsync<List<City>>($"{_apiBaseUrl}/api/City/GetCities/{stateId}");
        }

        // Optional: Get all cities if needed
        public async Task<List<City>> GetCitiesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<City>>($"{_apiBaseUrl}/api/City/GetCities");
        }
    }
}
