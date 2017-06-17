using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;

namespace webapis_0
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web
            DefaultInlineConstraintResolver _constarintResolver = new DefaultInlineConstraintResolver();
            _constarintResolver.ConstraintMap.Add("restriccionDominio",
                typeof(Restricciones_Personalizadas.DominioEmailRestriccion));
            // Rutas de API web
            config.MapHttpAttributeRoutes(_constarintResolver);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "AccionNombreApi",
                routeTemplate: "api/{controller}/{action}/{valor}"
            );
        }
    }
}
