using CCLRAbogados.Data;
using CCLRAbogados.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogados.Core.BL
{
    public class ArchivosBL: Base
    {
        public IList<Archivo> getArchivos()
        {
            using (var context = getContext())
            {
                var result = from r in context.Archivo
                             where r.Estado == true
                             select r;
                return result.AsEnumerable<Archivo>().OrderByDescending(x=>x.IdArchivo).ToList<Archivo>();
            }
        }
        public bool add(Archivo archivo, string baseUrl = "")
        {
            using (var context = getContext()) {
                try
                {
                    var urlToEncode = baseUrl + "/" + archivo.Uri;
                    archivo.ShortUrl = ShortUrl.Shorten(urlToEncode);
                    context.Archivo.Add(archivo);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception e) {
                    throw e;
                }
            }
        }
        public Archivo getByUri(string uri) {
            using (var context = getContext())
            {
                var result = from r in context.Archivo
                                where r.Estado == true & r.Uri == uri
                                select r;
                return result.SingleOrDefault<Archivo>();
            }
        }
    }
}
