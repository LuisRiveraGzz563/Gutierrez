using System;
using System.Collections.Generic;

namespace GutierrezAPI.Models.Entities;

public partial class Proveedor
{
    public int Id { get; set; }

    public string NumRegistroRepse { get; set; } = null!;

    public int? IdProveedorServicios { get; set; }

    public string CorreoElectronico { get; set; } = null!;

    public int Telefono { get; set; }

    public int IdTipoRegimen { get; set; }

    public string Rfc { get; set; } = null!;

    public int Estado { get; set; }

    public DateOnly UltimaFechaModificacion { get; set; }

    public int IdDocumentos { get; set; }

    public virtual ICollection<ProveedorDocumento> ProveedorDocumento { get; set; } = new List<ProveedorDocumento>();

    public virtual ICollection<UsuarioProveedor> UsuarioProveedor { get; set; } = new List<UsuarioProveedor>();
}
