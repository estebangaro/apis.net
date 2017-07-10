using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using webapis_0.Models.Entity_Framework_CF;

namespace webapis_0.Models.Configuraciones_Fluent_API
{
    public class JugadorConfig: EntityTypeConfiguration<Jugador>
    {
        public JugadorConfig()
        {
            ToTable("Jugadores", "Soccer");
            HasKey(_j => _j.Registro)
                .Property(_j => _j.Registro)
                .HasColumnName("Clave");
            Property(_equipo => _equipo.Nombre)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();
            Property(_j => _j.Nacimiento)
                .HasColumnType("Date")
                .IsRequired()
                .HasColumnName("Fecha Nacimiento");
            Property(_j => _j.Posicion)
                .IsRequired();
            HasRequired(_j => _j.Equipo_Actual)
                .WithMany(_e => _e.Jugadores)
                .HasForeignKey(_j => _j.Equipo);
        }
    }
}