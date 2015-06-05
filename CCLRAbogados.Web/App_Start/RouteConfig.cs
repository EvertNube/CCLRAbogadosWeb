using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CCLRAbogados.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute(name: "Contacto", url: "{controller}/{page}",
                             defaults: new { page = UrlParameter.Optional, action = "Index" },
                             constraints: new { controller = "Contacto" });

            routes.MapRoute(name: "Archivo", url: "{controller}/{path}",
                             defaults: new { path = UrlParameter.Optional, action = "Index" },
                             constraints: new { controller = "Archivo" });

            routes.MapRoute(name: "AreasDePractica", url: "{controller}/{page}/{id}/{subid}",
                             defaults: new { page = UrlParameter.Optional, id = UrlParameter.Optional, subid = UrlParameter.Optional, action = "Index" },
                             constraints: new { controller = "AreasDePractica" });

            routes.MapRoute(name: "Miembros", url: "{controller}/{page}/{id}/{subid}",
                             defaults: new { page = UrlParameter.Optional, id = UrlParameter.Optional, subid = UrlParameter.Optional, action = "Index" },
                             constraints: new { controller = "Miembros" });

            routes.MapRoute( name: "Estudio", url: "{controller}/{page}/{id}/{subid}",
                             defaults: new { page = UrlParameter.Optional, id = UrlParameter.Optional, subid = UrlParameter.Optional, action = "Index" },
                             constraints: new { controller = "Estudio" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}