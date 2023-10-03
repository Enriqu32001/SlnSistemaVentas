using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaVentas.AppWeb.Models.ViewModels;
using SistemaVentas.BLL.Interfaces;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace SistemaVentas.AppWeb.Controllers
{
	[Authorize]
	public class PlantillaController : Controller
    {

        private readonly IMapper _mapper;
        private readonly INegocioService _negocioService;
        private readonly IVentaService _ventaService;

        public PlantillaController(IMapper mapper, INegocioService negocioService, IVentaService ventaService)
        {
            _mapper = mapper;
            _negocioService = negocioService;
            _ventaService = ventaService;
        }


        public IActionResult EnviarClave(string correo, string clave)
        {
            ViewData["Correo"] = correo;
            ViewData["Clave"] = clave;
            //ViewData["Url"] = $"{this.Request.Scheme}://eatv21-001-site1.ftempurl.com/";
            ViewData["Url"] = $"{this.Request.Scheme}://{this.Request.Host}";

            return View();
        }

        public async Task<IActionResult> PDFVenta(string numeroVenta)
        {
            VMVenta vmVenta = _mapper.Map<VMVenta>(await _ventaService.Detalle(numeroVenta));
            VMNegocio vmNegocio = _mapper.Map<VMNegocio>(await _negocioService.Obtener());
            List<VMDetalleVenta> vmHistorialVenta = _mapper.Map<List<VMDetalleVenta>>(await _ventaService.DetalleV(numeroVenta));


            VMPDFVenta modelo = new VMPDFVenta();

            modelo.negocio = vmNegocio;
            modelo.venta = vmVenta;
            modelo.Detalleventa = vmHistorialVenta;

            decimal res = 0;
            decimal rest = 0;
            foreach (var item in modelo.Detalleventa)
            {

                res += Convert.ToDecimal(item.subtotal, new CultureInfo("es-PER"));
                rest += Convert.ToDecimal(item.impuestoTotal, new CultureInfo("es-PER"));

            }

            modelo.venta.impuestototalTotal = rest.ToString();
            modelo.venta.subtotalTotal = res.ToString();

            return View(modelo);
        }

        public IActionResult RestablecerClave(string clave)
        {
            ViewData["Clave"] = clave;
            return View();
        }
    }
}
