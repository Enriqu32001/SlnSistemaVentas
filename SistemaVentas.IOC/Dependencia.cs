using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVentas.DAL.DBContexto;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.DAL.Implementacion;
using SistemaVentas.DAL.Interfaces;
using SistemaVentas.BLL.Interfaces;
using SistemaVentas.BLL.Implementacion;


namespace SistemaVentas.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbventaContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CadenaSQL"));
            });

            services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<IVentaRepository, VentaRepository>();

            services.AddScoped<ICorreoServices, CorreoService>();
            services.AddScoped<IFireBaseService, FireBaseService>();
            
            services.AddScoped<IUtilidadesService, UtilidadesService>();
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<ITIpoIdentificacionService, TipoIdentificacionService>();

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IClienteService, ClienteService>();

            services.AddScoped<INegocioService, NegocioService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IMarcaService, MarcaProductoService>();

            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<ITipoDocumentoVentaService, TipoDocumentoVentaService>();
            services.AddScoped<IVentaService, VentaService>();

            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IMenuService, MenuService>();



        }
    }
}
