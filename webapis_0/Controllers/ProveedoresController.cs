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
        }

        // api/proveedores/{id}
        public Proveedor GetProveedorPorId(int id)
        {
            return _dbContexto.Proveedores.Include(_prov => _prov.Facturas).FirstOrDefault(_prov => _prov.ProveedorId == id);
        }
    }
}
