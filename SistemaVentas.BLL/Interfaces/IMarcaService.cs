using SistemaVentas.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVentas.Entity;

namespace SistemaVentas.BLL.Interfaces
{
    public interface IMarcaService
    {
        Task<List<MarcaProducto>> Lista();
        Task<MarcaProducto> Crear(MarcaProducto entidad);
        Task<MarcaProducto> Editar(MarcaProducto entidad);
        Task<bool> Eliminar(int idMarca);
    }
}
