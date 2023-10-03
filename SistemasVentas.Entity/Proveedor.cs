using System;
using System.Collections.Generic;

namespace SistemaVentas.Entity;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    public string? Nombre { get; set; }

    public string Telefono { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
