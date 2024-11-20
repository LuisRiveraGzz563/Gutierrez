using System;
using System.Collections.Generic;

namespace GutierrezAPI.Models.Entities;

public partial class Documento
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string SoliciarApartirDe { get; set; } = null!;

    public string EnviarCada { get; set; } = null!;

    public string Link { get; set; } = null!;

    public sbyte Omitir { get; set; }

    public virtual ICollection<ProveedorDocumento> ProveedorDocumento { get; set; } = new List<ProveedorDocumento>();
}
