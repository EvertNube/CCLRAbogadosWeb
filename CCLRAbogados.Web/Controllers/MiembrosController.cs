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
        public ActionResult Index(string page, string id, int? pagina)
        {
            cleanViewBag();

            MiembrosBL objBL = new MiembrosBL();
            base.getPaginaInfo(CONSTANTES.URI_MIEMBROS);
            ViewBag.page = page ?? "";
            ViewBag.id = id ?? "";

            if(!String.IsNullOrWhiteSpace(page))
            {
                //ViewBag.TipoExperiencias = objBL.getTipoExperienciasViewBag(false);
                int idMiembro = (objBL.getMiembroPorUri(page)).IdMiembro;
                ViewBag.TipoExperiencias = objBL.getExperienciasPorMiembro(idMiembro);
                return View(getPaginaMiembro(objBL, page, id));
            }
            ViewBag.Cargos = objBL.getCargosViewBag(false);
            return View(objBL.getMiembrosActivos());
        }

        private MiembroDTO getPaginaMiembro(MiembrosBL miembrosBL, string page, string id)
        {
            //int num = Int32.Parse(id);
            MiembroDTO miembro = miembrosBL.getMiembroPorUri(page);
            if(miembro.IdMiembro != 0)
            { 
                ViewBag.ParentName = "Miembros";
                ViewBag.ParentUrl = Url.Action("Index", "Miembros", new { page = "", id = "" });
                ViewBag.Miembro = true;
                ViewBag.Title = miembro.Titulo;

                socialData.Title = miembro.Titulo;
                socialData.ShortUrl = miembro.ShortUrl;

                ViewBag.Social = socialData;
                return miembro;
            }
            return null;
        }

        protected override void cleanViewBag()
        {
            base.cleanViewBag();
            ViewBag.Miembros = null;
            ViewBag.Miembro = null;
        }
    }
}
