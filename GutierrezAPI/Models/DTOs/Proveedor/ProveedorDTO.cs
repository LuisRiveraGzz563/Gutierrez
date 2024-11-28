using System.Text.Json.Serialization;

namespace GutierrezAPI.Models.DTOs.Proveedor
{
    public class ProveedorDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("numRegistroRepse")]
        public string NumRegistroRepse { get; set; } = null!;
        [JsonPropertyName("idProveedorServicios")]
        public int? IdProveedorServicios { get; set; }
        [JsonPropertyName("correoElectronico")]
        public string CorreoElectronico { get; set; } = null!;
        [JsonPropertyName("telefono")]
        public string Telefono { get; set; } = null!;
        [JsonPropertyName("idTipoRegimen")]
        public int IdTipoRegimen { get; set; }
        [JsonPropertyName("rfc")]
        public string Rfc { get; set; } = null!;
        [JsonPropertyName("estado")]
        public int Estado { get; set; }
        [JsonPropertyName("ultimaFechaModificacion")]
        public DateOnly UltimaFechaModificacion { get; set; }
        [JsonPropertyName("idDocumentos")]
        public int IdDocumentos { get; set; }
    }
}
