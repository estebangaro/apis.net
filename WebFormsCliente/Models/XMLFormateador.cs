using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using WebFormsCliente;

namespace webapis_0.Models
{
    public class XMLFormateador : MediaTypeFormatter
    {
        public XMLFormateador()
        {
            // SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("aplication/xmlequipo"));
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/xmlequipo"));
            SupportedEncodings.Add(new UTF8Encoding(false));
        }
        public override bool CanReadType(Type type)
        {
            TypeInfo infoTipo = typeof(IEnumerable<Equipo>).GetTypeInfo();

            return type == typeof(Equipo)
                || infoTipo.IsAssignableFrom(type.GetTypeInfo());
        }

        public override bool CanWriteType(Type type)
        {
            TypeInfo infoTipo = typeof(IEnumerable<Equipo>).GetTypeInfo();

            return type == typeof(Equipo)
                || infoTipo.IsAssignableFrom(type.GetTypeInfo());
        }

        public override Task WriteToStreamAsync(Type type, object value, 
            Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            return Task.Run(() =>
            {
                Encoding codificacion = SelectCharacterEncoding(content.Headers);
                StreamWriter escritorTxt = new StreamWriter(writeStream);
                IEnumerable<Equipo> coleccion =
                    value as IEnumerable<Equipo>;
                XElement equipos = new XElement("Equipos");
                if (coleccion != null)
                {
                    equipos.Add(
                            from equipo in coleccion
                            select new XElement("Equipo",
                                new XElement("Apodo", equipo.Apodo),
                                new XElement("Campeonatos", equipo.CampeonatosLiga),
                                new XElement("Fundacion", equipo.Fundacion.ToShortDateString()),
                                new XElement("Jugadores", equipo.Jugadores != null && equipo.Jugadores.Count > 0 ?
                                    equipo.Jugadores.Select(jugador =>
                                        new XElement("Jugador",
                                            new XElement("Nacimiento", jugador.Nacimiento.ToShortDateString()),
                                            new XElement("Nombre", jugador.Nombre),
                                            new XElement("Posicion", jugador.Posicion),
                                            new XElement("Registro", jugador.Registro))) :
                                        null),
                                new XElement("Nombre", equipo.Nombre)));

                }
                else
                {
                    Equipo equipo = value as Equipo;
                    equipos.Add(new XElement("Equipo",
                        new XElement("Apodo", equipo.Apodo),
                        new XElement("Campeonatos", equipo.CampeonatosLiga),
                        new XElement("Fundacion", equipo.Fundacion.ToShortDateString()),
                        new XElement("Jugadores", equipo.Jugadores != null && equipo.Jugadores.Count > 0 ?
                            equipo.Jugadores.Select(jugador =>
                                new XElement("Jugador",
                                    new XElement("Nacimiento", jugador.Nacimiento.ToShortDateString()),
                                    new XElement("Nombre", jugador.Nombre),
                                    new XElement("Posicion", (int)jugador.Posicion),
                                    new XElement("Registro", jugador.Registro))) :
                                null),
                        new XElement("Nombre", equipo.Nombre)));
                }

                escritorTxt.Write(equipos.ToString());
                escritorTxt.Flush();
            });
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, 
            HttpContent content, IFormatterLogger formatterLogger)
        {
            return Task<object>.Run(() =>
            {
                Encoding codificacion = SelectCharacterEncoding(content.Headers);
                DateTime fundacion;
                string[] fechaC;
                List<Equipo> coleccion;
                using (StreamReader lector = new StreamReader(readStream))
                {
                    XElement equipos = XElement.Parse(
                            lector.ReadToEnd());
                    coleccion = new List<Equipo>();
                    coleccion.AddRange(
                            equipos.Elements().Select(
                                    equipo =>
                                    {
                                        fechaC = equipo.Element("Fundacion").Value.Split(new char[] { '/' },
                                            StringSplitOptions.RemoveEmptyEntries);
                                        fundacion = Convert.ToDateTime($"{fechaC[2]}/{fechaC[1]}/{fechaC[0]}");
                                        return new Equipo
                                        {
                                            Apodo = equipo.Element("Apodo").Value,
                                            CampeonatosLiga = short.Parse(equipo.Element("Campeonatos").Value),
                                            Fundacion = fundacion,
                                            Nombre = equipo.Element("Nombre").Value,
                                            Jugadores = equipo.Element("Jugadores").HasElements ?
                                                equipo.Element("Jugadores").Elements().Select(
                                                        jugador =>
                                                        {
                                                            fechaC = equipo.Element("Nacimiento").
                                                                Value.Split(new char[] { '/' },
                                                                    StringSplitOptions.RemoveEmptyEntries);
                                                            fundacion = Convert.
                                                                ToDateTime($"{fechaC[2]}/{fechaC[1]}/{fechaC[0]}");
                                                            return new Jugador
                                                            {
                                                                Nacimiento = fundacion,
                                                                Nombre = jugador.Element("Nombre").Value,
                                                                Posicion = (POSICION_SOCCER)
                                                                    int.Parse(jugador.Element("Posicion").Value),
                                                                Registro = int.Parse(jugador.Element("Registro").Value)
                                                            };
                                                        }
                                                    ).ToList() : null
                                        };
                                    }
                                ));
                }
                
                return coleccion.Count > 1 ? (object)coleccion : coleccion[0];
            });
        }
    }
}