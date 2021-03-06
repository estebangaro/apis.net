﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebFormsCliente;

namespace webapis_0.Models
{
    public class PipeMediaTypeFormatter : MediaTypeFormatter
    {
        public PipeMediaTypeFormatter()
        {
            this.SupportedMediaTypes.Add(new System.Net.Http.Headers.
                MediaTypeHeaderValue("text/pipe"));
            this.SupportedEncodings.Add(new UTF8Encoding(false));
        }
        public override bool CanReadType(Type type)
        {
            return type == typeof(Equipo);
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof(Equipo);
        }

        public override Task<object> ReadFromStreamAsync(Type type, 
            Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            Equipo leido;
            Encoding codificacion = SelectCharacterEncoding(content.Headers);

            using(StreamReader lector = new StreamReader(readStream, codificacion))
            {
                string[] cadena = lector.ReadLine().Split(new char[] { '|' }, 
                    StringSplitOptions.RemoveEmptyEntries);
                leido = new Equipo
                {
                    Apodo = cadena[0],
                    CampeonatosLiga = short.Parse(cadena[1]),
                    Fundacion = DateTime.Today,
                    Nombre = cadena[2]                    
                };
            }

            return Task.FromResult<object>(leido);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, 
            HttpContent content, TransportContext transportContext)
        {
            return Task.Factory.StartNew(() =>
            {
                Encoding codificacion = SelectCharacterEncoding(content.Headers);

                StreamWriter escritor = new StreamWriter(writeStream, codificacion);

                Equipo escrito = value as Equipo;
                escritor.WriteLine($"{escrito.Apodo}|{escrito.CampeonatosLiga}|{escrito.Nombre}");

                escritor.Flush();

            });
        }
    }
}