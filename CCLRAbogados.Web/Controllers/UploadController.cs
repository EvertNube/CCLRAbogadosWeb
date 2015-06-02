using CCLRAbogados.Core.BL;
using CCLRAbogados.Data;
using CCLRAbogados.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;

namespace CCLRAbogados.Web.Controllers
{
    public class UploadController : ApiController
    {
        public HttpResponseMessage Upload()
        {
            // Get a reference to the file that our jQuery sent.  Even with multiple files, they will all be their own request and be the 0 index

            if (HttpContext.Current.Request.Files.Count > 0)
            {
                var file = HttpContext.Current.Request.Files[0];
                var filesUploaded = new List<Archivo>();
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var extension = Path.GetExtension(file.FileName);
                    var uri = GenerateGUID.GenerateId();
                    var path = Path.Combine(HttpContext.Current.Server.MapPath(CONSTANTES.FILES_PATH), uri + extension);
                    file.SaveAs(path);
                    ArchivosBL archivosBL = new ArchivosBL();
                    var archivo = new Archivo()
                    {
                        Nombre = fileName,
                        Extension = extension,
                        Estado = true,
                        Uri = uri
                    };
                    string absolutePath = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/" + CONSTANTES.URI_ARCHIVO;
                    archivosBL.add(archivo, absolutePath);
                    filesUploaded.Add(archivo);
                }
                // Now we need to wire up a response so that the calling script understands what happened
                HttpContext.Current.Response.ContentType = "text/plain";
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                var result = System.Web.Helpers.Json.Encode(filesUploaded);

                HttpContext.Current.Response.Write(serializer.Serialize(result));
                HttpContext.Current.Response.StatusCode = 200;
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }


        }
    }
}
