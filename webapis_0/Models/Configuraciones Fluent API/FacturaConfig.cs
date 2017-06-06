using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using webapis_0.Models.Entity_Framework_CF;

namespace webapis_0.Models.Configuraciones_Fluent_API
{
    public class FacturaConfig : EntityTypeConfiguration<Factura>
    {
        public FacturaConfig()
        {
            ToTable("Facturas", "demos");
            HasKey(_fac => new { _fac.NumeroFactura, _fac.ProveedorId });
            Property(_fac => _fac.NumeroFactura)
                .HasColumnName("Numero");
            Property(_fac => _fac.CFDI)
                .IsUnicode()
                .HasMaxLength(20)
                .IsRequired();
            Property(_fac => _fac.RFC_Emisor)
                .IsUnicode()
                .HasMaxLength(15)
                .IsRequired();
            Property(_fac => _fac.RFC_Receptor)
                .IsUnicode()
                .HasMaxLength(15)
                .IsRequired();
            Property(_fac => _fac.Fecha)
                .IsRequired();
            Property(_fac => _fac.Importe)
                .HasPrecision(8, 2)
                .IsRequired();
            Property(_fac => _fac.IVA)
                .HasPrecision(5, 2)
                .IsOptional();
            Property(_fac => _fac.ISR)
                .HasPrecision(5, 2)
                .IsOptional();
            Property(_fac => _fac.Total)
                .HasPrecision(8, 2)
                .IsRequired();
        }
    }
}