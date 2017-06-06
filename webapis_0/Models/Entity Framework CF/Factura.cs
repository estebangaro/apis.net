using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapis_0.Models.Entity_Framework_CF
{
    public class Factura
    {
        public int NumeroFactura { get; set; }
        public string CFDI { get; set; }
        public string RFC_Emisor { get; set; }
        public string RFC_Receptor { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Importe { get; set; }
        public Nullable<decimal> IVA { get; set; }
        public Nullable<decimal> ISR { get; set; }
        public decimal Total { get; set; }

        public int ProveedorId { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual ICollection<Linea_Factura> Detalle { get; set; }
    }
}