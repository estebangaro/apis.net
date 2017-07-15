﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using webapis_0.Models;

namespace WebFormsCliente
{
    public class WebAPICliente
    {
       private HttpMessageHandler manejador;
        private string URI = "api/equipos";
        private string DireccionBase = "http://192.168.0.13:2412/";

        public WebAPICliente()
        {
            manejador = new HttpClientHandler();
        }

        public WebAPICliente(HttpMessageHandler manejador)
        {
            this.manejador = manejador;
        }

        public async Task<List<Equipo>> GetEquipoAsync()
        {
            List<Equipo> equipos;
            HttpResponseMessage respuesta;

            using(HttpClient clienteHTTP = new HttpClient(manejador))
            {
                clienteHTTP.BaseAddress = new Uri(DireccionBase);
                clienteHTTP.DefaultRequestHeaders.Accept.Clear();
                clienteHTTP.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    respuesta = await clienteHTTP.GetAsync(URI);
                    respuesta.EnsureSuccessStatusCode();
                    equipos = await respuesta.Content.ReadAsAsync<List<Equipo>>();
                }
                catch
                {
                    equipos = new List<Equipo> { };
                }
            }

            return equipos;
        }

        public async Task<Equipo> PostEquipoAsync(Equipo nuevo)
        {
            Equipo equipo;
            using (HttpClient clienteHTTP = new HttpClient(manejador))
            {
                try
                {
                    clienteHTTP.BaseAddress = new Uri(DireccionBase);
                    clienteHTTP.DefaultRequestHeaders.Accept.Clear();
                    clienteHTTP.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/pipe"));
                    var respuestaHTTP = await clienteHTTP.PostAsync(URI, nuevo, new PipeMediaTypeFormatter());
                    respuestaHTTP.EnsureSuccessStatusCode();
                    equipo = await respuestaHTTP.Content.ReadAsAsync<Equipo>(new List<MediaTypeFormatter> {
                        new PipeMediaTypeFormatter()
                    });
                }
                catch(Exception ex)
                {
                    equipo = null;
                }
            }
            return equipo;
        }

    }
}