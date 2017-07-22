using System;
using System.Collections.Generic;

namespace WebFormsCliente
{
    public class Equipo
    {
        public string Nombre { get; set; }
        public DateTime Fundacion { get; set; }
        public short CampeonatosLiga { get; set; }
        public string Apodo { get; set; }
        public ICollection<Jugador> Jugadores { get; set; }
    }
}