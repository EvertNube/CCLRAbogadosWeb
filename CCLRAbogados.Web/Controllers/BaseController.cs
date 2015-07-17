using CCLRAbogados.Core;
using CCLRAbogados.Core.BL;
using CCLRAbogados.Core.DTO;
using CCLRAbogados.Data;
using CCLRAbogados.Helpers;
using CCLRAbogados.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCLRAbogados.Web.Controllers
{
    public class BaseController : Controller
    {
        protected Navbar navbar { get; set; }
        protected Pagina currentPage { get; set; }
        protected SocialDTO socialData { get; set; }

        protected bool getPaginaInfo(string controller)
        {
            cleanViewBag();
            PaginasBL paginaBL = new PaginasBL();
            Pagina pagina = new Pagina();
            if (pagina != null)
            {
                pagina = paginaBL.getPagina(controller);
                this.currentPage = pagina;
                ViewBag.Title = pagina.Titulo;
                ViewBag.Name = pagina.Nombre;
                ViewBag.Content = pagina.Contenido;
                /*if (pagina != null)
                    ViewBag.Links = paginaBL.getSideBarLinks(pagina, HttpContext.Request.Url.AbsoluteUri);*/
                if (pagina.MostrarHighlights)
                    ViewBag.Highlights = paginaBL.getHighlightsByPaginaId(pagina.IdPagina);
                if (pagina.MostrarCover)
                    ViewBag.Cover = pagina.Cover;
                ViewBag.Landing = pagina.Landing;
                socialData.Url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                socialData.Title = pagina.Nombre;
                ViewBag.Social = socialData;

                return true;
            }
            this.currentPage = null;
            return false;
        }

        public BaseController()
        {
            initStartupVariables();
        }

        protected void initStartupVariables()
        {
            HomeBL contextBL = new HomeBL();
            ViewBag.EnlacesFooter = contextBL.getEnlacesFooter();
            this.navbar = new Navbar();
            ViewBag.Navbar = this.navbar;
            socialData = new SocialDTO();
            //socialData.FbAppId = ConfigurationManager.AppSettings["FBAppID"].ToString();
            socialData.Type = CONSTANTES.SOCIAL_DEFAULT_TYPE;
            socialData.Image = CONSTANTES.SOCIAL_DEFAULT_IMAGE;
            ViewBag.Social = this.socialData;
            ViewBag.Title = ConfigurationManager.AppSettings["DefaultTitle"].ToString();
        }

        protected virtual void cleanViewBag()
        {
            ViewBag.Name = null;
            ViewBag.ParentName = null;
            ViewBag.ParentUrl = null;
            ViewBag.Links = null;
            ViewBag.Title = null;
            ViewBag.Content = null;
            ViewBag.Highlights = null;
            ViewBag.Landing = null;
            ViewBag.Cover = null;
        }

        protected void createResponseMessage(string status, string message = "", string status_field = "status", string message_field = "message")
        {
            TempData[status_field] = status;
            if (!String.IsNullOrWhiteSpace(message))
            {
                TempData[message_field] = message;
            }
        }
        

    }
}
