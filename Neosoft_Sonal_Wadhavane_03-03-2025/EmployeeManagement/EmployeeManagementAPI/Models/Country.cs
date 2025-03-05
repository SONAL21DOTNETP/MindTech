using System.Text.Json.Serialization;

namespace EmployeeManagementAPI.Models
{
    public class Country
    {
        [JsonPropertyName("row_Id")]
        public int Id { get; set; }

        [JsonPropertyName("countryName")]
        public string Name { get; set; }
    }
}
