using GutierrezAPI.Helpers;
using GutierrezAPI.Models.DTOs.Login;
using GutierrezAPI.Models.Entities;
using GutierrezAPI.Models.Security;
using GutierrezAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GutierrezAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(Repository<Usuario> repository, IConfiguration configuration,ILogger<LoginController> logger) : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(LoginDTO login)
        {
            login.Contraseña = Encriptacion.EncriptarSha512(login.Contraseña);
            var user = repository.GetAll()
                .Include(x=>x.UsuarioProveedor)
                .Include(x=>x.IdRolNavigation)
                .FirstOrDefault(x=>x.Correo == login.Correo && x.Contraseña == login.Contraseña);
            if (user == null)
            {
                return Unauthorized("Usuario no autorizado.");
            }
            else
            {
                var JWTConfiguration = configuration.GetSection("JWT").Get<JWT>();
                if (JWTConfiguration != null)
                {
                    List<Claim> claims =
                    [
                        new Claim("Id", user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Nombre),
                        new Claim(ClaimTypes.Role, user.IdRolNavigation.Nombre)
                    ];
                    SecurityTokenDescriptor TokenDescriptor = new()
                    {
                        Issuer = JWTConfiguration.Issuer,
                        Audience = JWTConfiguration.Audience,
                        IssuedAt = DateTime.UtcNow,
                        Expires = DateTime.UtcNow.AddHours(5),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTConfiguration.Key)),
                        SecurityAlgorithms.HmacSha256),
                        Subject = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme)
                    };
                    JwtSecurityTokenHandler handler = new();
                    var token = handler.CreateToken(TokenDescriptor);
                    var nombreusuario = user.Nombre;
                    logger.LogInformation("el usuario {@nombreusuario} ah iniciado sesion a las: {Time}",DateTime.UtcNow, nombreusuario);
                    return Ok(handler.WriteToken(token));
                }
                logger.LogWarning("Se intento iniciar sesion a las:{Time}",DateTime.UtcNow);
                return Conflict("Servidor no configurado");
            }
        }
    }
}
