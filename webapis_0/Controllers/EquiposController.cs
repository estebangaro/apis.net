using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using webapis_0.Models.Entity_Framework_CF;

namespace webapis_0.Controllers
{
    public class EquiposController : ApiController
    {

        #region Campos

        private Models.Entity_Framework_CF.webapis_contexto _dbContexto;

        #endregion

        #region Métodos

        #region Constructores

        public EquiposController()
        {
            DbContexto = new Models.Entity_Framework_CF.webapis_contexto();
            DbContexto.Configuration.ProxyCreationEnabled = false;
        }

        #endregion

        #region Acciones

        #region Modelo Enrutamiento por Convención de Nombres de Verbos HTTP

        // http://192.168.0.13:2412/api/equipos/ GET
        public List<Models.Entity_Framework_CF.Equipo> GetEquipos()
        {
            List<Equipo> _equiposConsulta = _dbContexto.Equipos.ToList();
            //_equiposConsulta.ForEach(_equipo => _equipo.Jugadores = null);

            return _equiposConsulta;
        }

        // http://192.168.0.13:2412/api/equipos/{id} GET
        public Models.Entity_Framework_CF.Equipo GetEquipo(string id)
        {
            Equipo _equipoConsulta = _dbContexto.Equipos.Where(e => e.Nombre == id).FirstOrDefault();
            //_equiposConsulta.ForEach(_equipo => _equipo.Jugadores = null);

            return _equipoConsulta;
        }

        [EnableCors(origins: "http://localhost:52909", headers: "*", methods: "POST")]
        // http://192.168.0.13:2412/api/equipos/ POST
        public HttpResponseMessage PostEquipo(Equipo nuevo)
        {
            HttpResponseMessage _registro;
            try
            {
                _dbContexto.Equipos.Add(nuevo);
                _dbContexto.SaveChanges();
                _registro = Request.CreateResponse(HttpStatusCode.OK, nuevo);
                _registro.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = nuevo.Nombre }));
            }
            catch
            {
                _registro = Request.CreateResponse(HttpStatusCode.Conflict);
            }

            return _registro;
        }

        // http://192.168.0.13:2412/api/equipos/ PUT
        public HttpResponseMessage PutEquipo(Equipo actualizado)
        {
            _dbContexto.Configuration.ProxyCreationEnabled = true;
            HttpResponseMessage _registro;
            try
            {
                _dbContexto.Entry(actualizado).State = System.Data.Entity.EntityState.Modified;
                _dbContexto.SaveChanges();
                _registro = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                _registro = Request.CreateResponse(HttpStatusCode.Conflict);
            }

            return _registro;
        }

        // http://192.168.0.13:2412/api/equipos/{ideliminado} HTTP/DELETE
        public IHttpActionResult DeleteEquipo(string id)
        {
            _dbContexto.Configuration.ProxyCreationEnabled = true;
            IHttpActionResult resultado;
            Equipo existeEquipo = _dbContexto.Equipos.
                FirstOrDefault(_equipo => _equipo.Nombre.ToLower() == id.ToLower());
            if (existeEquipo != null)
            {
                // _dbContexto.Entry<Equipo>(existeEquipo).State = System.Data.Entity.EntityState.Deleted;
                _dbContexto.Equipos.Remove(existeEquipo);
                _dbContexto.SaveChanges();
                resultado = new Models.EquipoEliminadoResultado { Estado = true };
            }
            else
            {
                resultado = NotFound();
            }

            return resultado;
        }

        // http://192.168.0.3:2412/api/equipos [HTTP/GETJUGADORES]
        [AcceptVerbs("GETJUGADORES")]
        public List<Jugador> ObtenerJugadores()
        {
            _dbContexto.Configuration.LazyLoadingEnabled = false;
            List<Jugador> jugadores = _dbContexto.Jugadores.ToList();

            return jugadores;
        }

        #endregion

        #region Modelo Enrutamiento por Atributos de Enrutamiento

        [Route("jugadores/mayor/{nombre}")]
        [AcceptVerbs("OBTENJUGADORESNOMBRE")]
        // http://localhost:56288/jugadores/mayor/{nombre}
        public HttpResponseMessage JugadoresNombreComo(string nombre)
        {
            HttpResponseMessage resultado;
            IEnumerable<Jugador> jugadores =
                DbContexto.Jugadores.Where(jugador => jugador.Nombre.Contains(nombre));
            if(jugadores != null && jugadores.Count() > 0)
            {
                resultado = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ObjectContent<List<Jugador>>(
                        jugadores.ToList(),
                        new JsonMediaTypeFormatter()
                    )
                };
            }
            else
            {
                resultado = new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound };
            }

            return resultado;
        } 

        #endregion

        #endregion

        #endregion

        #region Propiedades

        public Models.Entity_Framework_CF.webapis_contexto DbContexto
        {
            get
            {
                if (_dbContexto != null)
                    return _dbContexto;
                throw new Exception("No se ha inicializado el contexto de datos");
            }

            set
            {
                _dbContexto = value is Models.Entity_Framework_CF.webapis_contexto 
                    ? value : null;
            }
        }

        #endregion

        #region Eventos

        #endregion
    }
}
