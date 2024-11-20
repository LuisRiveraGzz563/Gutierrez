using System;
using System.Collections.Generic;

namespace GutierrezAPI.Models.Entities;

public partial class ProveedorDocumento
{
    public int Id { get; set; }

    public int IdProveedor { get; set; }

    public int IdDocumento { get; set; }

    public virtual Documento IdDocumentoNavigation { get; set; } = null!;

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;
}
