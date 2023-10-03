using SistemaVentas.Entity;

namespace SistemaVentas.AppWeb.Models.ViewModels
{
    public class VMVenta
    {
        public int IdVenta                  { get; set; }

        public string? NumeroVenta          { get; set; }

        public int? IdTipoDocumentoVenta    { get; set; }

        public string? TipoDocumentoVenta   { get; set; }

        public int? idCliente               { get; set; }

        public string? NumeroIdentificacion { get; set; }

        public int? IdTipoPago              { get; set; }

        public string? TipoPago             { get; set; }

        public int? IdUsuarioRegistro       { get; set; }

        public string? Usuario              { get; set; }

        public string? NombreCliente        { get; set; }

        public string? subtotalTotal        { get; set; }
        
        public string? impuestototalTotal    { get; set; }

        public string? Total                { get; set; }

        public string? FechaRegistro        { get; set; }

        public virtual ICollection<VMDetalleVenta>? DetalleVenta { get; set; }
        public virtual ICollection<VMCliente>? Cliente { get; set; }
    }
}
