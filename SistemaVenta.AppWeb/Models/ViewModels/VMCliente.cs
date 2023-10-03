namespace SistemaVentas.AppWeb.Models.ViewModels
{
    public class VMCliente
    {
        public int IdCliente { get; set; }

        public string? PNySN { get; set; }

        public string? PAySA { get; set; }

        public string? Direccion { get; set; }

        public string? TelefonoPrincipal { get; set; }

        public string? TelefonoSecundario { get; set; }

        public string? numeroIdentificacion { get; set; }

        public int IdTipoIdentificacion { get; set; }

        public string? nombreIdentificacion { get; set; }

        public int? EsActivo { get; set; }
    }
}
