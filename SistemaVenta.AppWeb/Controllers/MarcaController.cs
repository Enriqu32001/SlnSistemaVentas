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
    public class MarcaController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IMarcaService _marcaService;

        public MarcaController(IMapper mapper, IMarcaService marcaService)
        {
            _mapper = mapper;
            _marcaService = marcaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMMarcaProducto> vmMarcaLista = _mapper.Map<List<VMMarcaProducto>>(await _marcaService.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmMarcaLista });
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMMarcaProducto modelo)
        {
            GenericResponse<VMMarcaProducto> gResponse = new GenericResponse<VMMarcaProducto>();

            try
            {
                MarcaProducto marca_creada = await _marcaService.Crear(_mapper.Map<MarcaProducto>(modelo));
                modelo = _mapper.Map<VMMarcaProducto>(marca_creada);

                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] VMMarcaProducto modelo)
        {
            GenericResponse<VMMarcaProducto> gResponse = new GenericResponse<VMMarcaProducto>();

            try
            {
                MarcaProducto marca_editada = await _marcaService.Editar(_mapper.Map<MarcaProducto>(modelo));
                modelo = _mapper.Map<VMMarcaProducto>(marca_editada);

                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int idMarca)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();

            try
            {
                gResponse.Estado = await _marcaService.Eliminar(idMarca);
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
    }
}
