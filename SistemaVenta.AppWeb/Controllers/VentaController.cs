using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.AppWeb.Models.ViewModels;
using SistemaVentas.AppWeb.Utilidades.Response;
using SistemaVentas.BLL.Interfaces;
using SistemaVentas.Entity;
using System.Security.Claims;

namespace SistemaVentas.AppWeb.Controllers
{
    [Authorize]
    public class VentaController : Controller
    {
        private readonly ITipoDocumentoVentaService _documentoVentaService;
        private readonly IClienteService _clienteService;
        private readonly IVentaService _ventaService;
        private readonly IMapper _mapper;
        private readonly IConverter _converter;

        public VentaController(
            ITipoDocumentoVentaService documentoVentaService, 
            IVentaService ventaService, 
            IMapper mapper, 
            IConverter converter,
            IClienteService clienteService)
        {
            _documentoVentaService = documentoVentaService;
            _ventaService = ventaService;
            _mapper = mapper;
            _converter = converter;
            _clienteService = clienteService;

        }

        public IActionResult NuevaVenta()
        {
            return View();
        }

        public IActionResult HistorialVenta()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaTipoDocumentoVenta()
        {
            List<VMTipoDocumentoVenta> vmListaTipoDocumento = _mapper.Map<List<VMTipoDocumentoVenta>>(await _documentoVentaService.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaTipoDocumento);
        }


        [HttpGet]
        public async Task<IActionResult> ListaCliente()
        {
            List<VMCliente> vmListaCliente = _mapper.Map<List<VMCliente>>(await _clienteService.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaCliente);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProductos(string busqueda)
        {
            List<VMProducto> vmListaProductos = _mapper.Map<List<VMProducto>>(await _ventaService.ObtenerProducto(busqueda));
            return StatusCode(StatusCodes.Status200OK, vmListaProductos);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarVenta([FromBody]VMVenta modelo)
        {
            GenericResponse<VMVenta> gResponse = new GenericResponse<VMVenta>();

            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

                string idUsuario = claimUser.Claims
                     .Where(c => c.Type == ClaimTypes.NameIdentifier)
                     .Select(c => c.Value).SingleOrDefault();

                modelo.IdUsuarioRegistro = int.Parse(idUsuario);

                if (modelo.idCliente == null)
                {
                    gResponse.Estado = false;
                    gResponse.Mensaje = "El cliente es requerido.";
                    return StatusCode(StatusCodes.Status200OK, gResponse);
                }

                Venta venta_creada = await _ventaService.Registrar(_mapper.Map<Venta>(modelo));
                modelo = _mapper.Map<VMVenta>(venta_creada);

                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch(Exception ex)
            {
                gResponse.Estado = false;
                //gResponse.Mensaje = ex.Message;
                gResponse.Mensaje = ex.InnerException.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);

        }

        [HttpGet]
        public async Task<IActionResult> Historial(string numeroVenta, string fechaInicio, string fechaFin)
        {
           
            List<VMDetalleVenta> vmHistorialVenta = _mapper.Map<List<VMDetalleVenta>>(await _ventaService.Historial(numeroVenta, fechaInicio, fechaFin));

            return StatusCode(StatusCodes.Status200OK, vmHistorialVenta);

        }

        public IActionResult MostrarPDFVenta(string numeroVenta)
        {
            string urlPlantillaVista = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/PDFVenta?numeroVenta={numeroVenta}";
            //string urlPlantillaVista = $"{this.Request.Scheme}://eatv21-001-site1.ftempurl.com/Plantilla/PDFVenta?numeroVenta={numeroVenta}";

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                },
                Objects = { 
                    new ObjectSettings()
                    {
                        Page = urlPlantillaVista
                    }
                }
            };

            var archivoPDF =  _converter.Convert(pdf);

            return File(archivoPDF, "application/pdf");
        }

    }

}

