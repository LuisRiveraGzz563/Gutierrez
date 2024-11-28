using GutierrezAPI.Helpers;
using GutierrezAPI.Models.DTOs.Usuario;
using GutierrezAPI.Models.Entities;
using GutierrezAPI.Models.Validators;
using GutierrezAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GutierrezAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController(Repository<Usuario> repos, ILogger<UsuariosController> logger) : ControllerBase
    {
        private readonly UserDTOValidator UserValidator = new();
        [HttpGet]
        public IActionResult GetAll()
        {
            var usuarios = repos.GetAll()
                .Include(x => x.UsuarioProveedor)
                    .ThenInclude(x => x.IdProveedorNavigation)
                        .ThenInclude(x => x.IdProveedorServiciosNavigation)
                .OrderBy(x => x.Nombre)
                .Select(x => new GetUsuarioDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Estatus = x.UsuarioProveedor.First().IdProveedorNavigation.Estado > 0 ? 
                              x.UsuarioProveedor.First().IdProveedorNavigation.Estado : 0,
                    Proveedor = x.UsuarioProveedor.First().IdProveedorNavigation.IdProveedorServiciosNavigation.Nombre ?? "N/A",
                    Rfc = x.UsuarioProveedor.First().IdProveedorNavigation.Rfc ?? "N/A"
                });
            return usuarios != null ? Ok(usuarios) : NotFound("No se han encontrado usuarios");
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var usuario = repos.Get(id);

            return usuario != null ? Ok(usuario) : NotFound("No se han encontrado usuarios");
        }

        [HttpPost("Agregar")]
        public IActionResult Agregar(UserDTO usuario)
        {
            if (UserValidator.Validate(usuario).IsValid)
            {
                Usuario user = new()
                {
                    IdRol = usuario.IdRol,
                    Correo = usuario.Correo,
                    Contraseña = Encriptacion.EncriptarSha512(usuario.Contraseña),
                    Nombre = usuario.Usuario
                };
                if (repos.Insert(user))
                {
                    logger.LogTrace("Se Creo un usuario a las:{Time}", DateTime.UtcNow);
                    return Ok("Se agrego el usuario");
                }
            }
            logger.LogInformation("se intento AGREGAR un usuario con datos incompletos a las: {Time}", DateTime.UtcNow);
            return BadRequest("Ingrese los datos solicitados");
        }

        [HttpPut("Editar")]
        public IActionResult Editar(UserDTO usuario)
        {
            if (UserValidator.Validate(usuario).IsValid)
            {
                var user = repos.Get(usuario.Id);
                if (user == null)
                {
                    return NotFound("No se ah encontrado el usuario que desea editar");
                }
                user.Nombre = user.Nombre;
                user.Correo = user.Correo;
                user.IdRol = user.IdRol;
                user.Contraseña = Encriptacion.EncriptarSha512(user.Contraseña);

                if (repos.Update(user))
                {
                    var nombre = user.Nombre;
                    logger.LogTrace("se EDITO correctamente el usuario {$nombre} a las {Time}", nombre, DateTime.UtcNow);
                    return Ok("Se edito el usuario");
                }
            }
            logger.LogInformation("se intento EDITAR un usuario con datos incompletos a las: {Time}", DateTime.UtcNow);
            return BadRequest("Ingrese los datos solicitados");
        }

        [HttpDelete("Eliminar/{id:int}")]
        public IActionResult Eliminar(int id)
        {
            var user = repos.Get(id);
            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }
            else if (repos.Delete(user))
            {
                var nombre = user.Nombre;
                logger.LogInformation("Se ah eliminado el usuario {$nombre} a las {Time}", nombre, DateTime.UtcNow);
                return Ok("El usuario se ah eliminado");
            }
            //Esta respuesta se conserva en caso de que se utilice un numero que sea mayor a un entero
            return BadRequest("No se ah eliminado el Usuario");
        }
    }
}