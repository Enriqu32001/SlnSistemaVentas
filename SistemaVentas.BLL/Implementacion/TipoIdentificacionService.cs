using SistemaVentas.BLL.Interfaces;
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
    public class TipoIdentificacionService : ITIpoIdentificacionService
    {
        private readonly IGenericRepository<TipoIdentificacion> _repositorio;

        public TipoIdentificacionService(IGenericRepository<TipoIdentificacion> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<TipoIdentificacion>> Lista()
        {
            IQueryable<TipoIdentificacion> query = await _repositorio.Consultar();
            return query.ToList();
        }
    }
}
