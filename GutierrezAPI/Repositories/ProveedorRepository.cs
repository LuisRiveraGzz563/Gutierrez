using GutierrezAPI.Models.DTOs.Proveedor;
using GutierrezAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GutierrezAPI.Repositories
{
    public class ProveedorRepository(LabsysteGutierrezContext context) : Repository<Proveedor>(context)
    {
        private readonly LabsysteGutierrezContext Context = context;
        public async new Task<IAsyncEnumerable<GetProveedorDTO>> GetAll()
        {
            var lista = Context.UsuarioProveedor
                    .Include(x => x.IdProveedorNavigation)
                    .Include(x=>x.IdUsuarioNavigation)
                .Select(x => new GetProveedorDTO
                {
                    Id = x.Id,
                    Nombre = x.IdUsuarioNavigation.Nombre,
                    Estado = x.IdProveedorNavigation.Estado,
                    Rfc = x.IdProveedorNavigation.Rfc,
                    UltimaFechaModificacion = x.IdProveedorNavigation.UltimaFechaModificacion
                }).AsAsyncEnumerable();
            await Task.CompletedTask;
            return lista;
        }
        public IEnumerable<SolicitarAccesoDTO> GetSolicitudes()
        {
            var solicitudes = Context.Proveedor
                .Include(x=>x.IdProveedorServiciosNavigation)
                .Select(x=> new SolicitarAccesoDTO
                {
                    Id = x.Id,
                    Nombre = x.IdProveedorServiciosNavigation.Nombre,
                    Rfc = x.Rfc,
                    Estado = x.Estado
                })      
                .AsEnumerable();
            return solicitudes;
        }
        public ProveedorDocumento? GetProveedorDocumentoByRepse(string numrepse)
        {
            var proveedordocumento = Context.ProveedorDocumento
                .Include(x => x.IdProveedorNavigation)
                .Include(x => x.IdDocumentoNavigation)
                .FirstOrDefault(x => x.IdProveedorNavigation.NumRegistroRepse == numrepse);

            return proveedordocumento;
        }
        public ProveedorDTO? GetProveedor(int id)
        {
            var datos = Context.Proveedor
              .Select(x => new ProveedorDTO
              {
                  Id = x.Id,
                  CorreoElectronico = x.CorreoElectronico,
                  Estado = x.Estado,
                  IdDocumentos = x.IdDocumentos,
                  IdProveedorServicios = x.IdProveedorServicios,
                  IdTipoRegimen = x.IdTipoRegimen,
                  NumRegistroRepse = x.NumRegistroRepse,
                  Rfc = x.Rfc,
                  Telefono = x.Telefono,
                  UltimaFechaModificacion = x.UltimaFechaModificacion
              })
              .FirstOrDefault(x => x.Id == id);
            return datos;
        }
        public IEnumerable<ProveedorDocumento?> GetDocumentosProveedor(int id)
        {
            var datos = Context.ProveedorDocumento
                .Include(x => x.IdDocumentoNavigation)
                .Where(x => x.Id == id);
            return datos;
        }
    }
}
