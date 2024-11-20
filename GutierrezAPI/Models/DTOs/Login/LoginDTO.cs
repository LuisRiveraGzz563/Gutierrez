using System.Text.Json.Serialization;

namespace GutierrezAPI.Models.DTOs.Login
{
    public class LoginDTO
    {
        [JsonPropertyName("correo")]
        public string Correo { get; set; } = null!;
        [JsonPropertyName("contraseña")]
        public string Contraseña { get; set; } = null!;
    }
}
