using System;
using System.Collections.Generic;

namespace GutierrezAPI.Models.Entities;

public partial class Documento
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateOnly SoliciarApartirDe { get; set; }

    public int EnviarCada { get; set; }

    public string Link { get; set; } = null!;

    public sbyte Omitir { get; set; }

    public virtual ICollection<ProveedorDocumento> ProveedorDocumento { get; set; } = new List<ProveedorDocumento>();
}
