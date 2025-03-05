using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetCities/{stateId}")]
        public IActionResult GetCities(int stateId)
        {
            List<object> cities = new List<object>();
            string query = "SELECT Row_Id, CityName FROM City WHERE StateId = @StateId";

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@StateId", stateId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cities.Add(new
                            {
                                Row_Id = reader["Row_Id"],
                                CityName = reader["CityName"].ToString()
                            });
                        }
                    }
                }
            }
            return Ok(cities);
        }
    }

}
