using SistemaVentas.Entity;

namespace SistemaVentas.AppWeb.Models.ViewModels
{
    public class VMDetalleVenta
    {
        public int? IdProducto              { get; set; }

        public string? MarcaProducto        { get; set; }

        public string? Descripcion          { get; set; }

        public string? CategoriaProducto    { get; set; }

        public int? Cantidad                { get; set; }

        public string? FechaRegistro { get; set; }

        public string? NumeroVenta { get; set; }

        public string? TipoDocumento { get; set; }

        public string? DocumentoCliente { get; set; }

        public string? NombreCliente { get; set; }

        public string? Usuario { get; set; }

        public string? Precio_unitario { get; set; }

        public string? ImporteRecibido { get; set; }

        public string? ImporteCambio { get; set; }

        public string? subtotal { get; set; }

        public string? impuestoTotal { get; set; }

        public string? Total { get; set; }

    }
}
