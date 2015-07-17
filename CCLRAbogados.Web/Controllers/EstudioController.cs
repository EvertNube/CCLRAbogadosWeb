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
    public class EstudioController : BaseDynamicController
    {
        public ActionResult Index(string page, string id, string subid)
        {
            //ViewBag.Title = ConfigurationManager.AppSettings["DefaultTitle"].ToString();
            bool result = base.showPagina(CONSTANTES.URI_ESTUDIO, HttpContext.Request.Url, page, id, subid);
            if (result) { return View("Pagina"); }
            else { return View(); }
        }

    }
}
