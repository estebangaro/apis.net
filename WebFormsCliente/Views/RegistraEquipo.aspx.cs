using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsCliente.Views
{
    public partial class RegistraEquipo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                registradoContenedor.InnerHtml = "";
        }



        protected async void btnUpdateEquipo_Click(object sender, EventArgs e)
        {
            Equipo modificado = new Equipo
            {
                Apodo = txbApodo.Text,
                CampeonatosLiga = short.Parse(txbCampeonatos.Text),
                Fundacion = calendarFundacion.SelectedDate,
                Nombre = txbNombre.Text
            };
            WebAPICliente _cliente = new WebAPICliente();
            var registrado = await _cliente.PutEquipoAsync(modificado);
            if (registrado)
                registradoContenedor.InnerHtml = $"Equipo {modificado.Apodo} actualizado exitosamente";
            else
                registradoContenedor.InnerHtml = $"Equipo {modificado.Apodo} no ha logrado actualizarse";
        }

        protected async void btnPostEquipo_Click(object sender, EventArgs e)
        {
            Equipo nuevo = new Equipo
            {
                Apodo = txbApodo.Text,
                CampeonatosLiga = short.Parse(txbCampeonatos.Text),
                Fundacion = calendarFundacion.SelectedDate,
                Nombre = txbNombre.Text
            };
            WebAPICliente _cliente = new WebAPICliente();
            var registrado = await _cliente.PostEquipoAsync(nuevo);
            if (registrado != null)
                registradoContenedor.InnerHtml = $"Equipo {registrado.Apodo} registrado exitosamente";
            else
                registradoContenedor.InnerHtml = $"Equipo {nuevo.Apodo} no ha logrado registrarse";
        }
    }
}