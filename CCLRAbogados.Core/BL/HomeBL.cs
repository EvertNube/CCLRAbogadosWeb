using CCLRAbogados.Core.DTO;
using CCLRAbogados.Data;
using CCLRAbogados.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogados.Core.BL
{
    public class HomeBL: Base
    {
        public IList<DynamicContent> getBanners()
        {
            DynContentBL contextBL = new DynContentBL();
            return contextBL.getElementsByCategoria(CONSTANTES.TIPO_BANNER_ID).OrderBy(x=>x.Orden).Where(x=>x.Estado == true).ToList();
        }

        public IList<DynamicContent> getMiniBanners()
        {
            DynContentBL contextBL = new DynContentBL();
            return contextBL.getElementsByCategoria(CONSTANTES.TIPO_MINIBANNER_ID).OrderBy(x => x.Orden).Where(x => x.Estado == true).ToList();
        }

        public IDictionary<int, DynamicContent> getEnlacesGris()
        {
            DynContentBL contextBL = new DynContentBL();
            IDictionary<int, DynamicContent> dict = new Dictionary<int, DynamicContent>();
            var enlaces = contextBL.getElementsByCategoria(CONSTANTES.TIPO_ENLACESGRIS_ID).Where(x=>x.Estado == true)
                                                                                          .OrderBy(x=>x.Orden)
                                                                                          .Take(4).ToList();
            int cont = 1;
            foreach (var e in enlaces){
                dict.Add(cont,e);
                cont++;
            }

            return dict;
        }

        public IList<Agenda> getEventos()
        {
            AgendaBL contextBL = new AgendaBL();
            return contextBL.getAgendas(DateTime.Today, limit: 3).ToList();
        }

        public IList<DynamicContent> getSocial()
        {
            DynContentBL contextBL = new DynContentBL();
            return contextBL.getElementsByCategoria(CONSTANTES.TIPO_SOCIAL_ID).OrderBy(x => x.Orden).Where(x => x.Estado == true).ToList();
        }

        public IList<DynamicContent> getEnlacesFooter()
        {
            DynContentBL contextBL = new DynContentBL();
            return contextBL.getElementsByCategoria(CONSTANTES.TIPO_ENLACESFOOTER_ID).OrderBy(x => x.Orden).Where(x => x.Estado == true).ToList();
        }

        public DynamicContent getYoutube()
        {
            DynContentBL contextBL = new DynContentBL();
            return contextBL.getElementsByCategoria(CONSTANTES.TIPO_YOUTUBE_ID).Where(x => x.Estado == true).OrderBy(x => x.Orden).FirstOrDefault();
        }
    }
}
