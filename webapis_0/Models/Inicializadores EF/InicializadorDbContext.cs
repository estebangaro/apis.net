using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using webapis_0.Models.Entity_Framework_CF;

namespace webapis_0.Models.Inicializadores_EF
{
    public class InicializadorDbContext: DropCreateDatabaseIfModelChanges<webapis_contexto>
    {
        protected override void Seed(webapis_contexto context)
        {
            try
            {
                List<Proveedor> _proveedores = new List<Proveedor> {
                new Proveedor{ Contacto = "Amber Trejo", Direccion ="Iguanas 65 Campestre Miguel Hidalgo", Nombre="Luis Esteban", Telefono="5512345678" },
                new Proveedor{ Contacto = "Jacky Trejo", Direccion ="Iguanas 66 Campestre Miguel Hidalgo", Nombre="Fernando", Telefono="5512345698" },
                new Proveedor{ Contacto = "Herminia Gonzalez", Direccion ="Iguanas 67 Campestre Miguel Hidalgo", Nombre="Victor Felipe", Telefono="5518345678" }
            };

                List<Factura> _facturas = new List<Factura> {
                new Factura{ CFDI = "cfdidemo", Fecha=DateTime.Now, Importe=1200, ISR=100,
                    IVA =100, NumeroFactura=1, Proveedor=_proveedores[0], RFC_Emisor="emisordemorfc", RFC_Receptor="receptordemorfc", Total=1400}
            };

                List<Linea_Factura> _lineas = new List<Linea_Factura> {
                new Linea_Factura{ Total = 200, Cantidad = 1, PrecioUnitario = 200, ITEM = 1, Factura = _facturas[0], Descripcion ="Cuaderno", Unidad="PZ"}
                ,new Linea_Factura{ Total = 300, Cantidad = 2, PrecioUnitario = 150, ITEM = 2, Factura = _facturas[0], Descripcion ="Termo", Unidad="PZ"}
                ,new Linea_Factura{ Total = 500, Cantidad = 1, PrecioUnitario = 500, ITEM = 3, Factura = _facturas[0], Descripcion ="Pluma", Unidad="PZ"}
                ,new Linea_Factura{ Total = 100, Cantidad = 2, PrecioUnitario = 50, ITEM = 4, Factura = _facturas[0], Descripcion ="Separador", Unidad="PZ"}
                ,new Linea_Factura{ Total = 100, Cantidad = 1, PrecioUnitario = 100, ITEM = 5, Factura = _facturas[0], Descripcion ="Estampa", Unidad="PZ"}
            };
                Equipo _nuevo = new Equipo
                {
                    Apodo = "La máquina celeste",
                    CampeonatosLiga = 8,
                    Fundacion = new DateTime(1965, 08, 23),
                    Nombre = "Cruz Azul FC"
                };
                List<Jugador> _jugadores = new List<Jugador>
                {
                    new Jugador{ Equipo_Actual = _nuevo, Nacimiento = DateTime.Today, Nombre = "Cristian Gimenez", Posicion = POSICION_SOCCER.Delantero},
                    new Jugador{ Equipo_Actual = _nuevo, Nacimiento = DateTime.Today, Nombre = "Jesus Corona", Posicion = POSICION_SOCCER.Portero},
                    new Jugador{ Equipo_Actual = _nuevo, Nacimiento = DateTime.Today, Nombre = "Catita Dominguez", Posicion = POSICION_SOCCER.Defensa}
                };

                context.Proveedores.AddRange(_proveedores);
                context.Facturas.AddRange(_facturas);
                context.Items.AddRange(_lineas);
                context.Equipos.Add(_nuevo);
                context.Jugadores.AddRange(_jugadores);

                context.SaveChanges();
            }
            catch(DbEntityValidationException errores)
            {
                
            }
        }
    }
}