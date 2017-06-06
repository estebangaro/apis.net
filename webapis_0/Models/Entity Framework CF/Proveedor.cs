using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapis_0.Models.Entity_Framework_CF
{
    public class Proveedor
    {
        // Code First Convention - PK
        public int ProveedorId { get; set; }
        public string Nombre { get; set; }
        public DateTime Registro { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Telefono2 { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}