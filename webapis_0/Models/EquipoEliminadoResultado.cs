using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace webapis_0.Models
{
    public class EquipoEliminadoResultado : IHttpActionResult
    {
        public bool Estado { get; set; }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(
                    new HttpResponseMessage
                    {
                        StatusCode = Estado ? System.Net.HttpStatusCode.OK :
                        System.Net.HttpStatusCode.Conflict
                    }
                );
        }
    }
}