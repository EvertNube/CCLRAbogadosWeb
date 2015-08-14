﻿using CCLRAbogados.Core;
using CCLRAbogados.Core.BL;
using CCLRAbogados.Core.DTO;
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
    public class confidencialController : BaseDynamicController
    {
        public confidencialController()
        {
            base.navbar.clearAll();
            ViewBag.Navbar = base.navbar;
        }
        public ActionResult Index(string page)
        {
            showPagina(CONSTANTES.URI_CONFIDENCIAL, HttpContext.Request.Url, page, "", "");
            return View("Index");
        }
    }
}