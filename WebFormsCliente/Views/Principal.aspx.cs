﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebFormsCliente.Views
{
    public partial class Principal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                marcadoListaEqipos.InnerHtml = "";
            }
        }

        protected async void btn_GetEquiposAsync_Click(object sender, EventArgs e)
        {
            WebAPICliente cliemteWebAPI = new WebAPICliente();

            List<Equipo> equipos = await cliemteWebAPI.GetEquipoAsync();
            if (equipos.Count > 0)
            {
                XElement listaEquipos = new XElement("ul",
                        equipos.Select(equipo => new XElement("li", equipo.Apodo))
                    );
                marcadoListaEqipos.InnerHtml = listaEquipos.ToString();
            }
            else
                marcadoListaEqipos.InnerHtml = "<p style=\"color:red;font-size:1.5em;\">" +
                    "¡Ha fallado el consumo del servicio!</p>";
        }

        protected async void btn_GetJugadoresAsync_Click(object sender, EventArgs e)
        {
            WebAPICliente cliemteWebAPI = new WebAPICliente();

            List<Jugador> jugadores = await cliemteWebAPI.ObtenerJugadoresAsync();
            if (jugadores.Count > 0)
            {
                XElement listaJugadores = new XElement("ul",
                        jugadores.Select(jugador => new XElement("li", jugador.Nombre))
                    );
                maracdoListaJugadores.InnerHtml = listaJugadores.ToString();
            }
            else
                maracdoListaJugadores.InnerHtml = "<p style=\"color:red;font-size:1.5em;\">" +
                    "¡Ha fallado el consumo del servicio!</p>";
        }

        protected void btn_RegistrarEquipo_Click(object sender, EventArgs e)
        {
            if(((Control)sender).ID != "btn_EliminarEquipo")
                Response.Redirect("/Views/RegistraEquipo.aspx");
            else
                Response.Redirect("/Views/Eliminar Equipo.aspx");
        }
    }
}