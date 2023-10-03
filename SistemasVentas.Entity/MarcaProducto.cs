using System;
using System.Collections.Generic;

namespace SistemaVentas.Entity;

public partial class MarcaProducto
{
    public int IdMarcaProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? EsActivo { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
