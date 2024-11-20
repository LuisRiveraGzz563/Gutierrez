using System.Text.Json.Serialization;

namespace GutierrezAPI.Models.DTOs.Proveedor
{
    public class ProveedorDocumentoDTO
    {
       public int Id { get; set; }
        public int IdProveedor { get; set; }             
        public int IdDocumento { get; set; }
    }
}
