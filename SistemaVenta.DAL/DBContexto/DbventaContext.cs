using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Entity;

namespace SistemaVentas.DAL.DBContexto;

public partial class DbventaContext : DbContext
{
    public DbventaContext()
    {
    }

    public DbventaContext(DbContextOptions<DbventaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Configuracion> Configuracions { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }

    public virtual DbSet<MarcaProducto> MarcaProductos { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Negocio> Negocios { get; set; }

    public virtual DbSet<NumeroCorrelativo> NumeroCorrelativos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolMenu> RolMenus { get; set; }

    public virtual DbSet<TipoDocumentoVenta> TipoDocumentoVenta { get; set; }

    public virtual DbSet<TipoIdentificacion> TipoIdentificacions { get; set; }

    public virtual DbSet<TipoPago> TipoPagos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=SQL5103.site4now.net;initial catalog=db_a985c6_dbventa;USER ID=db_a985c6_dbventa_admin; Password=eatv1234; Persist Security Info=true; TrustServerCertificate=true;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__8A3D240CFFA4F569");

            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.nombreCategoria)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombreCategoria");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__885457EEFEB05E6A");

            entity.ToTable("Cliente");

            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
            entity.Property(e => e.IdTipoIdentificacion).HasColumnName("idTipoIdentificacion");
            entity.Property(e => e.NumeroIdentificacion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("numeroIdentificacion");
            entity.Property(e => e.PaySa)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PAySA");
            entity.Property(e => e.PnySn)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PNySN");
            entity.Property(e => e.TelefonoPrincipal)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Telefono_Principal");
            entity.Property(e => e.TelefonoSecundario)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Telefono_Secundario");

            entity.HasOne(d => d.IdTipoIdentificacionNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdTipoIdentificacion)
                .HasConstraintName("FK__Cliente__idTipoI__489AC854");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.ClienteIdUsuarioModificacionNavigations)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("FK__Cliente__IdUsuar__4A8310C6");

            entity.HasOne(d => d.IdUsuarioRegistroNavigation).WithMany(p => p.ClienteIdUsuarioRegistroNavigations)
                .HasForeignKey(d => d.IdUsuarioRegistro)
                .HasConstraintName("FK__Cliente__IdUsuar__498EEC8D");
        });

        modelBuilder.Entity<Configuracion>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Configuracion");

            entity.Property(e => e.Propiedad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("propiedad");
            entity.Property(e => e.Recurso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("recurso");
            entity.Property(e => e.Valor)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("valor");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__DetalleV__BFE2843F09822B0F");

            entity.Property(e => e.IdDetalleVenta).HasColumnName("idDetalleVenta");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.ImporteCambio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ImporteRecibido).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ImpuestoTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("impuestoTotal");
            entity.Property(e => e.Precio_unitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_unitario");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("subtotal");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__DetalleVe__idPro__756D6ECB");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__DetalleVe__idVen__74794A92");
        });

        modelBuilder.Entity<MarcaProducto>(entity =>
        {
            entity.HasKey(e => e.IdMarcaProducto).HasName("PK__MarcaPro__41A13D08BCED3F2B");

            entity.ToTable("MarcaProducto");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__Menu__C26AF4831AFD4438");

            entity.ToTable("Menu");

            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.Controlador)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("controlador");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Icono)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("icono");
            entity.Property(e => e.IdMenuPadre).HasColumnName("idMenuPadre");
            entity.Property(e => e.PaginaAccion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("paginaAccion");

            entity.HasOne(d => d.IdMenuPadreNavigation).WithMany(p => p.InverseIdMenuPadreNavigation)
                .HasForeignKey(d => d.IdMenuPadre)
                .HasConstraintName("FK__Menu__idMenuPadr__24927208");
        });

        modelBuilder.Entity<Negocio>(entity =>
        {
            entity.HasKey(e => e.IdNegocio).HasName("PK__Negocio__70E1E107CD5775A5");

            entity.ToTable("Negocio");

            entity.Property(e => e.IdNegocio)
                .ValueGeneratedNever()
                .HasColumnName("idNegocio");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreLogo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreLogo");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numeroDocumento");
            entity.Property(e => e.PorcentajeImpuesto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("porcentajeImpuesto");
            entity.Property(e => e.SimboloMoneda)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("simboloMoneda");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.UrlLogo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("urlLogo");
        });

        modelBuilder.Entity<NumeroCorrelativo>(entity =>
        {
            entity.HasKey(e => e.IdNumeroCorrelativo).HasName("PK__NumeroCo__25FB547EB7543A69");

            entity.ToTable("NumeroCorrelativo");

            entity.Property(e => e.IdNumeroCorrelativo).HasColumnName("idNumeroCorrelativo");
            entity.Property(e => e.CantidadDigitos).HasColumnName("cantidadDigitos");
            entity.Property(e => e.FechaActualizacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaActualizacion");
            entity.Property(e => e.Gestion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("gestion");
            entity.Property(e => e.UltimoNumero).HasColumnName("ultimoNumero");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Idproducto).HasName("PK__Producto__9E7C0D59726790B3");

            entity.ToTable("Producto");

            entity.Property(e => e.CodigoBarra)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("codigoBarra");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.NombreImagen)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("nombreImagen");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.UrlImagen)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("urlImagen");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Producto__idCate__6FB49575");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("FK__Producto__IdMarc__6EC0713C");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__Producto__idProv__70A8B9AE");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__A3FA8E6B12BF7A54");

            entity.ToTable("Proveedor");

            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__3C872F76DDA6764C");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
        });

        modelBuilder.Entity<RolMenu>(entity =>
        {
            entity.HasKey(e => e.IdRolMenu).HasName("PK__RolMenu__CD2045D81B632D65");

            entity.ToTable("RolMenu");

            entity.Property(e => e.IdRolMenu).HasColumnName("idRolMenu");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.IdRol).HasColumnName("idRol");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.RolMenus)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK__RolMenu__idMenu__2C3393D0");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.RolMenus)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__RolMenu__idRol__2B3F6F97");
        });

        modelBuilder.Entity<TipoDocumentoVenta>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumentoVenta).HasName("PK__TipoDocu__A9D59AEE7B49F4D2");

            entity.Property(e => e.IdTipoDocumentoVenta).HasColumnName("idTipoDocumentoVenta");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
        });

        modelBuilder.Entity<TipoIdentificacion>(entity =>
        {
            entity.HasKey(e => e.IdTipoIdentificacion).HasName("PK__TipoIden__3F0ADFF62AD89E30");

            entity.ToTable("TipoIdentificacion");

            entity.Property(e => e.IdTipoIdentificacion).HasColumnName("idTipoIdentificacion");
            entity.Property(e => e.nombreIdentificacion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombreIdentificacion");
        });

        modelBuilder.Entity<TipoPago>(entity =>
        {
            entity.HasKey(e => e.IdTipoPago).HasName("PK__TipoPago__EB0AA9E75EA48374");

            entity.ToTable("TipoPago");

            entity.Property(e => e.NombreTipoPago)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombreTipoPago");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A62E7F7006");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Clave)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.IdUsuarioModificacion).HasColumnName("idUsuarioModificacion");
            entity.Property(e => e.IdUsuarioRegistro).HasColumnName("idUsuarioRegistro");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombreCompleto");
            entity.Property(e => e.NombreFoto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreFoto");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.UrlFoto)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("urlFoto");
            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                           .HasForeignKey(d => d.IdRol)
                           .HasConstraintName("FK__Usuario__idRol__300424B4");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Venta__077D561433350A47");

            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdTipoDocumentoVenta).HasColumnName("idTipoDocumentoVenta");
            entity.Property(e => e.NumeroVenta)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("numeroVenta");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Venta__idCliente__503BEA1C");

            entity.HasOne(d => d.IdTipoDocumentoVentaNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdTipoDocumentoVenta)
                .HasConstraintName("FK__Venta__idTipoDoc__4F47C5E3");

            entity.HasOne(d => d.IdTipoPagoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdTipoPago)
                .HasConstraintName("FK__Venta__IdTipoPag__51300E55");

            entity.HasOne(d => d.IdUsuarioRegistroNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdUsuarioRegistro)
                .HasConstraintName("FK__Venta__IdUsuario__5224328E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
