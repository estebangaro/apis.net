using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapis_0.Models.Entity_Framework_CF
{
    public class Linea_Factura
    {
        public int ITEM { get; set; }
        public decimal Cantidad { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
        public string Unidad { get; set; }

        public int Cabecero { get; set; }
        public int Proveedor { get; set; }
        public virtual Factura Factura { get; set; }
    }
}