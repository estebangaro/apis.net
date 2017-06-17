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

        // api/proveedores/EliminarProveedor/{valor} = x HTTP DELETE
        [HttpDelete]
        public HttpResponseMessage EliminarProveedor(int valor)
        {
            Proveedor _aEliminar = _dbContexto.Proveedores.
                FirstOrDefault(_prov => _prov.ProveedorId == valor);
            HttpResponseMessage _respuesta;

            if (_aEliminar != null)
                _respuesta = Request.CreateResponse(HttpStatusCode.OK,
                    string.Format($"El proveedor {_aEliminar.Nombre} está listo para ser eliminado"));
            else
                _respuesta = Request.CreateResponse(HttpStatusCode.NoContent);

            return _respuesta;
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

        // Patrón de definición de URIs => "Jerarquía de Recursos".
        [Route("proveedores/{clave}/facturas", Name = "ListaFacturasProveedor")]
        [HttpGet]
        public List<Factura> ObtenFacturasProveedor(int clave)
        {
            // Se asume un valor de clave válido.
            List<Factura> _lista = _dbContexto.Proveedores.Include(_prov => _prov.Facturas.
            Select(_fact => _fact.Detalle)).
                FirstOrDefault(_prov => _prov.ProveedorId == clave).Facturas.ToList();
            _lista.ForEach(_fact =>
            {
                _fact.Proveedor = null;
                _fact.Detalle.ToList().ForEach(_item => _item.Factura = null);
            });

            return _lista;
        }

        // Patrón de definición de URIs => "Múltiples versiones de Controladores".
        [Route("proveedorV1/ObtenProveedorMayor")]
        [HttpGet]
        public HttpResponseMessage ObtenProveedorMayor()
        {
            Proveedor _consulta = _dbContexto.Proveedores.
                OrderByDescending(_prov => _prov.Nombre.Length).
                FirstOrDefault();
            HttpResponseMessage _respuesta;

            if (_consulta != null)
            {
                _respuesta = Request.CreateResponse(HttpStatusCode.OK, _consulta);
                _respuesta.Headers.Location = new Uri(Url.Link("ListaFacturasProveedor",
                    new { clave = _consulta.ProveedorId }));
            }
            else
                _respuesta = Request.CreateErrorResponse(HttpStatusCode.NoContent, "No existe el proveedor con la clave proporcionada");

            return _respuesta;
        }

        // Patrón de definición de URIs => "Sobrecarga de Segmentos de URI". (1)
        [Route("api/facturas/all")]
        [HttpGet]
        public ICollection<Factura> ObtenFacturas()
        {
            List<Factura> _facturas = _dbContexto.Facturas.ToList();
            _facturas.ForEach(_fact =>
            {
                _fact.Proveedor = null;
                _fact.Detalle = null;
            });

            return _facturas;
        }

        // Patrón de definición de URIs => "Sobrecarga de Segmentos de URI". (2)
        [Route("api/facturas/{clave}")]
        [HttpGet]
        public Factura ObtenFacturas(int clave)
        {
            Factura _facturas = _dbContexto.Facturas.FirstOrDefault(_fact => _fact.NumeroFactura == clave);
            if (_facturas != null)
                _facturas.Proveedor = null;

            return _facturas;
        }


        #endregion

        #region Restricciones de Rutas

        [Route("api/proveedores/consulta/{noClave:int}")]
        public Proveedor GetProveedorPorClave(int noClave)
        {
            return _dbContexto.Proveedores.
                FirstOrDefault(_prov => _prov.ProveedorId == noClave);
        }

        [Route("api/proveedores/consulta/{nombreProv:alpha}")]
        public Proveedor GetProveedorPorNombre(string nombreProv)
        {
            return _dbContexto.Proveedores.
                FirstOrDefault(_prov => _prov.Nombre == nombreProv);
        }

        #endregion

        #region Restricciones Personalizadas de Rutas

        [Route("api/proveedores/{email:restriccionDominio}")]
        [HttpGet]
        public Proveedor ObtenProveedorPorEmail(string email)
        {
            return _dbContexto.Proveedores.FirstOrDefault(_prov => _prov.Direccion.Contains(email));
        }

        #endregion

        #region Nombres de Rutas



        #endregion

        #region Orden de Rutas
        #endregion

        #region Parametros Opcionales de URIs y valores predeterminados
        #endregion

    }
}
