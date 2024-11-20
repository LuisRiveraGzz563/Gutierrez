using System.Text.Json.Serialization;

namespace GutierrezAPI.Models.DTOs.Documento
{
    public class DocumentoDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;
        [JsonPropertyName("solicitarAPartirDe")]
        public string SoliciarApartirDe { get; set; } = null!;
        [JsonPropertyName("enviarCada")]
        public string EnviarCada { get; set; } = null!;
        [JsonPropertyName("link")]
        public string Link { get; set; } = null!;
        [JsonPropertyName("omitir")]
        public sbyte Omitir { get; set; }
    }
}
