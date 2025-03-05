using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;


namespace EmployeeManagementAPI.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CountryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetCountries")]
        public IActionResult GetCountries()
        {
            List<object> countries = new List<object>();
            string query = "SELECT Row_Id, CountryName FROM Country";

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            countries.Add(new
                            {
                                Row_Id = reader["Row_Id"],
                                CountryName = reader["CountryName"].ToString()
                            });
                        }
                    }
                }
            }
            return Ok(countries);
        }
    }

}
