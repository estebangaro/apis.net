using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsCliente.Views
{
    public partial class Principal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btn_GetEquiposAsync_Click(object sender, EventArgs e)
        {
            WebAPICliente cliemteWebAPI = new WebAPICliente();

            List<Equipo> equipos = await cliemteWebAPI.GetEquipoAsync();
            if (equipos.Count > 0)
            {
                foreach (Equipo equipo in equipos)
                {
                    TextBox txbEquipo = new TextBox();
                    txbEquipo.Text = equipo.Apodo;
                    marcadoListaEquipos.Controls.Add(txbEquipo);
                }
            }
        }
    }
}