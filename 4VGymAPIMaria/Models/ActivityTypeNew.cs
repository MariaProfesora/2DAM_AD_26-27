using System.Text.Json.Serialization;

namespace _4VGymAPI.Models
{
    public class ActivityTypeNew
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("number-monitors")]
        public int? NumberMonitors { get; set; } = 1;
        public ActivityTypeNew() { }
        public ActivityTypeNew(string name, int? numberMonitors)
        {
            Name = name;
            NumberMonitors = numberMonitors;
        }
    }
}
