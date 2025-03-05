using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EmployeeManagementAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public StateController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetStates/{countryId}")]
        public IActionResult GetStates(int countryId)
        {
            List<object> states = new List<object>();
            string query = "SELECT Row_Id, StateName FROM State WHERE CountryId = @CountryId";

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CountryId", countryId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            states.Add(new
                            {
                                Row_Id = reader["Row_Id"],
                                StateName = reader["StateName"].ToString()
                            });
                        }
                    }
                }
            }
            return Ok(states);
        }
    }

}
