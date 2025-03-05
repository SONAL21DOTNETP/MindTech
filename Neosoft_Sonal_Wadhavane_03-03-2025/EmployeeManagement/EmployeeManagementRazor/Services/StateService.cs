using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EmployeeManagementAPI.Models;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagementRazor.Services
{
    public class StateService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public StateService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"];
        }

        public async Task<List<State>> GetStatesByCountryAsync(int countryId)
        {
            return await _httpClient.GetFromJsonAsync<List<State>>($"{_apiBaseUrl}/api/State/GetStates/" + countryId);
        }

        // Optional: load all states if needed.
        public async Task<List<State>> GetStatesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<State>>($"{_apiBaseUrl}/api/State/GetStates");
        }
    }
}
