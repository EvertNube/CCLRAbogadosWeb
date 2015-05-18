using CCLRAbogados.Core.DTO;
using CCLRAbogados.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogados.Core.BL
{
    public class NavigationBL:PaginasBL
    {
        public IList<EnlaceDTO> getNavigationLinks(string URI, string URI_PADRE = null)
        {
            using (var context = getContext())
            {
                Pagina pagina = getPagina(URI, URI_PADRE);
                if (pagina != null)
                {
                    var result = from r in context.Pagina
                                 where r.Estado == true & r.Padre == pagina.IdPagina & r.OnNavigation == true
                                 orderby r.Orden ascending
                                 select new EnlaceDTO
                                 {
                                     IdEnlace = r.IdPagina,
                                     Padre = r.Padre,
                                     Titulo = r.Nombre,
                                     Url = (r.EsEnlaceExterno == false || r.EsEnlaceExterno == null) ? r.Uri : r.EnlaceExterno,
                                     Orden = r.Orden,
                                     EsEnlaceExterno = r.EsEnlaceExterno,
                                     Target = r.EnlaceExternoTarget
                                 };
                    IList<EnlaceDTO> lista = result.ToList<EnlaceDTO>();
                    foreach (var enlace in lista){
                        if (!enlace.EsEnlaceExterno)
                            enlace.Url = getPaginaPath(enlace.IdEnlace)["path"];
                    }
                    return lista;
                }
                return null;
            }
        }

        public IDictionary<CategoriaPagina, IList<EnlaceDTO>> getGroupedNavigationLinks(string URI)
        {
            using (var context = getContext())
            {
                Pagina pagina = getPagina(URI);
                if (pagina != null)
                {

                    var result = from r in context.CategoriaPagina
                                 join c in context.Pagina on r.IdCategoriaPagina equals c.IdCategoriaPagina
                                 where c.Estado == true & c.Padre == pagina.IdPagina
                                 group r by r.IdCategoriaPagina;

                    IList<CategoriaPagina> categorias = result.SelectMany(categoria => categoria).Distinct().ToList<CategoriaPagina>();
                    IDictionary<CategoriaPagina, IList<EnlaceDTO>> lista = new Dictionary<CategoriaPagina, IList<EnlaceDTO>>();
                    foreach (var cat in categorias)
                    {
                        var enlaces = context.Pagina.Where(x => x.Estado == true & x.IdCategoriaPagina == cat.IdCategoriaPagina
                                                           & x.OnNavigation == true & x.Padre == pagina.IdPagina)
                                                    .OrderBy(x => x.Orden)
                                                    .Select(x => new EnlaceDTO
                                                    {
                                                        IdEnlace = x.IdPagina,
                                                        Padre = x.Padre,
                                                        Titulo = x.Titulo,
                                                        Url = (x.EsEnlaceExterno == false || x.EsEnlaceExterno == null) ? x.Uri : x.EnlaceExterno,
                                                        Orden = x.Orden,
                                                        Target = x.EnlaceExternoTarget
                                                    }).ToList<EnlaceDTO>();
                        foreach (var enlace in enlaces)
                        {
                            if (!enlace.EsEnlaceExterno)
                                enlace.Url = getPaginaPath(enlace.IdEnlace)["path"];
                        }
                        lista.Add(cat, enlaces);
                    }

                    return lista;
                }
                return null;
            }
        }
    }
}
