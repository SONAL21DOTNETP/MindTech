using System.Text.Json.Serialization;

namespace EmployeeManagementAPI.Models
{
    public class State
    {
        [JsonPropertyName("row_Id")]
        public int Id { get; set; }

        [JsonPropertyName("stateName")]
        public string Name { get; set; }
    }
}
