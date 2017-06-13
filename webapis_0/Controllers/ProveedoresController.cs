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
    public class ProveedoresController : ApiController
    {
        private webapis_contexto _dbContexto;

        #region Controladores

        public ProveedoresController()
        {
            _dbContexto = new webapis_contexto();
            _dbContexto.Configuration.ProxyCreationEnabled = false;
            _dbContexto.Configuration.LazyLoadingEnabled = false;
        }

        #endregion

        #region Entrutamiento basado en nombres de métodos HTTP
        // api/proveedores/{id} HTTPP GET
        public Proveedor GetProveedorPorId(int id)
        {
            Proveedor _consultado = _dbContexto.Proveedores.Include(_prov => 
                _prov.Facturas.Select(_fact => _fact.Detalle)).
                FirstOrDefault(_prov => _prov.ProveedorId == id);
            _consultado.Facturas.ToList().ForEach(_fact =>
            {
                _fact.Proveedor = null;
                _fact.Detalle.ToList().ForEach(_item => _item.Factura = null);
            });
            return _consultado;
        }

        // api/proveedores/?categoryID = {categoryID} HTTP GET
        public IEnumerable<Proveedor> GetProveedores(int categoryID)
        {
            return _dbContexto.Proveedores;
        }

        // api/proveedores/?{numeroF}=valor1&{proveedorId}=valor2 HTTP DELETE
        public HttpResponseMessage DeleteFactura(int numeroF, int proveedorId)
        {
            HttpResponseMessage _eliminacion;
            Factura _conssultadaF = _dbContexto.Facturas.FirstOrDefault(_fac =>
                _fac.ProveedorId == proveedorId && _fac.NumeroFactura == numeroF);

            if (_conssultadaF == null)
                _eliminacion = Request.CreateResponse(HttpStatusCode.Conflict);
            else
                _eliminacion = Request.CreateResponse(HttpStatusCode.OK);

            return _eliminacion;
        }

        // api/proveedores/{id} HTTP POST
        public HttpResponseMessage PostLinea(int id)
        {
            HttpResponseMessage _eliminacion;
            Linea_Factura _conssultadaL = _dbContexto.Items.FirstOrDefault(_fac =>
                _fac.ITEM == id);

            if (_conssultadaL == null)
                _eliminacion = Request.CreateResponse(HttpStatusCode.Conflict);
            else
                _eliminacion = Request.CreateResponse(HttpStatusCode.OK);

            return _eliminacion;
        }

        #endregion

        #region Enrutamiento basado en nombres de acción

        // api/proveedores/ObtenProveedoresNombre/{valor} HTTP GET
        [HttpGet]
        [ActionName("ObtenProveedoresNombre")]
        public List<Proveedor> ObtenProveedoresNombre1(string valor)
        {
            return _dbContexto.Proveedores.
                Where(_prov => _prov.Nombre.StartsWith(valor)).
                ToList();
        }

        // api/proveedores/ObtenProveedoresNombre/{valor} HTTP POST
        [HttpPost]
        [ActionName("ObtenProveedoresNombre")]
        public List<Proveedor> ObtenProveedoresNombre2(string valor)
        {
            return _dbContexto.Proveedores.
                Where(_prov => _prov.Nombre.StartsWith(valor)).
                ToList();
        }

        #endregion

        #region Enrutamiento basado en atributos de enrutamiento

        [HttpGet]
        [Route("api/facturas/{clave:int}/{proveedor:int}")]
        public Factura ObtenFacturaPorClave(int clave, int proveedor)
        {
            Factura _facura = _dbContexto.Facturas.Include(_factura => _factura.Detalle).FirstOrDefault(
                _fac => _fac.NumeroFactura == clave && _fac.ProveedorId == proveedor);
            _facura.Detalle.ToList().ForEach(_item => _item.Factura = null);

            return _facura;
        }

        [Route("facturas/{clave:int:min(0)}/{proveedor:int:max(20)}")]
        public Factura GetFacturaPorClave2(int clave, int proveedor)
        {
            Factura _facura = _dbContexto.Facturas.Include(_factura => _factura.Detalle).FirstOrDefault(
                _fac => _fac.NumeroFactura == clave && _fac.ProveedorId == proveedor);
            _facura.Detalle.ToList().ForEach(_item => _item.Factura = null);

            return _facura;
        }

        #endregion

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
