﻿using SistemaVentas.Entity;

namespace SistemaVentas.AppWeb.Models.ViewModels
{
    public class VMCategoria
    {
        public int IdCategoria          { get; set; }

        public string? nombreCategoria      { get; set; }

        public int? EsActivo           { get; set; }
    }
}
