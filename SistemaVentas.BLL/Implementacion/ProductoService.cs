﻿using Microsoft.EntityFrameworkCore;
using SistemaVentas.BLL.Interfaces;
using SistemaVentas.DAL.Interfaces;
using SistemaVentas.Entity;

namespace SistemaVentas.BLL.Implementacion
{
    public class ProductoService : IProductoService
    {

        private readonly IGenericRepository<Producto> _repositorio;
        private readonly IFireBaseService _firebaseServicio;


        public ProductoService(IGenericRepository<Producto> repositorio, IFireBaseService firebaseServicio)
        {
            _repositorio = repositorio;
            _firebaseServicio = firebaseServicio;
        }

        public async Task<List<Producto>> Lista()
        {
            IQueryable<Producto> query = await _repositorio.Consultar();
            return query.Include(c => c.IdCategoriaNavigation).Include(c => c.IdMarcaNavigation).ToList();
        }

        public async Task<Producto> Crear(Producto entidad, Stream imagen = null, string NombreImagen = "")
        {
            Producto producto_existe = await _repositorio.Obtener(p => p.CodigoBarra == entidad.CodigoBarra);

            if (producto_existe != null)
                throw new TaskCanceledException("El codigo de barra ya existe");
            try
            {

                entidad.NombreImagen = NombreImagen;
                if(imagen != null)
                {
                    string urlImage = await _firebaseServicio.SubirStorage(imagen, "carpeta_producto", NombreImagen);
                    entidad.UrlImagen = urlImage;
                }

                Producto producto_creado = await _repositorio.Crear(entidad);

                if(producto_creado.Idproducto == 0)
                    throw new TaskCanceledException("No se pudo crear el producto");

                IQueryable<Producto> query = await _repositorio.Consultar(p => p.Idproducto == producto_creado.Idproducto);

                producto_creado = query.Include(c => c.IdCategoriaNavigation).Include(c => c.IdMarcaNavigation).First();
                    
                return producto_creado;

            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        public async Task<Producto> Editar(Producto entidad, Stream imagen = null, string NombreImagen = "")
        {
            Producto producto_existe = await _repositorio.Obtener(p => p.CodigoBarra == entidad.CodigoBarra && p.Idproducto != entidad.Idproducto);
     
            if(producto_existe != null)
                throw new TaskCanceledException("El codigo de barra ya existe");

            try
            {
                IQueryable<Producto> queryProducto = await _repositorio.Consultar(p => p.Idproducto == entidad.Idproducto);

                Producto producto_para_editar = queryProducto.First();
                producto_para_editar.Descripcion = entidad.Descripcion;
                producto_para_editar.IdMarca = entidad.IdMarca;
                producto_para_editar.IdCategoria = entidad.IdCategoria;
                producto_para_editar.Stock = entidad.Stock;
                producto_para_editar.Precio = entidad.Precio;
                producto_para_editar.EsActivo = entidad.EsActivo;

                if(producto_para_editar.NombreImagen == "")
                {
                    producto_para_editar.NombreImagen = NombreImagen;
                }

                if(imagen != null)
                {
                    string urlImagen = await _firebaseServicio.SubirStorage(imagen, "carpeta_producto", producto_para_editar.NombreImagen);
                    producto_para_editar.UrlImagen = urlImagen;
                }

                bool respuesta = await _repositorio.Editar(producto_para_editar);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar el producto");

                Producto producto_editado = queryProducto.Include(p => p.IdCategoriaNavigation).Include(p => p.IdMarcaNavigation).First();
                return producto_editado;

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idProducto)
        {
            try
            {
                Producto producto_encontrado = await _repositorio.Obtener(c => c.Idproducto == idProducto);

                if (producto_encontrado == null) throw new TaskCanceledException("El producto no existe");

                string nombreImagen = producto_encontrado.NombreImagen;

                bool respuesta = await _repositorio.Eliminar(producto_encontrado);

                if (respuesta)

                    await _firebaseServicio.EliminarStorage("carpeta_producto", nombreImagen);

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
