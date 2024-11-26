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
            var lista = Context.ProveedorDocumento
                .Include(p => p.IdProveedorNavigation)
                    .ThenInclude(x => x.UsuarioProveedor)
                        .ThenInclude(x => x.IdUsuarioNavigation)
                .Select(x => new GetProveedorDTO
                {
                    Id = x.Id,
                    Nombre = x.IdProveedorNavigation.UsuarioProveedor.First().IdUsuarioNavigation.Nombre.ToString(),
                    Estado = x.IdProveedorNavigation.Estado,
                    Rfc = x.IdProveedorNavigation.Rfc,
                    UltimaFechaModificacion = x.IdProveedorNavigation.UltimaFechaModificacion
                }).AsAsyncEnumerable();
            await Task.CompletedTask;
            return lista;
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
