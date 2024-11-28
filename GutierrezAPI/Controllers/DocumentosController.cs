using GutierrezAPI.Models.DTOs.Documento;
using GutierrezAPI.Models.Entities;
using GutierrezAPI.Models.Validators;
using GutierrezAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GutierrezAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosController(Repository<Documento> documentosrepos, ILogger<DocumentosController> logger) : ControllerBase
    {
 
        private readonly DocumentoDTOValidator validador = new();
        [HttpGet]
        public IActionResult GetAll()
        {
            var proveedores = documentosrepos.GetAll().Include(x=>x.ProveedorDocumento);
            return proveedores != null ? Ok(proveedores) : NotFound("No se han encontrado proveedores");
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var proveedor = documentosrepos.Get(id);
            return proveedor != null ? Ok(proveedor) : NotFound("No se ha encontrado el proveedor");
        }

        [HttpPost("Agregar")]
        public IActionResult Agregar(DocumentoDTO documento)
        {
            if (validador.Validate(documento).IsValid)
            {
                Documento doc = new()
                {
                    Id = 0,
                    EnviarCada = documento.EnviarCada,
                    //este debe ser un datetime para validar los documentos despues
                    SoliciarApartirDe = documento.SoliciarApartirDe,
                    Link = documento.Link,
                    Nombre = documento.Nombre,
                    Omitir = documento.Omitir,//1 para si 2 para no
                };

                if (documentosrepos.Insert(doc))
                {
                    string docname = documento.Nombre;
                    logger.LogInformation("Se ha agregado el documento {@docname} a las {Time}", DateTime.UtcNow, docname);
                    return Ok("Se ha agregado el documento");
                }
            }
            return BadRequest("Ingrese los datos del documento");
        }

        [HttpPut("Editar")]
        public IActionResult Editar(DocumentoDTO dto)
        {
            if (validador.Validate(dto).IsValid)
            {
                //buscar el documento anterior
                var documento = documentosrepos.Get(dto.Id);
                if (documento == null)
                {
                    return NotFound("Documento no encontrado");
                }
                else
                {
                    documento.EnviarCada = dto.EnviarCada;
                    documento.SoliciarApartirDe = dto.SoliciarApartirDe;
                    documento.Omitir = dto.Omitir;
                    documento.Link = dto.Link;
                    documento.Nombre = dto.Nombre;

                    if (documentosrepos.Update(documento))
                    {
                        logger.LogInformation("Se ha editado el documento con el id:{@id}, a las:{Time}", DateTime.UtcNow, dto.Id);
                        return Ok("Se ha editado el documento correctamente");
                    }
                }
            }
            return BadRequest("Ingrese los datos solicitados");
           
        }
        [HttpDelete("{id:int}")]
        public IActionResult Eliminar(int id)
        {
            var documento = documentosrepos.Get(id);
            
            if (documento == null)
                return NotFound("No se ha encontrado el documento");
            //se eliminara la relacion que exíste entre el documento y el proveedor
            string docname = documento.Nombre;
            if (documentosrepos.Delete(documento))
            {
                logger.LogInformation("Se ha eliminado el documento con el nombre:{@docname}, a las: {Time}",DateTime.UtcNow,docname);
                return Ok("Se ha eliminado correctamente el documento");
            }
            return Conflict("No se ha eliminado el documento");
        }
    }
}
