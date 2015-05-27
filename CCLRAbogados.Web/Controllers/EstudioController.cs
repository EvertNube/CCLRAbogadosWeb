using CCLRAbogados.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CCLRAbogados.Web.Models;

namespace CCLRAbogados.Web.Controllers
{
    public class EstudioController : BaseDynamicController
    {
        public EstudioController()
        {
            base.navbar.clearAll();
			ViewBag.Navbar = base.navbar;
        }

        public ActionResult Index(string page, string id, string subid)
        {
            bool result = base.showPagina(CONSTANTES.URI_ESTUDIO, HttpContext.Request.Url, page, id, subid);
            if (!String.IsNullOrEmpty(page) && page == CONSTANTES.URI_ESTUDIO)
            {
                base.navbar.lightbrown = "lightbrown";
                base.navbar.lightbrownActive = "active";
                ViewBag.Navbar = base.navbar;    
            }
            if (result) { return View("Pagina"); }
            else { return View(); }
        }

    }
}
