using CCLRAbogados.Core.BL;
using CCLRAbogados.Data;
using CCLRAbogados.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCLRAbogados.Web.Controllers
{
    public class ArchivoController : BaseController
    {
        //
        // GET: /Archivos/
        public FileStreamResult Index(string path)
        {
            ArchivosBL archivosBL = new ArchivosBL();
            Archivo archivo = archivosBL.getByUri(path);
            if (archivo != null)
            {
                var filePath = Server.MapPath(CONSTANTES.FILES_PATH) + archivo.Uri + archivo.Extension;
                string contentType = "application/octet-stream";
                switch (archivo.Extension.ToLower())
                {
                    case ".doc": contentType = "application/msword"; break;
                    case ".xls": contentType = "application/vnd.ms-excel"; break;
                    case ".pdf": contentType = "application/pdf"; break;
                    case ".jpg":
                    case ".jpeg":
                    case ".jpe": contentType = "image/jpeg"; break;
                    case ".png": contentType = "image/png"; break;
                    case ".zip": contentType = "application/zip"; break;
                    case ".rar": contentType = "application/x-rar-compressed"; break;
                    default: contentType = "application/octet-stream"; break;
                }
                try
                {
                    Stream file = new FileStream(filePath, FileMode.Open);
                    Response.ContentType = contentType;
                    return File(file, contentType, archivo.Nombre);
                }
                catch (FileNotFoundException e){
                    Response.StatusCode = 404;
                    throw e;
                }
            }
            Response.StatusDescription = "Archivo no encontrado";
            Response.StatusCode = 404;
            return null;
        }

    }
}
