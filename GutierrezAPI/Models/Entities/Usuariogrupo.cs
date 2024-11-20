using System;
using System.Collections.Generic;

namespace GutierrezAPI.Models.Entities;

public partial class Usuariogrupo
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public int IdGrupo { get; set; }

    public virtual Grupo IdGrupoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
