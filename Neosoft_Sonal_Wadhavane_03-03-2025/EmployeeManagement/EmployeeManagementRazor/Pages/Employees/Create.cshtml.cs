using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagementAPI.Models;
using EmployeeManagementRazor.Services;

namespace EmployeeManagementRazor.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly CountryService _countryService;
        private readonly StateService _stateService;
        private readonly CityService _cityService;

        // Properties to hold data for dropdowns
        public List<Country> Countries { get; set; } = new List<Country>();
        public List<State> States { get; set; } = new List<State>();     // Optional: preload if needed
        public List<City> Cities { get; set; } = new List<City>();         // Optional: preload if needed

        public CreateModel(
            CountryService countryService,
            StateService stateService,
            CityService cityService)
        {
            _countryService = countryService;
            _stateService = stateService;
            _cityService = cityService;
        }
        [BindProperty]
        public Employee Employee { get; set; } = new Employee();


        [BindProperty]
        public IFormFile ProfileImage { get; set; } 

        public async Task OnGetAsync()
        {
            // Load countries for the dropdown
            Countries = await _countryService.GetCountriesAsync();

            // Optionally, preload states and cities if desired.
            // Uncomment the following lines if you want to preload them.
            // States = await _stateService.GetStatesAsync();
            // Cities = await _cityService.GetCitiesAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ProfileImage != null && ProfileImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/Employee");
                Directory.CreateDirectory(uploadsFolder); // Ensure the directory exists

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ProfileImage.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(fileStream);
                }

                Employee.ProfileImage = "/Uploads/Employee/" + uniqueFileName; // Save path in the model
            }
            else
            {
                ModelState.AddModelError("ProfileImage", "Please upload a profile picture.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Save Employee data to DB using ADO.NET logic

            return RedirectToPage("Index");
        }

        // Page Handler to load states dynamically based on the selected country.
        public async Task<JsonResult> OnGetStatesByCountryAsync(int countryId)
        {
            var states = await _stateService.GetStatesByCountryAsync(countryId);
            return new JsonResult(states);
        }

        // Page Handler to load cities dynamically based on the selected state.
        public async Task<JsonResult> OnGetCitiesByStateAsync(int stateId)
        {
            var cities = await _cityService.GetCitiesByStateAsync(stateId);
            return new JsonResult(cities);
        }
    }
}
