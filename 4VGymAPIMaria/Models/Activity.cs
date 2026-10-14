using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _4VGymAPI.Models
{
    public class Activity
    {
        public long Id { get; set; }

        [JsonPropertyName("activity_type")]
        public required ActivityType? ActivityType { get; set; }

        [JsonPropertyName("instructors")]
        public required List<Instructor> Instructors { get; set; }

        [JsonPropertyName("max_participants")]
        public int MaxParticipants { get; set; }

        [JsonPropertyName("date_start")]
        public DateTime DateStart { get; set; }

        [JsonPropertyName("date_end")]
        public DateTime DateEnd { get; set; }
    }
}
