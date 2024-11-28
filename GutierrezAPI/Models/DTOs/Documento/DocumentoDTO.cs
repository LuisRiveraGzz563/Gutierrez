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
        public DateOnly SoliciarApartirDe { get; set; }
        [JsonPropertyName("enviarCada")]
        public int EnviarCada { get; set; }
        [JsonPropertyName("link")]
        public string Link { get; set; } = null!;
        [JsonPropertyName("omitir")]
        public sbyte Omitir { get; set; }
    }
}
