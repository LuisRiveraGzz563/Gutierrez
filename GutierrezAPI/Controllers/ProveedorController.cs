using GutierrezAPI.Models.DTOs.Proveedor;
using GutierrezAPI.Models.Entities;
using GutierrezAPI.Models.Validators;
using GutierrezAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GutierrezAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController(ProveedorRepository repositorio, ILogger<ProveedorController> logger) : ControllerBase
    {
        private readonly ProveedorDTOValidator validador = new();
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var proveedores = await repositorio.GetAll();
            logger.LogInformation("se hizo un GetAll a las: {Time}", DateTime.UtcNow);
            return proveedores != null ? Ok(proveedores) : NotFound("No se han encontrado proveedores");
        }
        [HttpGet("Solicitudes")]
        public IActionResult GetSolicitudesAcceso()
        {
            var proveedores = repositorio.GetSolicitudes();
            logger.LogInformation("se hizo una peticion GET para obtener las solicitudes de acceso a las: {Time}", DateTime.UtcNow);
            return proveedores != null ? Ok(proveedores) : NotFound("No se han encontrado proveedores");
        }

        [HttpPut("ProcesarSolicitud")]
        public IActionResult ProcesarSolicitud(SolicitarAccesoDTO dto)
        {
            if(dto.Estado <=0 || dto.Estado >= 2)
            {
                return Conflict("No puedes ingresar un estado inexistente");
            }
            var proveedor = repositorio.Get(dto.Id);
            if (proveedor == null)
            {
                return NotFound("No existe el proveedor");
            }
            var rfc = dto.Rfc;
            logger.LogInformation("Se ah procesado la solicitud del proveedor con el rfc:{rfc} a las {Time}", rfc, DateTime.UtcNow);
            proveedor.Estado = dto.Estado;
            if (repositorio.Update(proveedor))
            {
                if (proveedor.Estado == 1)
                {
                    logger.LogInformation("la solicitud fue aceptada");
                    return Ok("la solicitud fue aceptada");
                }
                else
                {
                    logger.LogInformation("la solicitud fue rechazada");
                    return Ok("la solicitud fue rechazada");
                }
            }
            return Conflict("Contacta con tu administrador");
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var proveedor = repositorio.GetProveedor(id);
            //el @id es el id del proveedor al que se desea buscar
            logger.LogInformation("se hizo un Get al proveedor con el id:{@id}, a las: {Time}", DateTime.UtcNow, id);
            return proveedor != null ? Ok(proveedor) : NotFound("No se ha encontrado el proveedor");
        }

        [HttpPost("Agregar")]
        public IActionResult Agregar(ProveedorDTO dto)
        {
            if (validador.Validate(dto).IsValid)
            {
                Proveedor proveedor = new()
                {
                    Id = 0,
                    CorreoElectronico = dto.CorreoElectronico,
                    Estado = dto.Estado,
                    NumRegistroRepse = dto.NumRegistroRepse,
                    Rfc = dto.Rfc,
                    Telefono = dto.Telefono,
                    UltimaFechaModificacion = DateOnly.FromDateTime(DateTime.UtcNow),
                    IdTipoRegimen = dto.IdTipoRegimen,
                };
                if (repositorio.Insert(proveedor))
                {
                    string rfc = dto.Rfc;
                    logger.LogInformation("Se ah AGREGADO un proveedor con el rfc: {@rfc} a las: {Time}", DateTime.UtcNow, rfc);
                    return Ok("Se ah agregado el proveedor correctamente");
                }
            }
            logger.LogInformation("Se intento AGREGAR un proveedor con datos incompletos a las: {Time}", DateTime.UtcNow);
            return BadRequest("Ingresa los datos solicitados");
        }

        [HttpPut("Editar")]
        public IActionResult Editar(ProveedorDTO dto)
        {
            if (validador.Validate(dto).IsValid)
            {
                var proveedor = repositorio.Get(dto.Id);
                if (proveedor == null)
                {
                    return NotFound("No se ah encontrado el proveedor");
                }
                else
                {
                    string rfc = dto.Rfc;
                    proveedor.CorreoElectronico = dto.CorreoElectronico;
                    proveedor.IdTipoRegimen = dto.IdTipoRegimen;
                    proveedor.NumRegistroRepse = dto.NumRegistroRepse;
                    proveedor.Telefono = dto.Telefono;
                    proveedor.UltimaFechaModificacion = DateOnly.FromDateTime(DateTime.UtcNow);
                    proveedor.Rfc = rfc;
                    if (repositorio.Update(proveedor))
                    {
                        logger.LogInformation("Se ah EDITADO un proveedor con el rfc: {@rfc} a las: {Time}", DateTime.UtcNow, rfc);
                        return Ok("Se ah EDITADO correctamente el proveedor");
                    }
                }
            }
            logger.LogInformation("Se intento EDITAR un proveedor con datos incompletos a las: {Time}", DateTime.UtcNow);
            return BadRequest("Ingresa los datos solicitados");
        }

        [HttpDelete("{id:int}")]
        public IActionResult Eliminar(int id)
        {                         
            var proveedor = repositorio.Get(id);
            if (proveedor == null)
            {
                logger.LogInformation("se ah intentado ELIMINAR el proveedor con el id:{@id}, a las: {Time}", DateTime.UtcNow, id);
                return NotFound("no se ah encontrado el proveedor");
            }
            //guardamos el repse antes de que se elimine
            string repse = proveedor.NumRegistroRepse;
            if (repositorio.Delete(proveedor))
            {
                logger.LogInformation("se ah ELIMINADO el proveedor con el repse: {@repse}, a las: {Time}", DateTime.UtcNow, repse);
                return Ok("Se ah elimiado el proveedor correctamente");
            }
            return Conflict("No se ah eliminado el proveedor");
        }
    }
}