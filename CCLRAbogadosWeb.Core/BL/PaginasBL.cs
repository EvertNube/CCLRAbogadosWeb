using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCLRAbogadosWeb.Data;
using CCLRAbogadosWeb.Core.DTO;
using CCLRAbogadosWeb.Helpers;

namespace CCLRAbogadosWeb.Core
{
    public class PaginasBL : Base
    {
        public Pagina getPagina(string subid, string id = null, string padre = null)
        {
            using (var context = getContext())
            {
                if (padre == null)
                {
                    if (id == null)
                    {
                        var result = context.Pagina.SingleOrDefault(x => x.Estado == true && x.Padre == null && x.Uri == subid);
                        return result;
                    }
                    else
                    {
                        var result = from r in context.Pagina
                                     join p in context.Pagina on r.Padre equals p.IdPagina
                                     where p.Estado == true & p.Uri == id &
                                           r.Estado == true & r.Uri == subid
                                     select r;
                        return result.SingleOrDefault<Pagina>();
                    }
                }
                else
                {
                    var result = from r in context.Pagina
                                 join p in context.Pagina on r.Padre equals p.IdPagina
                                 join q in context.Pagina on p.Padre equals q.IdPagina
                                 where q.Estado == true & q.Uri == padre &
                                       p.Estado == true & p.Uri == id &
                                       r.Estado == true & r.Uri == subid
                                 select r;

                    return result.SingleOrDefault<Pagina>();
                }
            }
        }
        public PaginaDTO getPaginaById(int? id, bool content = false)
        {
            using (var context = getContext())
            {
                var result = from r in context.Pagina
                             where r.IdPagina == id
                             select new PaginaDTO
                             {
                                 IdPagina = r.IdPagina,
                                 Nombre = r.Nombre,
                                 Titulo = r.Titulo,
                                 Estado = r.Estado,
                                 Padre = r.Padre,
                                 Uri = r.Uri,
                                 Landing = r.Landing,
                                 LandingNotNull = r.Landing != null ? (bool)r.Landing : false,
                                 Orden = r.Orden,
                                 Contenido = content == true ? r.Contenido : "",
                                 EsEnlaceExterno = r.EsEnlaceExterno,
                                 EnlaceExterno = r.EnlaceExterno,
                                 EstadoNotNull = r.Estado != null ? (bool)r.Estado : false,
                                 OnSideBar = r.OnSideBar,
                                 OnNavigation = r.OnNavigation,
                                 MostrarHighlights = r.MostrarHighlights,
                                 IdCategoriaPagina = r.IdCategoriaPagina,
                                 EnlaceExternoTarget = r.EnlaceExternoTarget,
                                 Cover = r.Cover,
                                 MostrarCover = r.MostrarCover
                             };
                PaginaDTO pagina = result.SingleOrDefault<PaginaDTO>();
                if (pagina != null)
                {
                    IDictionary<string, string> paths = getPaginaPath(pagina.IdPagina);
                    if (!pagina.EsEnlaceExterno)
                    {
                        pagina.Path = paths["path"];
                    }
                    else pagina.Path = pagina.EnlaceExterno;
                    pagina.PathNames = paths["pathNames"];
                }
                return pagina;
            }
        }

        public IDictionary<string, string> getPaginaPath(int? id, bool root = true)
        {
            using (var context = getContext())
            {
                StringBuilder path = new StringBuilder();
                StringBuilder pathNames = new StringBuilder();
                var result = from r in context.Pagina
                             where r.IdPagina == id
                             select new EnlaceDTO()
                             {
                                 Url = r.Uri,
                                 Titulo = r.Nombre,
                                 Padre = r.Padre
                             };
                EnlaceDTO pagina = result.SingleOrDefault<EnlaceDTO>();
                if (pagina.Padre != null)
                {
                    IDictionary<string, string> temp = getPaginaPath(pagina.Padre);
                    path.AppendFormat("{0}/{1}", temp["path"], pagina.Url);
                    pathNames.AppendFormat("{0} > {1}", temp["pathNames"], pagina.Titulo);
                }
                else
                {
                    if (root) path.AppendFormat("/{0}", pagina.Url);
                    else path.AppendFormat("{0}", pagina.Url);
                    pathNames.AppendFormat("{0}", pagina.Titulo);
                }
                IDictionary<string, string> paths = new Dictionary<string, string>();
                paths.Add("path", path.ToString());
                paths.Add("pathNames", pathNames.ToString());
                return paths;
            }

        }

        public bool updatePagina(PaginaDTO page, bool onlyContent = true)
        {
            using (var context = getContext())
            {
                try
                {
                    if (onlyContent)
                    {
                        Pagina pagina = context.Pagina.SingleOrDefault<Pagina>(x => x.IdPagina == page.IdPagina);
                        PaginaHistorial historial = context.PaginaHistorial.SingleOrDefault<PaginaHistorial>(x => x.IdPagina == page.IdPagina);

                        if (historial != null)
                        {
                            historial.Contenido = pagina.Contenido;
                            historial.FechaActualizacion = pagina.FechaActualizacion;
                            historial.IdUsuarioActualizacion = pagina.IdUsuarioAcualizacion;
                        }
                        else
                        {
                            historial = new PaginaHistorial()
                            {
                                IdPagina = pagina.IdPagina,
                                Contenido = pagina.Contenido,
                                IdUsuarioActualizacion = pagina.IdUsuarioAcualizacion,
                                FechaActualizacion = DateTime.Now
                            };
                            context.PaginaHistorial.Add(historial);
                        }

                        pagina.Contenido = page.Contenido;
                        pagina.FechaActualizacion = DateTime.Now;

                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        Pagina pagina = context.Pagina.SingleOrDefault<Pagina>(x => x.IdPagina == page.IdPagina);
                        pagina.Nombre = page.Nombre;
                        pagina.Uri = page.Uri;
                        pagina.Titulo = page.Titulo;
                        pagina.Orden = page.Orden;
                        pagina.OnSideBar = page.OnSideBar;
                        pagina.Landing = page.LandingNotNull;
                        pagina.EsEnlaceExterno = page.EsEnlaceExterno;
                        pagina.EnlaceExterno = page.EnlaceExterno;
                        pagina.Estado = page.EstadoNotNull;
                        pagina.OnNavigation = page.OnNavigation;
                        pagina.MostrarHighlights = page.MostrarHighlights;
                        pagina.EnlaceExternoTarget = page.EnlaceExternoTarget;
                        pagina.Cover = page.Cover;
                        pagina.MostrarCover = page.MostrarCover;
                        context.SaveChanges();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public bool validarCamposPagina(PaginaDTO pagina)
        {
            if (pagina.EsEnlaceExterno &&
                String.IsNullOrEmpty(pagina.EnlaceExterno))
                return false;
            if (String.IsNullOrEmpty(pagina.Nombre) ||
                String.IsNullOrEmpty(pagina.Titulo) ||
                String.IsNullOrEmpty(pagina.Uri))
                return false;
            return true;
        }

        public IList<EnlaceDTO> getSideBarLinks(Pagina pagina, string baseUrl)
        {
            baseUrl = getFixedUrl(baseUrl);
            using (var context = getContext())
            {
                int? id = null;
                if (pagina.Landing != null && pagina.Landing == true)
                {
                    id = pagina.IdPagina;
                    baseUrl = string.Format("{0}{1}/", baseUrl, pagina.Uri);
                }
                else if (pagina.Landing != null && pagina.Landing == false) { id = pagina.Padre; }

                var result = from r in context.Pagina
                             where r.Estado == true & r.Padre == id & r.OnSideBar == true
                             select new EnlaceDTO
                             {
                                 IdEnlace = r.IdPagina,
                                 Padre = r.Padre,
                                 Titulo = r.Nombre,
                                 Url = (r.EsEnlaceExterno == false || r.EsEnlaceExterno == null) ? baseUrl + r.Uri : r.EnlaceExterno,
                                 Orden = r.Orden,
                                 EsEnlaceExterno = r.EsEnlaceExterno,
                                 Target = r.EnlaceExternoTarget
                             };
                IList<EnlaceDTO> links = result.AsEnumerable<EnlaceDTO>().OrderBy(x => x.Orden).ToList<EnlaceDTO>();
                if (links != null && links.Count > 0)
                {
                    if (pagina.Landing != null && pagina.Landing == true && pagina.Padre != null)
                    {
                        links.Insert(0, getCurrentPageLink(pagina.IdPagina, baseUrl));
                    }
                    else if (pagina.Landing != null && pagina.Landing == false)
                    {
                        links.Insert(0, getCurrentPageLink(pagina.Padre, baseUrl));
                    }
                }

                return links;
            }
        }

        private string getFixedUrl(string baseUrl)
        {
            int _length = baseUrl.Length;
            int _pos = baseUrl.LastIndexOf('/');
            baseUrl = baseUrl.Substring(0, _pos);
            if (_pos + 1 == _length)
            {
                _pos = baseUrl.LastIndexOf('/') + 1;
                baseUrl = baseUrl.Substring(0, _pos);
            }
            else { baseUrl += "/"; }

            return baseUrl;
        }

        public EnlaceDTO getCurrentPageLink(int? id, string baseUrl)
        {
            PaginaDTO page = getPaginaById(id);
            EnlaceDTO currentPage = new EnlaceDTO()
            {
                IdEnlace = page.IdPagina,
                Padre = page.Padre,
                Titulo = CONSTANTES.CURRENT_PAGE_NAME,
                Url = baseUrl,
                Orden = page.Orden
            };
            return currentPage;
        }

        public IList<EnlaceDTO> getPaginasTree(int? id = null)
        {
            using (var context = getContext())
            {
                var result = from r in context.Pagina
                             where (id == null ? r.Padre == null : r.Padre == id)
                             select new EnlaceDTO
                             {
                                 IdEnlace = r.IdPagina,
                                 Padre = r.Padre,
                                 Titulo = r.Nombre,
                                 Url = (r.EsEnlaceExterno == false || r.EsEnlaceExterno == null) ? r.Uri : r.EnlaceExterno,
                                 Orden = r.Orden,
                                 Landing = r.Landing,
                                 EsEnlaceExterno = r.EsEnlaceExterno,
                                 Target = r.EnlaceExternoTarget,
                                 Estado = r.Estado ?? false
                             };
                IList<EnlaceDTO> paginasTree = result.AsEnumerable<EnlaceDTO>().OrderBy(x => x.Orden).ToList<EnlaceDTO>();

                foreach (EnlaceDTO enlace in paginasTree)
                {
                    if (enlace.Landing != null && enlace.Landing != false)
                    { enlace.Hijos = getPaginasTree(enlace.IdEnlace); }
                    enlace.Path = (enlace.EsEnlaceExterno == false) ? getPaginaPath(enlace.IdEnlace)["path"] : enlace.Url;
                }

                return paginasTree;
            }
        }

        public bool addPagina(PaginaDTO page, UsuarioDTO user)
        {
            using (var context = getContext())
            {
                try
                {
                    if ((page.Padre != null && !getPaginaById(page.Padre).EsEnlaceExterno)
                        || (page.Padre == null && user.IdRol <= 2))
                    {
                        Pagina pagina = new Pagina();
                        pagina.Nombre = page.Nombre;
                        pagina.Landing = page.LandingNotNull;
                        pagina.Titulo = page.Titulo;
                        pagina.Uri = page.Uri;
                        pagina.Padre = page.Padre;
                        pagina.IdUsuarioAcualizacion = user.IdUsuario;
                        pagina.Estado = true;
                        pagina.FechaActualizacion = DateTime.Now;
                        pagina.Orden = 1;
                        pagina.Contenido = String.Format("<h3>{0}</h3>", pagina.Nombre);
                        pagina.OnSideBar = true;
                        pagina.EsEnlaceExterno = page.EsEnlaceExterno;
                        pagina.EnlaceExterno = page.EnlaceExterno;
                        pagina.EnlaceExternoTarget = false;
                        pagina.MostrarHighlights = true;
                        pagina.OnNavigation = true;
                        pagina.EnlaceExternoTarget = false;
                        pagina.MostrarCover = false;
                        context.Pagina.Add(pagina);
                        context.SaveChanges();
                        return true;
                    }
                    else return false;
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
        }

        public bool recoverPageById(int id, UsuarioDTO user)
        {
            using (var context = getContext())
            {
                var pagina = context.Pagina.SingleOrDefault<Pagina>(x => x.IdPagina == id);
                if (pagina != null)
                {
                    var historial = context.PaginaHistorial.SingleOrDefault<PaginaHistorial>(x => x.IdPagina == pagina.IdPagina);
                    if (historial != null)
                    {
                        try
                        {
                            PaginaDTO paginaDTO = new PaginaDTO();
                            paginaDTO.IdPagina = pagina.IdPagina;
                            paginaDTO.Contenido = pagina.Contenido;
                            paginaDTO.FechaActualizacion = pagina.FechaActualizacion;
                            paginaDTO.IdUsuarioActualizacion = pagina.IdUsuarioAcualizacion;

                            pagina.Contenido = historial.Contenido;
                            pagina.FechaActualizacion = historial.FechaActualizacion;
                            pagina.IdUsuarioAcualizacion = user.IdUsuario;

                            historial.Contenido = paginaDTO.Contenido;
                            historial.FechaActualizacion = paginaDTO.FechaActualizacion;
                            historial.IdUsuarioActualizacion = paginaDTO.IdUsuarioActualizacion;

                            context.SaveChanges();
                            return true;
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }
                }
                return false;
            }
        }

        public Highlight getHighlight(int id)
        {
            using (var context = getContext())
            {
                return context.Highlight.SingleOrDefault(x => x.IdPagina == id);
            }
        }

        public Highlight addHighlight(Highlight highlight)
        {
            using (var context = getContext())
            {
                try
                {
                    highlight.Orden = 1;
                    highlight.Estado = true;
                    context.Highlight.Add(highlight);
                    context.SaveChanges();
                    return highlight;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public bool updateHighlight(Highlight _highlight)
        {
            using (var context = getContext())
            {
                try
                {
                    Highlight highlight = context.Highlight.SingleOrDefault(x => x.IdPagina == _highlight.IdPagina);
                    highlight.Nombre = _highlight.Nombre;
                    highlight.Descripcion = _highlight.Descripcion;
                    highlight.Resumen = _highlight.Resumen;
                    highlight.Css = _highlight.Css;
                    highlight.Image = _highlight.Image;
                    highlight.Orden = _highlight.Orden;
                    highlight.Estado = _highlight.Estado;

                    context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public IDictionary<string, Highlight> getHighlightsByPaginaId(int id)
        {
            using (var context = getContext())
            {
                var result = from r in context.Highlight
                             join p in context.Pagina on r.IdPagina equals p.IdPagina
                             where p.Estado == true & p.Padre == id &
                                   r.Estado == true
                             orderby r.Orden ascending
                             select r;

                IDictionary<string, Highlight> lista = new Dictionary<string, Highlight>();
                PaginaDTO enlace;
                foreach (var item in result)
                {
                    enlace = getPaginaById(item.IdPagina);
                    if (!enlace.EsEnlaceExterno)
                    {
                        IDictionary<string, string> paths = getPaginaPath(item.IdPagina);
                        lista.Add(paths["path"], item);
                    }
                    else lista.Add(enlace.EnlaceExterno, item);
                }
                return lista;
            }
        }

        public IDictionary<CategoriaPagina, Dictionary<string, Highlight>> getGroupedHighlights(int id)
        {
            using (var context = getContext())
            {
                var result = from r in context.CategoriaPagina
                             join p in context.Pagina on r.IdCategoriaPagina equals p.IdCategoriaPagina
                             join h in context.Highlight on p.IdPagina equals h.IdPagina
                             where p.Estado == true & p.Padre == id & h.Estado == true
                             group r by r.IdCategoriaPagina;

                IList<CategoriaPagina> categorias = result.SelectMany(x => x).Distinct().ToList<CategoriaPagina>();

                var lista = new Dictionary<CategoriaPagina, Dictionary<string, Highlight>>();
                foreach (var categoria in categorias)
                {
                    var listaHighlights = new Dictionary<string, Highlight>();
                    var highlights = from h in context.Highlight
                                     join p in context.Pagina on h.IdPagina equals p.IdPagina
                                     join c in context.CategoriaPagina on p.IdCategoriaPagina equals c.IdCategoriaPagina
                                     where p.Estado == true & p.Padre == id & h.Estado == true
                                           & c.IdCategoriaPagina == categoria.IdCategoriaPagina
                                     select h;
                    PaginaDTO enlace;
                    foreach (var h in highlights)
                    {
                        enlace = getPaginaById(h.IdPagina);
                        if (!enlace.EsEnlaceExterno)
                        {
                            listaHighlights.Add(getPaginaPath(h.IdPagina)["path"], h);
                        }
                        else listaHighlights.Add(enlace.EnlaceExterno, h);
                    }
                    lista.Add(categoria, listaHighlights);
                }

                return lista;
            }
        }


        public PaginaDTO getAbsoluteParent(int id)
        {
            using (var context = getContext())
            {
                Pagina pagina = context.Pagina.Where(x => x.IdPagina == id).SingleOrDefault();
                if (pagina.Padre == null) return new PaginaDTO() { IdPagina = pagina.IdPagina, Cover = pagina.Cover, Uri = pagina.Uri, Estado = pagina.Estado, MostrarHighlights = pagina.MostrarHighlights, MostrarCover = pagina.MostrarCover };
                else return getAbsoluteParent((int)pagina.Padre);
            }

        }
    }
}
