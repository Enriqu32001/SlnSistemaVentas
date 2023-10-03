using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.AppWeb.Models.ViewModels;

using SistemaVentas.AppWeb.Utilidades.Response;
using SistemaVentas.BLL.Interfaces;
using SistemaVentas.Entity;

namespace SistemaVentas.AppWeb.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IClienteService _clienteService;

        public ClienteController(IMapper mapper, IClienteService clienteService)
        {
            _mapper = mapper;
            _clienteService = clienteService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMCliente> vmclientelista = _mapper.Map<List<VMCliente>>(await _clienteService.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmclientelista }); // 'data', 

        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMCliente modelo)
        {
            GenericResponse<VMCliente> gResponse = new GenericResponse<VMCliente>();

            try
            {
                Cliente cliente_creado = await _clienteService.Crear(_mapper.Map<Cliente>(modelo));
                modelo = _mapper.Map<VMCliente>(cliente_creado);

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
        public async Task<IActionResult> Editar([FromBody] VMCliente modelo)
        {
            GenericResponse<VMCliente> gResponse = new GenericResponse<VMCliente>();

            try
            {
                Cliente cliente_editado = await _clienteService.Editar(_mapper.Map<Cliente>(modelo));
                modelo = _mapper.Map<VMCliente>(cliente_editado);

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
        public async Task<IActionResult> Eliminar(int idCliente)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();

            try
            {
                gResponse.Estado = await _clienteService.Eliminar(idCliente);

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
