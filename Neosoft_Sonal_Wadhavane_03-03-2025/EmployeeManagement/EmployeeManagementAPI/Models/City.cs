using System.Text.Json.Serialization;

namespace EmployeeManagementAPI.Models
{
    public class City
    {
        [JsonPropertyName("row_Id")]
        public int Id { get; set; }

        [JsonPropertyName("cityName")]
        public string Name { get; set; }

        // Include this if your API returns a StateId.
        public int StateId { get; set; }
    }
}
