using CCLRAbogados.Core.DTO;
using CCLRAbogados.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CCLRAbogados.Web.Models;

namespace CCLRAbogados.Web.Controllers
{
    public class ContactoController : BaseDynamicController
    {
        public ContactoController()
        {
            base.navbar.clearAll();
            //base.navbar.activeAll();
            base.navbar.gold4Active = "active";
            ViewBag.Navbar = base.navbar;
        }
        public ActionResult Index(string page, string id, string subid)
        {
            showPagina(CONSTANTES.URI_CONTACTO, HttpContext.Request.Url, page, id, subid);

            ContactoDTO contacto = TempData["contacto"] as ContactoDTO;
            if (contacto == null) { contacto = new ContactoDTO(); }
            else
            {
                contacto.fillReferencia();
                contacto.fillAreas();
            }
            return View("Pagina", contacto);
        }
    }
}
