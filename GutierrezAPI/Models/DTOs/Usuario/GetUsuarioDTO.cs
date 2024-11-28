using System.Text.Json.Serialization;

namespace GutierrezAPI.Models.DTOs.Usuario
{
    public class GetUsuarioDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("rfc")]
        public string Rfc { get; set; } = null!;
        [JsonPropertyName("proveedor")]
        public string Proveedor { get; set; } = null!;   
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;
        [JsonPropertyName("estatus")]
        public int Estatus { get; set; }
        
    }
}
