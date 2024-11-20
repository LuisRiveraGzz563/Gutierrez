﻿using System.Text.Json.Serialization;

namespace GutierrezAPI.Models.DTOs.Proveedor
{
    public class GetProveedorDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = null!;
        [JsonPropertyName("rfc")]
        public string Rfc { get; set; } = null!;
        [JsonPropertyName("estado")]
        public int Estado { get; set; }
        [JsonPropertyName("ultimaFechaModificacion")]
        public DateOnly UltimaFechaModificacion { get; set; }
    }
}
