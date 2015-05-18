using CCLRAbogados.Data;
using CCLRAbogados.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogados.Core.BL
{
    public class AgendaBL:Base
    {
        public IList<Agenda> getAgendas(DateTime fecha = default(DateTime), int? limit = null, bool activeOnly = true) {
            using (var context = getContext()) {

                if (fecha != default(DateTime))
                {
                    var result = activeOnly ? from r in context.Agenda
                                              where r.Estado == true & r.Fecha >= fecha
                                              orderby r.Fecha ascending
                                              select r :
                                 from r in context.Agenda
                                 where r.Fecha >= fecha
                                 orderby r.Fecha ascending
                                 select r;
                    return result.AsEnumerable<Agenda>().Take(limit != null ? (int)limit : result.AsEnumerable<Agenda>().Count()).ToList();
               }
                else {
                    var result = activeOnly ? from r in context.Agenda
                                              where r.Estado == true
                                              orderby r.Fecha descending
                                              select r :
                                 from r in context.Agenda
                                 orderby r.Fecha descending
                                 select r;
                    return result.AsEnumerable<Agenda>().Take(limit != null ? (int)limit : result.AsEnumerable<Agenda>().Count()).ToList();
                }
            }
        }

        public Agenda getAgenda(int id){
            using (var context = getContext())
            {
                return context.Agenda.SingleOrDefault<Agenda>(x => x.IdAgenda == id);
            }
        }
        public bool add(Agenda _agenda, string baseUrl = "")
        {
            using (var context = getContext())
            {
                try
                {
                    if (_agenda.IdAgenda == 0)
                    {
                        var urlToEncode = baseUrl + "/" + _agenda.Uri;
                        _agenda.ShortUrl = ShortUrl.Shorten(urlToEncode);
                        _agenda.Estado = true;
                        context.Agenda.Add(_agenda);
                    }
                    else {
                        Agenda agenda = context.Agenda.Where(x => x.IdAgenda == _agenda.IdAgenda).SingleOrDefault();
                        agenda.Titulo = _agenda.Titulo;
                        agenda.Texto = _agenda.Texto;
                        agenda.Uri = _agenda.Uri;
                        agenda.Resumen = _agenda.Resumen;
                        agenda.Lugar = _agenda.Lugar;
                        agenda.Hora = _agenda.Hora;
                        agenda.Fecha = _agenda.Fecha;
                        agenda.Estado = _agenda.Estado;
                    }
                    context.SaveChanges();
                    return true;
                }
                catch (Exception e){
                    throw e;
                }
            }
        }

        public Agenda getAgendaByUri(string page)
        {
            using (var context = getContext())
            {
                return context.Agenda.SingleOrDefault(x => x.Uri == page && x.Estado == true);
            }
        }
    }
}
