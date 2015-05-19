using CCLRAbogados.Core.BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCLRAbogados.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {
            base.navbar.clearAll();
            base.navbar.activeAll();
            ViewBag.Navbar = base.navbar;
        }
        public ActionResult Index()
        {
            HomeBL contextBL = new HomeBL();

            //ViewBag.Noticias = contextBL.getNoticias();
            ViewBag.Banner = contextBL.getBanners();
            ViewBag.MiniBanner = contextBL.getMiniBanners();
            ViewBag.EnlacesGris = contextBL.getEnlacesGris();
            ViewBag.Eventos = contextBL.getEventos();
            //ViewBag.Revistas = contextBL.getRevistas();
            //ViewBag.Libros = contextBL.getLibros();
            ViewBag.SocialLinks = contextBL.getSocial();
            ViewBag.EnlacesFooter = contextBL.getEnlacesFooter();
            ViewBag.Youtube = contextBL.getYoutube();

            socialData.Url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
            socialData.Title = ConfigurationManager.AppSettings["DefaultTitle"].ToString();
            ViewBag.Title = ConfigurationManager.AppSettings["DefaultTitle"].ToString();
            ViewBag.Social = socialData;

            //RevistasBL revistasBL = new RevistasBL();

            //ViewBag.TipoRevistas = revistasBL.getTipoRevistas();
            //ViewBag.Revistas = revistasBL.getRevistasTop();

            return View();
        }

    }
}
