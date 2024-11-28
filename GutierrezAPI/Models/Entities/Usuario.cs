using System;
using System.Collections.Generic;

namespace GutierrezAPI.Models.Entities;

public partial class Usuario
{
    public int Id { get; set; }

    public int IdRol { get; set; }

    public string Contraseña { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<UsuarioProveedor> UsuarioProveedor { get; set; } = new List<UsuarioProveedor>();
}
