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

        public ProveedoresController()
        {
            _dbContexto = new webapis_contexto();
            _dbContexto.Configuration.ProxyCreationEnabled = false;
            _dbContexto.Configuration.LazyLoadingEnabled = false;
        }
        #region Entrutamiento basado en nombres de métodos HTTP
        // api/proveedores/{id}
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

        // api/proveedores/?categoryID = {categoryID}
        public IEnumerable<Proveedor> GetProveedores(int categoryID)
        {
            return _dbContexto.Proveedores;
        }

        #endregion

        [Route("facturas/{clave:int}/{proveedor:int}")]
        public Factura GetFacturaPorClave(int clave, int proveedor)
        {
            Factura _facura = _dbContexto.Facturas.Include(_factura => _factura.Detalle).FirstOrDefault(
                _fac => _fac.NumeroFactura == clave && _fac.ProveedorId == proveedor);
            _facura.Detalle.ToList().ForEach(_item => _item.Factura = null);

            return _facura;
        }
    }
}
