using System.Text.Json.Serialization;

namespace _4VGymAPI.Models
{
    public class NewActivityTypeDTO
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("number-monitors")]
        public int? NumberMonitors { get; set; } = 1;
        public NewActivityTypeDTO() { }
        public NewActivityTypeDTO(string name, int? numberMonitors)
        {
            Name = name;
            NumberMonitors = numberMonitors;
        }
    }
}
