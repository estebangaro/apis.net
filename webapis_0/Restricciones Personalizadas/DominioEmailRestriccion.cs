using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http.Routing;

namespace webapis_0.Restricciones_Personalizadas
{
    public class DominioEmailRestriccion : IHttpRouteConstraint
    {
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName,
            IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object _valor;
            if (values.TryGetValue(parameterName, out _valor))
                // Se asume que existe un valor distinto de nullo para el nombre de parámetro en cuestión.
                return new Regex("^.{1,}-garo$").Match(_valor.ToString()).Success;
            return false;
        }
    }
}