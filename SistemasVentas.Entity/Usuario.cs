using System;
using System.Collections.Generic;

namespace SistemaVentas.Entity;

public partial class  Usuario 
{
    public int IdUsuario { get; set; }

    public string? NombreCompleto { get; set; }

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public int? IdRol { get; set; }

    public string? UrlFoto { get; set; }

    public string? NombreFoto { get; set; }

    public string? Clave { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public int? IdUsuarioRegistro { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public int? IdUsuarioModificacion { get; set; }

    public virtual ICollection<Cliente> ClienteIdUsuarioModificacionNavigations { get; set; } = new List<Cliente>();

    public virtual ICollection<Cliente> ClienteIdUsuarioRegistroNavigations { get; set; } = new List<Cliente>();

    public virtual Rol? IdRolNavigation { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
