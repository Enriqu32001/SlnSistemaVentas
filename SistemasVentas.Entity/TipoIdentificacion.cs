using System;
using System.Collections.Generic;

namespace SistemaVentas.Entity;

public partial class TipoIdentificacion
{
    public int IdTipoIdentificacion { get; set; }

    public string? nombreIdentificacion { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
