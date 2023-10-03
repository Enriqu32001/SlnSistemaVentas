using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SistemaVentas.BLL.Interfaces;
using SistemaVentas.DAL.Interfaces;
using SistemaVentas.Entity;

namespace SistemaVentas.BLL.Implementacion
{
    public class VentaService : IVentaService
    {

        private readonly IGenericRepository<Producto> _repositorioProducto;
        private readonly IVentaRepository _repositorioVenta;

        public VentaService(IGenericRepository<Producto> repositorioProducto, IVentaRepository repositorioVenta)
        {
            _repositorioProducto = repositorioProducto;
            _repositorioVenta = repositorioVenta;
        }


        public async Task<List<Producto>> ObtenerProducto(string busquedad)
        {
            //IQueryable<Producto> query = await _repositorioProducto.Consultar(
            //    p => p.EsActivo == true &&
            //    p.Stock > 0 &&
            //    string.Concat(p.CodigoBarra, p.IdMarca, p.Descripcion).Contains(busquedad));

            IQueryable<Producto> query = await _repositorioProducto.Consultar(
                p => p.EsActivo == true &&
                  p.Stock > 0 &&
                     (p.CodigoBarra.Contains(busquedad) ||
                        p.IdMarca.ToString().Contains(busquedad) ||
                         p.Descripcion.Contains(busquedad)));
            
            return query.Include(c => c.IdCategoriaNavigation).Include(c => c.IdMarcaNavigation).ToList();
        }

        public async Task<Venta> Registrar(Venta entidad)
        {
            try
            {
                return await _repositorioVenta.Registrar(entidad);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<DetalleVenta>> Historial(string numeroVenta, string fechaInicio, string fechaFin)
        {

            List<DetalleVenta> lista = await _repositorioVenta.DetalleVenta(numeroVenta, fechaInicio, fechaFin);

            return lista;
        }
        public async Task<Venta> Detalle(string numeroVenta)
        {
            IQueryable<Venta> query = await _repositorioVenta.Consultar(v => v.NumeroVenta == numeroVenta);

         return query
                .Include(tdv => tdv.IdTipoDocumentoVentaNavigation)
                .Include(u => u.IdUsuarioRegistroNavigation)
                .Include(dv => dv.DetalleVenta)
                .Include(c => c.IdClienteNavigation)
                .Include(tdp => tdp.IdTipoPagoNavigation)
                 .First();
        }

        public async Task<List<DetalleVenta>> DetalleV(string numeroVenta)
        {
            List<DetalleVenta> query = await _repositorioVenta.DetalleVenta(numeroVenta);

            return query;


         }
        
        public async Task<List<DetalleVenta>> Reporte(string fechaInicio, string fechaFin)
        {
            DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PER"));
            DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PER"));

            List<DetalleVenta> lista = await _repositorioVenta.Reporte(fecha_inicio, fecha_fin);

            return lista;
        }
    }
}
