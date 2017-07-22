using System;

namespace WebFormsCliente
{
    public class Jugador
    {
        public int Registro { get; set; }
        public string Nombre { get; set; }
        public DateTime Nacimiento { get; set; }
        public POSICION_SOCCER Posicion { get; set; }
    }

    public enum POSICION_SOCCER { Portero, Defensa, Medio, Lateral, Delantero };
}