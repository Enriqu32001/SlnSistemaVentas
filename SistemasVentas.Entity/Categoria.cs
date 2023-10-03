using System;
using System.Collections.Generic;

namespace SistemaVentas.Entity;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string? nombreCategoria { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public int? IdUsuarioRegistro { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? IdUsuarioModificacion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
