using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using webapis_0.Models.Entity_Framework_CF;

namespace webapis_0.Models.Configuraciones_Fluent_API
{
    public class EquipoConfig: EntityTypeConfiguration<Equipo>
    {
        public EquipoConfig()
        {
            ToTable("Equipos", "Soccer");
            HasKey<string>(_equipo => _equipo.Nombre)
                .Property(_equipo => _equipo.Nombre)
                .HasMaxLength(50)
                .IsUnicode();
            Property(_equipo => _equipo.Apodo)
                .HasMaxLength(20)
                .IsUnicode()
                .IsOptional();
            Property(_equipo => _equipo.CampeonatosLiga)
                .HasColumnName("Campeonatos")
                .IsRequired();
            Property(_equipo => _equipo.Fundacion)
                .IsRequired()
                .HasColumnName("Fundacion Equipo")
                .HasColumnType("Date");
        }
    }
}