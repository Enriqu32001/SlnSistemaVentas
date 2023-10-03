using System;
using System.Collections.Generic;

namespace SistemaVentas.Entity;

public partial class TipoPago
{
    public int IdTipoPago { get; set; }

    public string? NombreTipoPago { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
