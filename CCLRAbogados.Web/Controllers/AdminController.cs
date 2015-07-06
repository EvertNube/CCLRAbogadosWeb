using CCLRAbogados.Core;
using CCLRAbogados.Core.BL;
using CCLRAbogados.Core.DTO;
using CCLRAbogados.Core.Helpers;
using CCLRAbogados.Data;
using CCLRAbogados.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using CCLRAbogados.Web.Models;

namespace CCLRAbogados.Web.Controllers
{
    public class AdminController : Controller
    {
        protected Navbar navbar { get; set; }
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

        public AdminController()
        {
            UsuarioDTO user = getCurrentUser();
            if (user != null)
            {
                this.navbar = new Navbar();
                ViewBag.Navbar = this.navbar;

                ViewBag.currentUser = user;
                ViewBag.EsAdmin = isAdministrator();
                ViewBag.EsSuperAdmin = isSuperAdministrator();
            }
            else { ViewBag.EsAdmin = false; ViewBag.EsSuperAdmin = false; }
        }

        public ActionResult Index()
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }

            MenuNavBarSelected(1);

            PaginasBL paginasBL = new PaginasBL();
            IList<EnlaceDTO> paginas = paginasBL.getPaginasTree();
            return View(paginas);
        }

        public ActionResult Pagina(int id)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }

            MenuNavBarSelected(1);

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

        [HttpPost, ValidateInput(false)]
        public ActionResult PageUpdate(PaginaDTO pagina)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            PaginasBL paginasBL = new PaginasBL();
            if (paginasBL.validarCamposPagina(pagina))
            {
                if (paginasBL.updatePagina(pagina, false))
                {
                    createResponseMessage(CONSTANTES.SUCCESS);
                    return RedirectToAction("Pagina", new { id = pagina.IdPagina });
                }
            }
            createResponseMessage(CONSTANTES.ERROR);
            return RedirectToAction("Pagina", new { id = pagina.IdPagina });
        }

        private void createResponseMessage(string status, string message = "", string status_field = "status", string message_field = "message")
        {
            TempData[status_field] = status;
            if (!String.IsNullOrWhiteSpace(message))
            {
                TempData[message_field] = message;
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PageUpdateContent(PaginaDTO pagina)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            PaginasBL paginasBL = new PaginasBL();
            if (paginasBL.updatePagina(pagina))
            {
                createResponseMessage(CONSTANTES.SUCCESS, CONSTANTES.SUCCESS_MESSAGE);
            }
            else
            {
                createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_MESSAGE);
            }
            return RedirectToAction("Pagina", new { id = pagina.IdPagina });
        }

        public ActionResult NuevaPagina(int? id)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            PaginasBL paginasBL = new PaginasBL();
            PaginaDTO padre = paginasBL.getPaginaById(id);
            PaginaDTO pagina = TempData["Pagina"] as PaginaDTO;
            if (pagina == null) { pagina = new PaginaDTO(); }
            if (padre != null) pagina.Padre = padre.IdPagina;
            else pagina.Padre = null;
            ViewBag.Padre = padre;
            return View(pagina);
        }

        [HttpPost]
        public ActionResult PageCreate(PaginaDTO pagina)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }

            try
            {
                if (ModelState.IsValid)
                {
                    PaginasBL paginasBL = new PaginasBL();
                    paginasBL.addPagina(pagina, getCurrentUser());
                    createResponseMessage(CONSTANTES.SUCCESS, CONSTANTES.SUCCESS_MESSAGE);
                    return RedirectToAction("Index");
                }
                createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_MESSAGE);
            }
            catch
            {
                createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_MESSAGE);
            }
            TempData["Pagina"] = pagina;
            return RedirectToAction("NuevaPagina", new { id = pagina.Padre });


        }

        public ActionResult Usuarios()
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            if (!this.isAdministrator()) { return RedirectToAction("Index"); }

            MenuNavBarSelected(4);

            UsuariosBL usuariosBL = new UsuariosBL();
            return View(usuariosBL.getUsuarios());
        }

        public ActionResult Usuario(int? id = null)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            UsuarioDTO currentUser = getCurrentUser();
            if (!this.isAdministrator() && id != currentUser.IdUsuario) { return RedirectToAction("Index"); }
            if (id == 1 && !this.isSuperAdministrator()) { return RedirectToAction("Index"); }

            MenuNavBarSelected(4);

            UsuariosBL usuariosBL = new UsuariosBL();
            /*IEnumerable<RolDTO> roles = usuariosBL.getRoles();
            var rolesList = roles.ToList();
            rolesList.Insert(0, new RolDTO() { IdRol = 0, Nombre = "Seleccione un Rol" });
            ViewBag.Roles =  rolesList.AsEnumerable();*/
            IList<RolDTO> roles = usuariosBL.getRolesCurrent(this.getCurrentUser().IdRol);
            roles.Insert(0, new RolDTO() { IdRol = 0, Nombre = "Seleccione un Rol" });
            ViewBag.vbRls = roles;

            var objSent = TempData["Usuario"];
            if (objSent != null) { TempData["Usuario"] = null; return View(objSent); }
            if (id != null)
            {
                UsuarioDTO usuario = usuariosBL.getUsuario((int)id);
                ViewBag.vbRls = usuariosBL.getRolesCurrent(currentUser.IdRol);

                return View(usuario);
            }
            return View();
        }

        public ActionResult AddUser(UsuarioDTO user, string passUser = "", string passChange = "")
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            UsuarioDTO currentUser = getCurrentUser();
            if (!this.isAdministrator() && user.IdUsuario != currentUser.IdUsuario) { createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_ROL_PERMISSION); return RedirectToAction("Usuarios"); }
            if (user.IdRol == 1 && !this.isSuperAdministrator()) { createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_ROL_PERMISSION); return RedirectToAction("Usuarios"); }
            if (user.IdRol == 2 && !this.isAdministrator()) { createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_ROL_PERMISSION); return RedirectToAction("Usuarios"); }
            if (currentUser.IdRol == 2 && user.IdRol == 2 && user.IdUsuario != currentUser.IdUsuario) { createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_ROL_PERMISSION); return RedirectToAction("Usuarios"); }
            if (currentUser.IdRol >= 3 && user.IdUsuario != currentUser.IdUsuario) { createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_ROL_PERMISSION); return RedirectToAction("Usuarios"); }
            try
            {
                UsuariosBL usuariosBL = new UsuariosBL();
                if (user.IdUsuario == 0 && usuariosBL.validateUsuario(user))
                {
                    usuariosBL.add(user);
                    createResponseMessage(CONSTANTES.SUCCESS);
                    return RedirectToAction("Usuarios");
                }
                else if (user.IdUsuario != 0)
                {
                    if (usuariosBL.getUsuario(user.IdUsuario).IdRol == 1 && !this.isSuperAdministrator()) { createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_ROL_PERMISSION); return RedirectToAction("Usuarios"); }
                    if (usuariosBL.getUsuario(user.IdUsuario).IdRol == 2 && !this.isAdministrator()) { createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_ROL_PERMISSION); return RedirectToAction("Usuarios"); }

                    if (usuariosBL.update(user, passUser, passChange, this.getCurrentUser()))
                    {
                        createResponseMessage(CONSTANTES.SUCCESS);
                        if (user.IdUsuario == this.getCurrentUser().IdUsuario)
                        {
                            System.Web.HttpContext.Current.Session["User"] = usuariosBL.getUsuario(user.IdUsuario);
                            if (!this.getCurrentUser().Estado) System.Web.HttpContext.Current.Session["User"] = null;
                        }
                        return RedirectToAction("Usuarios");
                    }
                    else
                    {
                        //createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_UPDATE_MESSAGE + "<br>Si está intentando actualizar la contraseña, verifique que ha ingresado la contraseña actual correctamente.");
                        if (currentUser.IdRol <= 2)
                            createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_UPDATE_MESSAGE + "<br>Si está intentando actualizar una contraseña, verifique que conozca la <strong>actual contraseña del usuario a modificar</strong>.");
                        else
                            createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_UPDATE_MESSAGE + "<br>Si está intentando actualizar la contraseña, verifique que ha ingresado la contraseña actual correctamente.");
                    }

                }
                else
                {
                    createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_INSERT_MESSAGE);
                }
            }
            catch
            {
                if (user.IdUsuario != 0)
                    createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_UPDATE_MESSAGE);
                else createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_INSERT_MESSAGE);
            }
            TempData["Usuario"] = user;
            return RedirectToAction("Usuario");
        }

        public ActionResult Cargos()
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            if (!this.isAdministrator()) { return RedirectToAction("Index"); }

            MenuNavBarSelected(5);

            CargoBL cargosBL = new CargoBL();
            return View(cargosBL.getCargos());
        }
        public ActionResult Cargo(int? id = null)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            if (!this.isAdministrator()) { return RedirectToAction("Index"); }
            
            MenuNavBarSelected(5);

            CargoBL objBL = new CargoBL();
            ViewBag.IdEntidad = id;
            var objSent = TempData["Cargo"];
            if (objSent != null) { TempData["Cargo"] = null; return View(objSent); }
            if (id != null)
            {
                CargoDTO obj = objBL.getCargo((int)id);
                return View(obj);
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddCargo(CargoDTO dto)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            try
            {
                CargoBL objBL = new CargoBL();
                if (dto.IdCargo == 0)
                {
                    if (objBL.add(dto))
                    {
                        createResponseMessage(CONSTANTES.SUCCESS);
                        return RedirectToAction("Cargos");
                    }
                }
                else if (dto.IdCargo != 0)
                {
                    if (objBL.update(dto))
                    {
                        createResponseMessage(CONSTANTES.SUCCESS);
                        return RedirectToAction("Cargos");
                    }
                    else
                    {
                        createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_UPDATE_MESSAGE);
                    }

                }
                else
                {
                    createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_INSERT_MESSAGE);
                }
            }
            catch (Exception e)
            {
                if (dto.IdCargo != 0)
                    createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_UPDATE_MESSAGE);
                else createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_INSERT_MESSAGE);
            }
            TempData["Cargo"] = dto;
            return RedirectToAction("Cargo");
        }

        public ActionResult RecoverPage(int id)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            PaginasBL paginasBL = new PaginasBL();
            if (paginasBL.recoverPageById(id, (UsuarioDTO)Session["User"]))
            {
                createResponseMessage(CONSTANTES.SUCCESS, "<strong>Listo!</strong> Página recuperada satisfactoriamente");
            }
            else
            {
                createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_MESSAGE);
            }
            return RedirectToAction("Pagina", new { id = id });
        }

        public ActionResult Archivos()
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }

            MenuNavBarSelected(2);

            ArchivosBL archivosBL = new ArchivosBL();
            return View(archivosBL.getArchivos());
        }

        public ActionResult Subir()
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            MenuNavBarSelected(2);
            return View();
        }
        public ActionResult Ingresar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UsuarioDTO user)
        {
            if (ModelState.IsValid)
            {
                UsuariosBL usuariosBL = new UsuariosBL();
                if (usuariosBL.isValidUser(user))
                {
                    System.Web.HttpContext.Current.Session["User"] = usuariosBL.getUsuarioByCuenta(user);
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Ingresar");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Ingresar");
        }
        public ActionResult Agenda()
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            AgendaBL agendaBL = new AgendaBL();
            return View(agendaBL.getAgendas(activeOnly: false));
        }
        public ActionResult Evento(int? id = null)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            AgendaBL contextBL = new AgendaBL();

            var objSent = TempData["objSent"];
            if (objSent != null) { TempData["objSent"] = null; return View(objSent); }
            if (id != null) { return View(contextBL.getAgenda((int)id)); }
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult addEvent(Agenda entidad)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            AgendaBL objBL = new AgendaBL();
            try
            {
                string absolutePath = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/" + CONSTANTES.URI_AGENDA;
                objBL.add(entidad, absolutePath);
                createResponseMessage(CONSTANTES.SUCCESS);
                return RedirectToAction("Agenda");
            }
            catch
            {
                if (entidad.IdAgenda != 0)
                    createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_UPDATE_MESSAGE);
                else createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_INSERT_MESSAGE);
            }
            TempData["objSent"] = entidad;
            return RedirectToAction("Evento", new { id = entidad.IdAgenda != 0 ? entidad.IdAgenda.ToString() : "" });
        }

        public ActionResult Home(int? id = null)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            DynContentBL contextBL = new DynContentBL();
            ViewBag.Categorias = contextBL.getCategorias();
            ViewBag.SelectedCategory = id != null ? (int)id : 1;
            return View(contextBL.getElementsByCategoria(id != null ? (int)id : 1));
        }

        public ActionResult Elemento(int? id = null)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            DynContentBL contextBL = new DynContentBL();
            IList<TipoDynamicContent> categorias = contextBL.getCategorias();
            categorias.Insert(0, new TipoDynamicContent() { IdTipoDynamicContent = 0, Nombre = "Seleccione el tipo de contenido" });
            ViewBag.Categorias = categorias.AsEnumerable();
            var objSent = TempData["objSent"];
            if (objSent != null) { TempData["objSent"] = null; return View(objSent); }
            if (id != null) { return View(contextBL.getElementById((int)id)); }
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AddElement(DynamicContent entidad)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }

            DynContentBL contextBL = new DynContentBL();
            try
            {
                contextBL.add(entidad);
                createResponseMessage(CONSTANTES.SUCCESS);
                return RedirectToAction("Home");
            }
            catch
            {
                if (entidad.IdDynamicContent != 0)
                    createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_UPDATE_MESSAGE);
                else createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_INSERT_MESSAGE);
            }

            TempData["objSent"] = entidad;
            return RedirectToAction("Elemento", new { id = entidad.IdDynamicContent != 0 ? entidad.IdDynamicContent.ToString() : "" });
        }

        public ActionResult Highlight(int id)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }

            MenuNavBarSelected(1);

            PaginasBL paginasBL = new PaginasBL();
            HighLight paginaHighlight = paginasBL.getHighlight(id);
            if (paginaHighlight == null)
            {
                paginaHighlight = new HighLight();
                paginaHighlight.IdPagina = id;
                paginaHighlight = paginasBL.addHighlight(paginaHighlight);
            }
            ViewBag.PathNames = paginasBL.getPaginaById(id).PathNames;
            ViewBag.ClasesCss = ClasesCss.getLista();
            return View(paginaHighlight);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateHighlight(HighLight highlight)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            PaginasBL paginasBL = new PaginasBL();
            ViewBag.PathNames = paginasBL.getPaginaById(highlight.IdPagina).PathNames;
            ViewBag.ClasesCss = ClasesCss.getLista();
            if (ModelState.IsValid)
            {

                if (paginasBL.updateHighlight(highlight))
                {
                    createResponseMessage(CONSTANTES.SUCCESS, CONSTANTES.SUCCESS_MESSAGE);
                    return View("Highlight", highlight);
                }
            }
            createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_MESSAGE);
            return View("Highlight", highlight);
        }

        public ActionResult Miembros()
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            if (!isAdministrator()) { return RedirectToAction("Index"); }

            MenuNavBarSelected(3);

            MiembrosBL objBL = new MiembrosBL();
            return View(objBL.getMiembros());
        }

        public ActionResult Miembro(int? id = null)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            if (!this.isAdministrator()) { return RedirectToAction("Index"); }
            
            MenuNavBarSelected(3);

            MiembrosBL objBL = new MiembrosBL();
            ViewBag.IdEntidad = id;
            ViewBag.Cargos = objBL.getCargosViewBag(false);
            ViewBag.TipoExperiencias = objBL.getTipoExperiencias();

            var objSent = TempData["Miembro"];
            if (objSent != null) { TempData["Miembro"] = null; return View(objSent); }
            if (id != null)
            {
                MiembroDTO obj = objBL.getMiembro((int)id);
                return View(obj);
            }
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddMiembro(MiembroDTO dto)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            try
            {
                MiembrosBL objBL = new MiembrosBL();
                if (dto.IdMiembro == 0)
                {
                    string absolutePath = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/" + CONSTANTES.URI_MIEMBROS;

                    if (objBL.add(dto, absolutePath))
                    {
                        createResponseMessage(CONSTANTES.SUCCESS);
                        return RedirectToAction("Miembros");
                    }
                }
                else if (dto.IdMiembro != 0)
                {
                    if (objBL.update(dto))
                    {
                        createResponseMessage(CONSTANTES.SUCCESS);
                        return RedirectToAction("Miembros");
                    }
                    else
                    {
                        createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_UPDATE_MESSAGE);
                    }

                }
                else
                {
                    createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_INSERT_MESSAGE);
                }
            }
            catch (Exception e)
            {
                if (dto.IdMiembro != 0)
                    createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_UPDATE_MESSAGE);
                else createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_INSERT_MESSAGE);
            }
            TempData["Miembro"] = dto;
            return RedirectToAction("Miembro");
        }

        public ActionResult Experiencias(int idMiembro, int idTipoExperiencia)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            if (!isAdministrator()) { return RedirectToAction("Index"); }

            MenuNavBarSelected(3);

            MiembrosBL objBL = new MiembrosBL();
            ViewBag.IdMiembro = idMiembro;
            ViewBag.IdTipoExperiencia = idTipoExperiencia;
            ViewBag.NombreMiembro = idMiembro != 0 ? objBL.getMiembro(idMiembro).Nombre : "No asignado";
            ViewBag.NombreTipoExp = idTipoExperiencia != 0 ? objBL.getTipoExperiencia(idTipoExperiencia).Nombre : "Sin Tipo";
            return View(objBL.getExperienciasPorMiembroYPorTipo(idMiembro, idTipoExperiencia));
        }

        public ActionResult Experiencia(int? id = null, int? idTipoExperiencia = null, int? idMiembro = null)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            if (!this.isAdministrator()) { return RedirectToAction("Index"); }

            MenuNavBarSelected(3);

            MiembrosBL objBL = new MiembrosBL();
            ViewBag.IdExperiencia = id;
            ViewBag.IdMiembro = idMiembro;
            ViewBag.IdTipoExperiencia = idTipoExperiencia;
            ViewBag.NombreMiembro = idMiembro != null ? objBL.getMiembro(idMiembro.GetValueOrDefault()).Nombre : "No asignado";
            ViewBag.NombreTipoExp = idTipoExperiencia != null ? objBL.getTipoExperiencia(idTipoExperiencia.GetValueOrDefault()).Nombre : "Sin Tipo";
            //ViewBag.TipoExperiencias = objBL.getTipoExperienciasViewBag(false);

            var objSent = TempData["Experiencia"];
            if (objSent != null) { TempData["Experiencia"] = null; return View(objSent); }
            if (id != null && id != 0)
            {
                ExperienciaDTO obj = objBL.getExperiencia((int)id);
                return View(obj);
            }
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AddExperiencia(ExperienciaDTO dto)
        {
            if (!this.currentUser()) { return RedirectToAction("Ingresar"); }
            try
            {
                MiembrosBL objBL = new MiembrosBL();
                if (dto.IdExperiencia == 0)
                {
                    if (objBL.addExperiencia(dto))
                    {
                        createResponseMessage(CONSTANTES.SUCCESS);
                        return RedirectToAction("Experiencias", "Admin", new { idMiembro = dto.IdMiembro, idTipoExperiencia = dto.IdTipoExperiencia });
                        //return RedirectToAction("Miembro", new { id = dto.IdMiembro });
                    }
                }
                else if (dto.IdExperiencia != 0)
                {
                    if (objBL.updateExperiencia(dto))
                    {
                        createResponseMessage(CONSTANTES.SUCCESS);
                        return RedirectToAction("Experiencias", "Admin", new { idMiembro = dto.IdMiembro, idTipoExperiencia = dto.IdTipoExperiencia });
                        //return RedirectToAction("Miembro", new { id = dto.IdMiembro });
                    }
                    else
                    {
                        createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_UPDATE_MESSAGE);
                    }
                }
                else
                {
                    createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_INSERT_MESSAGE);
                }
            }
            catch (Exception e)
            {
                if (dto.IdExperiencia != 0)
                    createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_UPDATE_MESSAGE);
                else createResponseMessage(CONSTANTES.ERROR, CONSTANTES.ERROR_INSERT_MESSAGE);
            }
            TempData["Experiencia"] = dto;
            return RedirectToAction("Experiencia", "Admin", new { id = dto.IdExperiencia, idTipoExperiencia = dto.IdTipoExperiencia, idMiembro = dto.IdMiembro });
        }
        public ActionResult CambiarOrdenUp(int id)
        {
            MiembrosBL objBL = new MiembrosBL();
            objBL.OrdenSubir(objBL.getMiembro(id));
            return RedirectToAction("Miembros");
        }
        public ActionResult CambiarOrdenDown(int id)
        {
            MiembrosBL objBL = new MiembrosBL();
            objBL.OrdenBajar(objBL.getMiembro(id));
            return RedirectToAction("Miembros");
        }

        public void MenuNavBarSelected(int num)
        {
            navbar.clearAll();
            switch (num)
            {
                case 1:
                    navbar.dangerActive = "active";
                    break;
                case 2:
                    navbar.infoActive = "active";
                    break;
                case 3:
                    navbar.primaryActive = "active";
                    break;
                case 4:
                    navbar.warningActive = "active";
                    break;
                case 5:
                    navbar.successActive = "active";
                    break;
            }

            ViewBag.navbar = navbar;
        }
    }
}
