﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVentas.Entity;

namespace SistemaVentas.BLL.Interfaces
{
    public interface IVentaService
    {
        Task<List<Producto>> ObtenerProducto(string busquedad);
        Task<Venta> Registrar(Venta entidad);
        Task<List<DetalleVenta>> Historial(string numeroVenta, string fechaInicio, string fechaFin);
        Task<Venta> Detalle(string numeroVenta);

        Task<List<DetalleVenta>> DetalleV(string numeroVenta);

        Task<List<DetalleVenta>> Reporte(string fechaInicio, string fechaFin);
    }
}
