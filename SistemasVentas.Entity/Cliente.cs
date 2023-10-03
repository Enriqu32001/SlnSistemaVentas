using System;
using System.Collections.Generic;

namespace SistemaVentas.Entity;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string? PnySn { get; set; }

    public string? PaySa { get; set; }

    public string? Direccion { get; set; }

    public string? TelefonoPrincipal { get; set; }

    public string? TelefonoSecundario { get; set; }

    public string? NumeroIdentificacion { get; set; }

    public int? IdTipoIdentificacion { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public int? IdUsuarioRegistro { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? IdUsuarioModificacion { get; set; }

    public virtual TipoIdentificacion? IdTipoIdentificacionNavigation { get; set; }

    public virtual Usuario? IdUsuarioModificacionNavigation { get; set; }

    public virtual Usuario? IdUsuarioRegistroNavigation { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
