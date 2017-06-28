using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapis_0.Models.Entity_Framework_CF
{
    public class Equipo
    {
        public string Nombre { get; set; }
        public DateTime Fundacion { get; set; }
        public short CampeonatosLiga { get; set; }
        public string Apodo { get; set; }

        public virtual ICollection<Jugador> Jugadores { get; set; }
    }
}