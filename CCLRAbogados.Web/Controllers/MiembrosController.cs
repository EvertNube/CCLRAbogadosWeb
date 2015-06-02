using CCLRAbogados.Core;
using CCLRAbogados.Core.BL;
using CCLRAbogados.Core.DTO;
using CCLRAbogados.Core.Helpers;
using CCLRAbogados.Data;
using CCLRAbogados.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CCLRAbogados.Web.Models;

namespace CCLRAbogados.Web.Controllers
{
    public class MiembrosController : BaseDynamicController
    {
        public ActionResult Index(string page, string id, string subid)
        {
            /*bool result = base.showPagina(CONSTANTES.URI_MIEMBROS, HttpContext.Request.Url, page, id, subid);
            if (result) { return View("Pagina"); }
            else { return View(); }*/
            showPagina(CONSTANTES.URI_AREAS_DE_PRACTICA, HttpContext.Request.Url, page, id, subid);
            MiembrosBL objBL = new MiembrosBL();
            ViewBag.Cargos = objBL.getCargosViewBag(false);
            return View("Pagina", objBL.getMiembrosActivos());
            //return View("Pagina");
        }
    }
}
