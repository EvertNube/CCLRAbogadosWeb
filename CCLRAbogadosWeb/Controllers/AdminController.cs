using CCLRAbogadosWeb.Core;
using CCLRAbogadosWeb.Core.BL;
using CCLRAbogadosWeb.Core.DTO;
using CCLRAbogadosWeb.Core.Helpers;
using CCLRAbogadosWeb.Data;
using CCLRAbogadosWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CCLRAbogadosWeb.Controllers
{
    public class AdminController : Controller
    {
        private bool currentUser()
        {
            if (System.Web.HttpContext.Current.Session != null && System.Web.HttpContext.Current.Session["User"] != null) { return true; }
            else { return false; }
        }
        private UsuarioDTO getCurrentUser()
        {
            if (this.currentUser())
            {
                return (UsuarioDTO)System.Web.HttpContext.Current.Session["User"];
            }
            return null;
        }
        private bool isSuperAdministrator()
        {
            if (getCurrentUser().IdRol == 1) return true;
            return false;
        }
        private bool isAdministrator()
        {
            if (getCurrentUser().IdRol <= 2) return true;
            return false;
        }
        // GET: Admin
        public AdminController()
        {
            UsuarioDTO user = getCurrentUser();
            if (user != null)
            {
                ViewBag.currentUser = user;
                ViewBag.EsAdmin = isAdministrator();
                ViewBag.EsSuperAdmin = isSuperAdministrator();
            }
            else { ViewBag.EsAdmin = false; ViewBag.EsSuperAdmin = false; }
        }

        public ActionResult Index()
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            PaginasBL paginasBL = new PaginasBL();
            IList<EnlaceDTO> paginas = paginasBL.getPaginasTree();
            return View(paginas);
        }
        public ActionResult Pagina(int id)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            PaginasBL paginasBL = new PaginasBL();
            PaginaDTO pagina = paginasBL.getPaginaById(id, true);
            if (pagina.Padre != null || (pagina.Padre == null && isAdministrator()))
            {
                ViewBag.TieneHighlight = paginasBL.getHighlight(id) != null;
                return View(pagina);
            }
            createResponseMessage(CONSTANTES.ERROR, "Usted no tiene privilegios para acceder a esta sección.");
            return RedirectToAction("Index");
        }
        private void createResponseMessage(string status, string message = "", string status_field = "status", string message_field = "message")
        {
            TempData[status_field] = status;
            if (!String.IsNullOrWhiteSpace(message))
            {
                TempData[message_field] = message;
            }
        }

    }
}