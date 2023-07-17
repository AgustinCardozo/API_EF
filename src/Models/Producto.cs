using System;
using System.Collections.Generic;

namespace API_EF.Models
{
    public partial class Producto
    {
        public string ProdCodigo { get; set; }
        public string ProdDetalle { get; set; }
        public decimal? ProdPrecio { get; set; }
        public string ProdFamilia { get; set; }
        public string ProdRubro { get; set; }
        public decimal? ProdEnvase { get; set; }
    }
}
