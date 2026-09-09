using System.Text.Json.Serialization;

namespace _4VGymAPI.Models
{
    public class TipoActividad
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }
        [JsonPropertyName("minimo_numero_monitores")]
        public int MinimoNumeroMonitores { get; set; }
        
    }
}
