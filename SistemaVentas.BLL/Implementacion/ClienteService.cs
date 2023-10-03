using Microsoft.EntityFrameworkCore;
using SistemaVentas.BLL.Interfaces;
using SistemaVentas.DAL.Interfaces;
using SistemaVentas.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.BLL.Implementacion
{
    public class ClienteService: IClienteService
    {
        private readonly IGenericRepository<Cliente> _repositorio;

        public ClienteService(IGenericRepository<Cliente> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Cliente>> Lista()
        {
            IQueryable<Cliente> query = await _repositorio.Consultar();
            return query.Include(t => t.IdTipoIdentificacionNavigation).ToList();
        }

        public async Task<Cliente> Crear(Cliente entidad)
        {

           Cliente cliente_existe = await _repositorio.Obtener(u => u.NumeroIdentificacion == entidad.NumeroIdentificacion);

            if (cliente_existe != null)
                throw new TaskCanceledException("El numero de identificacion ya hiciste");

            try
            {
                Cliente cliente_creado = await _repositorio.Crear(entidad);      

                if (cliente_creado.IdCliente == 0)
                    throw new TaskCanceledException("No se pudo crear el cliente");

                IQueryable<Cliente> query = await _repositorio.Consultar(u => u.IdCliente == cliente_creado.IdCliente);
                cliente_creado = query.Include(r => r.IdTipoIdentificacionNavigation).First();


                return cliente_creado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Cliente> Editar(Cliente entidad)
        {
            try
            {

                IQueryable<Cliente> queryCliente = await _repositorio.Consultar(u => u.IdCliente == entidad.IdCliente);

                Cliente cliente_encontrado = await _repositorio.Obtener(c => c.IdCliente == entidad.IdCliente);
                cliente_encontrado.Direccion = entidad.Direccion;
                cliente_encontrado.PaySa = entidad.PaySa;
                cliente_encontrado.PnySn = entidad.PnySn;
                cliente_encontrado.IdTipoIdentificacionNavigation = entidad.IdTipoIdentificacionNavigation;
                cliente_encontrado.TelefonoPrincipal = entidad.TelefonoPrincipal;
                cliente_encontrado.TelefonoSecundario = entidad.TelefonoSecundario;
                cliente_encontrado.EsActivo = entidad.EsActivo;

                bool resp = await _repositorio.Editar(cliente_encontrado);

                if (!resp) throw new TaskCanceledException("No se pudo modificar el cliente");

                Cliente cliente_editado = queryCliente.Include(r => r.IdTipoIdentificacionNavigation).First();

                return cliente_editado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idCliente)
        {
            try
            {
                Cliente cliente_encontrado = await _repositorio.Obtener(c => c.IdCliente == idCliente);

                if (cliente_encontrado == null) throw new TaskCanceledException("El cliente NO existe");

                bool resp = await _repositorio.Eliminar(cliente_encontrado);

                return resp;

            }catch
            { throw; }
        }

       
        public async Task<Cliente> ObtenerPorID(int idCliente)
        {
            IQueryable<Cliente> query = await _repositorio.Consultar(c => c.IdCliente == idCliente);

            Cliente result = query.Include(n => n.IdTipoIdentificacionNavigation).FirstOrDefault();

            return result;
        }
    }
}
