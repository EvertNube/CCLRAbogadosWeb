using CCLRAbogados.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogados.Core.BL
{
    public class DynContentBL:Base
    {
        public IList<TipoDynamicContent> getCategorias()
        {
            using (var context = getContext())
            {
                return context.TipoDynamicContent.ToList();
            }
        }

        public IList<DynamicContent> getElementsByCategoria(int IdCategoria)
        {
            using (var context = getContext())
            {
                var result = from r in context.DynamicContent
                             where r.IdTipoDynamicContent == IdCategoria
                             orderby r.Orden
                             select r;
                return result.ToList<DynamicContent>();
            }   
        }

        public bool add(DynamicContent _element)
        {
            using (var context = getContext())
            {
                try
                {
                    if (_element.IdDynamicContent == 0)
                    {
                        _element.Estado = true;
                        context.DynamicContent.Add(_element);
                    }
                    else{
                        DynamicContent element = context.DynamicContent.Where(x => x.IdDynamicContent == _element.IdDynamicContent).SingleOrDefault();
                        element.Titulo = _element.Titulo;
                        element.Descripcion = _element.Descripcion;
                        element.Url = _element.Url;
                        element.IdTipoDynamicContent = _element.IdTipoDynamicContent;
                        element.Imagen = _element.Imagen;
                        element.Orden = _element.Orden;
                        element.Estado = _element.Estado;
                    }
                    context.SaveChanges();
                    return true;
                }
                catch (Exception e) {
                    throw e;
                    //return false;
                }
            }
        }

        public DynamicContent getElementById(int id)
        {
            using (var context = getContext())
            {
                return context.DynamicContent.Where(x => x.IdDynamicContent == id).SingleOrDefault();
            } 
        }
    }
}
