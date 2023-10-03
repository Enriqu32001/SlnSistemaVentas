namespace SistemaVentas.AppWeb.Models.ViewModels
{
    public class VMPDFVenta
    {
        public VMNegocio? negocio   { get; set; }
        public VMVenta? venta       { get; set; }
        public List<VMDetalleVenta> Detalleventa { get; set; }

        public VMCliente? cliente { get; set; }
    }
}
