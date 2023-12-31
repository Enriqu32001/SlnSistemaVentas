﻿using SistemaVentas.Entity;

namespace SistemaVentas.AppWeb.Models.ViewModels
{
    public class VMProducto
    {
        public int IdProducto               { get; set; }

        public string? CodigoBarra          { get; set; }

        public int? IdMarca                 { get; set; }

        public string? NombreMarca       { get; set; }

        public string? Descripcion          { get; set; }

        public int? IdCategoria             { get; set; }

        public string? NombreCategoria      { get; set; }

        public int? Stock                   { get; set; }

        public string? UrlImagen            { get; set; }

        public string? Precio              { get; set; }

        public int? EsActivo                { get; set; }
    }
}
