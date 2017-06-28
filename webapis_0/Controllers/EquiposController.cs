using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapis_0.Models.Entity_Framework_CF;

namespace webapis_0.Controllers
{
    public class EquiposController : ApiController
    {
        private Models.Entity_Framework_CF.webapis_contexto _dbContexto;

        public EquiposController()
        {
            _dbContexto = new Models.Entity_Framework_CF.webapis_contexto();
            _dbContexto.Configuration.ProxyCreationEnabled = false;
        }

        #region Modelo Enrutamiento por Convención de Nombres de Verbos HTTP

        // http://192.168.0.13:2412/api/equipos/ GET
        public List<Models.Entity_Framework_CF.Equipo> GetEquipos()
        {
            List<Equipo> _equiposConsulta = _dbContexto.Equipos.ToList();
            //_equiposConsulta.ForEach(_equipo => _equipo.Jugadores = null);

            return _equiposConsulta;
        }

        // http://192.168.0.13:2412/api/equipos/ POST
        public HttpResponseMessage PostEquipo(Equipo nuevo)
        {
            HttpResponseMessage _registro;
            try
            {
                _dbContexto.Equipos.Add(nuevo);
                _registro = Request.CreateResponse(HttpStatusCode.OK, nuevo);
                // _registro.Headers.Location = new Uri(route)
            }
            catch
            {
                _registro = Request.CreateResponse(HttpStatusCode.Conflict);
            }

            return _registro;
        }

        #endregion
    }
}
