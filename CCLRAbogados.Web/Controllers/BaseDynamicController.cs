using CCLRAbogados.Core;
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
    public class BaseDynamicController : BaseController
    {
        protected PaginaDTO absoluteParent { get; set; }
        public BaseDynamicController()
        {

        }

        protected bool showPagina(string controller, Uri url, string page, string id, string subid)
        {

            cleanViewBag();
            PaginasBL paginaBL = new PaginasBL();
            Pagina pagina = new Pagina();

            if (page != null)
            {
                if (subid != null) { pagina = paginaBL.getPagina(subid, id, page); }
                else if (id != null) { pagina = paginaBL.getPagina(id, page, controller); }
                else { pagina = paginaBL.getPagina(page, controller); }
                if (pagina != null)
                {
                    this.currentPage = pagina;

                    PaginaDTO paginaPadre = paginaBL.getPaginaById(pagina.Padre);
                    string baseUrl = url.GetLeftPart(UriPartial.Authority) + Url.Content("~/");
                    baseUrl = getFixedUrl(baseUrl);
                    if (pagina.Landing != null && pagina.Landing == true)
                    {
                        ViewBag.Name = pagina.Nombre;
                        ViewBag.ParentName = paginaPadre != null ? paginaPadre.Nombre : "";
                        ViewBag.ParentUrl = string.Format("{0}/{1}", baseUrl, paginaPadre.Path);
                    }
                    else
                    {
                        ViewBag.Name = paginaPadre != null ? paginaPadre.Nombre : pagina.Nombre;
                        if (paginaPadre.Landing != null && paginaPadre.Landing == true && paginaPadre.Padre != null)
                        {
                            PaginaDTO paginaPadrePadre = paginaBL.getPaginaById(paginaPadre.Padre);
                            ViewBag.ParentName = paginaPadrePadre != null ? paginaPadrePadre.Nombre : "";
                            ViewBag.ParentUrl = string.Format("{0}/{1}", baseUrl, paginaPadrePadre.Path);
                        }
                    }

                    ViewBag.Links = paginaBL.getSideBarLinks(pagina, url.AbsoluteUri);
                    ViewBag.Title = pagina.Titulo;
                    ViewBag.Content = pagina.Contenido;
                    ViewBag.Landing = pagina.Landing;
                    if (pagina.MostrarHighlights)
                        ViewBag.Highlights = paginaBL.getHighlightsByPaginaId(pagina.IdPagina);
                    if (pagina.MostrarCover)
                        ViewBag.Cover = pagina.Cover;
                    this.absoluteParent = paginaBL.getAbsoluteParent(pagina.IdPagina);
                    socialData.Title = pagina.Nombre;
                    socialData.Url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                    ViewBag.Social = socialData;
                    return true;
                }
                else
                {
                    throw new Exception("Página no existe");
                }
            }
            else
            {
                pagina = paginaBL.getPagina(controller);
                base.currentPage = pagina;
                ViewBag.Title = pagina.Titulo;
                ViewBag.Name = pagina.Nombre;
                ViewBag.Content = pagina.Contenido;
                ViewBag.Links = paginaBL.getSideBarLinks(pagina, HttpContext.Request.Url.AbsoluteUri);
                if (pagina.MostrarHighlights)
                    ViewBag.Highlights = paginaBL.getHighlightsByPaginaId(pagina.IdPagina);
                if (pagina.MostrarCover)
                    ViewBag.Cover = pagina.Cover;
                ViewBag.Landing = pagina.Landing;
                this.absoluteParent = paginaBL.getAbsoluteParent(pagina.IdPagina);
                socialData.Title = pagina.Nombre;
                socialData.Url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                ViewBag.Social = socialData;
                return false;
            }
        }



        private string getFixedUrl(string baseUrl)
        {
            int _length = baseUrl.Length;
            int _pos = baseUrl.LastIndexOf('/');
            if (_pos + 1 == _length)
            {
                baseUrl = baseUrl.Substring(0, _pos);
            }
            return baseUrl;
        }

    }
}
