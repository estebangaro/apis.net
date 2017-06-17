using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapis_0.Models.Entity_Framework_CF;
using System.Data.Entity;

namespace webapis_0.Controllers
{
    public class ProveedorV2TestController : ApiController
    {
        private webapis_contexto _dbContexto;

        #region Controladores

        public ProveedorV2TestController()
        {
            _dbContexto = new webapis_contexto();
            _dbContexto.Configuration.ProxyCreationEnabled = false;
            _dbContexto.Configuration.LazyLoadingEnabled = false;
        }

        #endregion



        // Patrón de definición de URIs => "Múltiples versiones de Controladores".
        [Route("proveedorV2/ObtenProveedorMayor")]
        [HttpGet]
        public Proveedor ObtenProveedorMayor()
        {
            return _dbContexto.Proveedores.
                OrderByDescending(_prov => _prov.Facturas.Count).
                FirstOrDefault();
        }


        #region Restricciones de Rutas
        #endregion

        #region Restricciones Personalizadas
        #endregion

        #region Nombres de Rutas
        #endregion

        #region Orden de Rutas
        #endregion

        #region Parametros Opcionales de URIs y valores predeterminados
        #endregion

    }
}
