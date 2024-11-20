using System;
using System.Collections.Generic;

namespace GutierrezAPI.Models.Entities;

public partial class UsuarioProveedor
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public int IdProveedor { get; set; }

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
