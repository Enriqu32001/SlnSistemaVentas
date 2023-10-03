using System;
using System.Collections.Generic;

namespace SistemaVentas.Entity;

public partial class Venta
{
    public int IdVenta { get; set; }

    public string? NumeroVenta { get; set; }

    public int? IdTipoDocumentoVenta { get; set; }

    public int? IdCliente { get; set; }

    public int? IdTipoPago { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public int? IdUsuarioRegistro { get; set; }

    public decimal? Total { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; } = new List<DetalleVenta>();

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual TipoDocumentoVenta? IdTipoDocumentoVentaNavigation { get; set; }

    public virtual TipoPago? IdTipoPagoNavigation { get; set; }

    public virtual Usuario? IdUsuarioRegistroNavigation { get; set; }
}
