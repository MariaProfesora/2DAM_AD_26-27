using System.Text.Json.Serialization;

namespace _4VGymAPI.Models
{
    public class Instructor
    {
       
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("telefono")]
        public string Telefono { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        

        [JsonPropertyName("photo_url")]
        public string PhotoUrl { get; set; } = string.Empty;

        [JsonPropertyName("phone")]
        public string Phone { get; set; } = string.Empty;

        public Instructor()
        {
        }

        public Instructor(int id, string name, string telefono, string email, string photoUrl, string phone)
        {
            Id = id;
            Name = name;
            Telefono = telefono;
            Email = email;
            PhotoUrl = photoUrl;
            Phone = phone;
        }


    }
}
