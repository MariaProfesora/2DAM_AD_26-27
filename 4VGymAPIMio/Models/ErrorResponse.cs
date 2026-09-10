using System.Text.Json.Serialization;

namespace _4VGymAPI.Models
{

    /// <summary>
    /// Representa la estructura de error según la especificación OpenAPI de 4VGym.
    /// </summary>
    public class ErrorResponse
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        public ErrorResponse() { }

        public ErrorResponse(int code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
