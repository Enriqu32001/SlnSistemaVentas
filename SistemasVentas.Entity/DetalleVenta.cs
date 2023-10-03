using System;
using System.Collections.Generic;

namespace SistemaVentas.Entity;

public partial class DetalleVenta
{
    public int IdDetalleVenta { get; set; }

    public int? IdVenta { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Precio_unitario { get; set; }

    public decimal? ImporteRecibido { get; set; }

    public decimal? ImporteCambio { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? ImpuestoTotal { get; set; }

    public decimal? Total { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Venta? IdVentaNavigation { get; set; }
}
