using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVentas.BLL.Interfaces;
using SistemaVentas.DAL.Interfaces;
using SistemaVentas.Entity;

namespace SistemaVentas.BLL.Implementacion
{
    public class MarcaProductoService : IMarcaService

    {
        private readonly IGenericRepository<MarcaProducto> _repositorio;

        public MarcaProductoService(IGenericRepository<MarcaProducto> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<MarcaProducto>> Lista()
        {
            IQueryable<MarcaProducto> query = await _repositorio.Consultar();
            return query.ToList();
        }

        public async Task<MarcaProducto> Crear(MarcaProducto entidad)
        {
            try
            {
                MarcaProducto marca_creada = await _repositorio.Crear(entidad);
                if (marca_creada.IdMarcaProducto == 0)
                    throw new TaskCanceledException("No se pudo crear la marca");

                return marca_creada;
            }
            catch
            {
                throw;
            }
        }

        public async Task<MarcaProducto> Editar(MarcaProducto entidad)
        {
            try
            {
                MarcaProducto marca = await _repositorio.Obtener(c => c.IdMarcaProducto == entidad.IdMarcaProducto);
                marca.Nombre = entidad.Nombre;
                marca.EsActivo = entidad.EsActivo;

                bool resp = await _repositorio.Editar(marca);

                if(!resp) throw new TaskCanceledException("No se pudo editar la marca");

                return marca;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idMarca)
        {
            try
            {
                MarcaProducto marca_encontrada = await _repositorio.Obtener(c => c.IdMarcaProducto == idMarca);

                if(marca_encontrada == null) throw new TaskCanceledException("La marca no existe");

                bool resp = await _repositorio.Eliminar(marca_encontrada);

                return resp;

            }
            catch
            {
                throw;
            }
        }


    }
}
