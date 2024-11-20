using System;
using System.Collections.Generic;

namespace GutierrezAPI.Models.Entities;

public partial class Grupo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Usuariogrupo> Usuariogrupo { get; set; } = new List<Usuariogrupo>();
}
