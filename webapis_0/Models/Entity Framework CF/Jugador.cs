using System;

namespace webapis_0.Models.Entity_Framework_CF
{
    public class Jugador
    {
        public int Registro { get; set; }
        public string Nombre { get; set; }
        public DateTime Nacimiento { get; set; }
        public POSICION_SOCCER Posicion { get; set; }

        public string Equipo { get; set; }
        public virtual Equipo Equipo_Actual { get; set; }
    }

    public enum POSICION_SOCCER { Portero, Defensa, Medio, Lateral, Delantero};
}