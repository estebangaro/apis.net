using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsCliente.Views
{
    public partial class Eliminar_equipo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void Unnamed_Click(object sender, EventArgs e)
        {
            WebAPICliente clienteHTTP = new WebAPICliente();
            bool resultado = await clienteHTTP.DeleteEquipoAsync(txbEquipo.Text);

            if (resultado)
                resultadoEliminacion.InnerText = "Se ha eliminado el equipo " + txbEquipo.Text + " correctamente";
            else
                resultadoEliminacion.InnerText = "Ha fallado la eliminación del equipo " + txbEquipo.Text;
        }
    }
}