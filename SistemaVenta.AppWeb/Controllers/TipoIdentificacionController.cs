using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SistemaVentas.AppWeb.Models.ViewModels;
using SistemaVentas.AppWeb.Utilidades.Response;
using SistemaVentas.BLL.Interfaces;
using SistemaVentas.Entity;
using Microsoft.AspNetCore.Authorization;

namespace SistemaVentas.AppWeb.Controllers
{
    [Authorize]
    public class TipoIdentificacionController : Controller
    {       
        private readonly IMapper _mapper;
        private readonly ITIpoIdentificacionService _tipoIdentificacion;

        public TipoIdentificacionController(IMapper mapper, ITIpoIdentificacionService tipoIdentificacion)
        {
            _mapper = mapper;
            _tipoIdentificacion = tipoIdentificacion;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMTipoIdentificacion> vmTipoIdentificacion = _mapper.Map<List<VMTipoIdentificacion>>(await _tipoIdentificacion.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmTipoIdentificacion });
        }
    }
}
