using CCLRAbogados.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CCLRAbogados.Web.Models;

namespace CCLRAbogados.Web.Controllers
{
    public class AreasDePracticaController : BaseDynamicController
    {
        public ActionResult Index(string page, string id, string subid)
        {
            bool result = base.showPagina(CONSTANTES.URI_AREAS_DE_PRACTICA, HttpContext.Request.Url, page, id, subid);
            if (result) { return View("Pagina"); }
            else { return View(); }
        }

    }
}
