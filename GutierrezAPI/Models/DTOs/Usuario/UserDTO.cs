using GutierrezAPI.Models.Entities;
using System.Text.Json.Serialization;

namespace GutierrezAPI.Models.DTOs.Usuario
{
    public class UserDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("idRol")]
        public int IdRol { get; set; }
        [JsonPropertyName("contraseña")]
        public string Contraseña { get; set; } = null!;
        [JsonPropertyName("correo")]
        public string Correo { get; set; } = null!;
        [JsonPropertyName("usuario")]
        public string Usuario { get; set; } = null!;

    }
}
