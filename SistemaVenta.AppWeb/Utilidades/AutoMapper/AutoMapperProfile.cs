using SistemaVentas.AppWeb.Models.ViewModels;
using SistemaVentas.Entity;
using System.Globalization;
using AutoMapper;

namespace SistemaVentas.AppWeb.Utilidades.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            //    Origen, Destino
            CreateMap<Rol, VMRol>().ReverseMap();
            #endregion Rol

            #region usuario
            CreateMap<Usuario, VMUsuarios>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                )
                .ForMember(destino =>
                destino.NombreRol,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Descripcion)
                );

            CreateMap<VMUsuarios, Usuario>()
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                )
                .ForMember(destino =>
                    destino.IdRolNavigation,
                    opt => opt.Ignore()
                );

            #endregion Usuario

            #region negocio
            CreateMap<Negocio, VMNegocio>()
                .ForMember(destino =>
                    destino.PorcentajeImpuesto,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.PorcentajeImpuesto.Value, new CultureInfo("es-NIC")))
                );

            CreateMap<VMNegocio, Negocio>()
                .ForMember(destino =>
                    destino.PorcentajeImpuesto,
                     opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PorcentajeImpuesto, new CultureInfo("es-NIC")))
                );
            #endregion negocio

            #region categoria
            CreateMap<Categoria, VMCategoria>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );

            CreateMap<VMCategoria, Categoria>()
                .ForMember(destino =>
                     destino.EsActivo,
                        opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                 );
            #endregion Categoria

            #region cliente
            CreateMap<Cliente, VMCliente>()
               .ForMember(destino =>
                   destino.EsActivo,
                   opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
               )
             .ForMember(destino =>
                   destino.nombreIdentificacion,
                    opt => opt.MapFrom(origen => origen.IdTipoIdentificacionNavigation.nombreIdentificacion)
                 )
                 .ForMember(destino =>
                   destino.numeroIdentificacion,
                    opt => opt.MapFrom(origen => origen.NumeroIdentificacion)
                 );

            CreateMap<VMCliente, Cliente>()
                .ForMember(destino =>
                     destino.EsActivo,
                        opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                 )
                .ForMember(destino =>
                      destino.IdTipoIdentificacionNavigation,
                          opt => opt.Ignore()
                  );
            #endregion

            #region Marca
            CreateMap<MarcaProducto, VMMarcaProducto>()
                .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );

            CreateMap<VMMarcaProducto, MarcaProducto>()
                .ForMember(destino =>
                     destino.EsActivo,
                        opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                 );
            #endregion

            #region Producto
            CreateMap<Producto, VMProducto>()
               .ForMember(destino =>
                   destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                )
             .ForMember(destino =>
                   destino.NombreCategoria,
                    opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.nombreCategoria)
                 )
                .ForMember(destino =>
                   destino.NombreMarca,
                    opt => opt.MapFrom(origen => origen.IdMarcaNavigation.Nombre)
                 )
             .ForMember(destino =>
                   destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PER")))
                 );


            CreateMap<VMProducto, Producto>()
               .ForMember(destino =>
                     destino.EsActivo,
                         opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                  )
                .ForMember(destino =>
                      destino.IdCategoriaNavigation,
                          opt => opt.Ignore()
                  )
                  .ForMember(destino =>
                      destino.IdMarcaNavigation,
                          opt => opt.Ignore()
                  )
                .ForMember(destino =>
                    destino.Precio,
                         opt => opt.MapFrom(origen => Convert.ToString(origen.Precio, new CultureInfo("es-PER")))
                 );
            #endregion

            #region TipoDocumentoVenta
            CreateMap<TipoDocumentoVenta, VMTipoDocumentoVenta>().ReverseMap();
            #endregion

            #region tipoDocumentoCliente
            CreateMap<TipoIdentificacion, VMTipoIdentificacion>().ReverseMap();
            #endregion

            #region Venta
            CreateMap<Venta, VMVenta>()
                 .ForMember(destino =>
                   destino.TipoDocumentoVenta,
                    opt => opt.MapFrom(origen => origen.IdTipoDocumentoVentaNavigation.Descripcion)
                )
                 .ForMember(destino =>
                   destino.Usuario,
                    opt => opt.MapFrom(origen => origen.IdUsuarioRegistroNavigation.NombreCompleto)
                )
                 .ForMember(destino =>
                   destino.NombreCliente,
                    opt => opt.MapFrom(origen => origen.IdClienteNavigation.PnySn + " " + origen.IdClienteNavigation.PaySa)
                )
                  .ForMember(destino =>
                   destino.NumeroIdentificacion,
                    opt => opt.MapFrom(origen => origen.IdClienteNavigation.NumeroIdentificacion)
                )

                 .ForMember(destino =>
                   destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PER")))
                 )
                .ForMember(destino =>
                   destino.FechaRegistro,
                    opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                 );

            CreateMap<VMVenta, Venta>()
                 .ForMember(destino =>
                   destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-PER")))
                 );
            #endregion

            #region DetalleVenta
            CreateMap<DetalleVenta, VMDetalleVenta>()
                .ForMember(destino =>
                    destino.Precio_unitario,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio_unitario.Value, new CultureInfo("es-PER")))
                )
                .ForMember(destino =>
                   destino.ImporteRecibido,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.ImporteRecibido.Value, new CultureInfo("es-PER")))
                 )
                  .ForMember(destino =>
                   destino.ImporteCambio,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.ImporteCambio.Value, new CultureInfo("es-PER")))
                 )
                  .ForMember(destino =>
                   destino.subtotal,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Subtotal.Value, new CultureInfo("es-PER")))
                 )
                  .ForMember(destino =>
                   destino.impuestoTotal,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.ImpuestoTotal.Value, new CultureInfo("es-PER")))
                 )
                .ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PER")))
                )
                   .ForMember(destino =>
                    destino.FechaRegistro,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                )
                 .ForMember(destino =>
                    destino.NumeroVenta,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.NumeroVenta)
                )
                 .ForMember(destino =>
                    destino.TipoDocumento,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.IdClienteNavigation.IdTipoIdentificacionNavigation.nombreIdentificacion)
                )
                .ForMember(destino =>
                    destino.DocumentoCliente,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.IdClienteNavigation.NumeroIdentificacion)
                )
                .ForMember(destino =>
                    destino.NombreCliente,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.IdClienteNavigation.PnySn + " " + origen.IdVentaNavigation.IdClienteNavigation.PaySa)
                )
                .ForMember(destino =>
                   destino.Usuario,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.IdUsuarioRegistroNavigation.NombreCompleto)

                ).ForMember(destino =>
                   destino.Descripcion,
                    opt => opt.MapFrom(origen => origen.IdProductoNavigation.Descripcion)
                ).ForMember(destino =>
                   destino.CategoriaProducto,
                    opt => opt.MapFrom(origen => origen.IdProductoNavigation.IdCategoriaNavigation.nombreCategoria)
                ).ForMember(destino =>
                   destino.MarcaProducto,
                    opt => opt.MapFrom(origen => origen.IdProductoNavigation.IdMarcaNavigation.Nombre)
                );

            CreateMap<VMDetalleVenta, DetalleVenta>()
              .ForMember(destino =>
                 destino.Precio_unitario,
                 opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio_unitario, new CultureInfo("es-PER")))
                )
                  .ForMember(destino =>
                   destino.ImporteCambio,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.ImporteRecibido, new CultureInfo("es-PER")))
                 )
                  .ForMember(destino =>
                   destino.ImporteRecibido,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.ImporteRecibido, new CultureInfo("es-PER")))
                 )
              .ForMember(destino =>
                   destino.Subtotal,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.subtotal, new CultureInfo("es-PER")))
                 )
                  .ForMember(destino =>
                   destino.ImpuestoTotal,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.impuestoTotal, new CultureInfo("es-PER")))
                 )
              .ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-PER")))
                );
               

            CreateMap<DetalleVenta, VMReporteVenta>()
                 .ForMember(destino =>
                    destino.FechaRegistro,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destino =>
                    destino.NumeroVenta,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.NumeroVenta)
                )
                .ForMember(destino =>
                    destino.TipoDocumento,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.IdTipoDocumentoVentaNavigation.Descripcion)
                )
                .ForMember(destino =>
                    destino.DocumentoCliente,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.IdClienteNavigation.NumeroIdentificacion)
                )
                .ForMember(destino =>
                    destino.NombreCliente,
                    opt => opt.MapFrom(origen => origen.IdVentaNavigation.IdClienteNavigation.PnySn + " " + origen.IdVentaNavigation.IdClienteNavigation.PaySa)
                )
                .ForMember(destino =>
                    destino.SubtotalVenta,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Subtotal.Value, new CultureInfo("es-PER")))
                )
                .ForMember(destino =>
                    destino.ImpuestoTotalVenta,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.ImpuestoTotal.Value, new CultureInfo("es-PER")))
                )
                .ForMember(destino =>
                    destino.TotalVenta,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-PER")))
                )
                .ForMember(destino =>
                    destino.Producto,
                    opt => opt.MapFrom(origen => origen.IdProductoNavigation.Descripcion)
                )
                 .ForMember(destino =>
                    destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.IdProductoNavigation.Precio.Value, new CultureInfo("es-PER")))
                )
                 .ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PER")))
                );
            #endregion

            #region Menu
            CreateMap<Menu, VMMenu>()
                 .ForMember(destino =>
                    destino.SubMenus,
                    opt => opt.MapFrom(origen => origen.InverseIdMenuPadreNavigation)
                );
            #endregion
        }
    }
}
