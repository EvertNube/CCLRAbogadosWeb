using CCLRAbogados.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CCLRAbogados.Web.Models;
using System.Configuration;

namespace CCLRAbogados.Web.Controllers
{
    public class AreasDePracticaController : BaseDynamicController
    {
        public AreasDePracticaController()
        {
            base.navbar.clearAll();
            base.navbar.gold3Active = "active";
            ViewBag.Navbar = base.navbar;
            //ViewBag.Title = ConfigurationManager.AppSettings["DefaultTitle"].ToString();
        }
        public ActionResult Index(string page, string id, string subid)
        {
            showPagina(CONSTANTES.URI_AREAS_DE_PRACTICA, HttpContext.Request.Url, page, id, subid);
            
            return View("Pagina");
        }

    }
}
