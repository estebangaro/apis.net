using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using webapis_0.Models.Entity_Framework_CF;

namespace webapis_0.Models.Configuraciones_Fluent_API
{
    public class ProveedorConfig: EntityTypeConfiguration<Proveedor>
    {
        public ProveedorConfig()
        {
            ToTable("Proveedores", "demos");
            Property(_prov => _prov.Nombre)
                .IsUnicode()
                .HasMaxLength(50)
                .IsRequired();
            Property(_prov => _prov.Registro)
                .HasColumnType("date")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.
                    DatabaseGeneratedOption.Computed);
            Property(_prov => _prov.Contacto)
                .IsUnicode()
                .HasMaxLength(50)
                .IsRequired();
            Property(_prov => _prov.Telefono)
                .IsUnicode()
                .HasMaxLength(10)
                .IsRequired();
            Property(_prov => _prov.Direccion)
                .IsUnicode()
                .HasMaxLength(80)
                .IsOptional();
            Property(_prov => _prov.Telefono2)
                .IsUnicode()
                .HasMaxLength(10)
                .IsOptional();

            HasMany(_prov => _prov.Facturas)
                .WithRequired(_fac => _fac.Proveedor)
                .HasForeignKey(_fac => _fac.ProveedorId);
        }
    }
}