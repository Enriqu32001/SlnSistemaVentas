using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVentas.Entity;

namespace SistemaVentas.BLL.Interfaces
{
    public interface ITIpoIdentificacionService
    {
        Task<List<TipoIdentificacion>> Lista();
    }
}
