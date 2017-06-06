using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using webapis_0.Models.Entity_Framework_CF;

namespace webapis_0.Models.Configuraciones_Fluent_API
{
    public class Linea_FacturaConfig: EntityTypeConfiguration<Linea_Factura>
    {
        public Linea_FacturaConfig()
        {
            ToTable("Detalle_Factura", "demos");
            HasKey(_item => new { _item.ITEM, _item.Cabecero, _item.Proveedor });
            Property(_item => _item.Cantidad)
                .HasPrecision(8, 2)
                .IsRequired();
            Property(_item => _item.Descripcion)
                .IsUnicode()
                .HasMaxLength(50)
                .IsRequired();
            Property(_item => _item.PrecioUnitario)
                .HasPrecision(8, 2)
                .IsRequired();
            Property(_item => _item.Total)
                .HasPrecision(8, 2)
                .IsRequired();
            Property(_item => _item.Unidad)
                .IsUnicode()
                .HasMaxLength(2)
                .IsRequired()
                .IsFixedLength();

            HasRequired(_item => _item.Factura)
                .WithMany(_fac => _fac.Detalle)
                .HasForeignKey(_item => new { _item.Cabecero, _item.Proveedor });
        }
    }
}