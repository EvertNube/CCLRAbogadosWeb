using CCLRAbogados.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCLRAbogados.Data;
using CCLRAbogados.Helpers;

namespace CCLRAbogados.Core.BL
{
    public class MiembrosBL : Base
    {
        public  IList<MiembroDTO> getMiembros()
        {
            using(var context = getContext())
            {
                try
                {
                    //var result = context.Miembro.Where(x => x.Estado).Select(x => new MiembroDTO
                    var result = context.Miembro.AsEnumerable().Select(x => new MiembroDTO
                    {
                        IdMiembro = x.IdMiembro,
                        Nombre = x.Nombre,
                        IdCargo = x.IdCargo,
                        Telefono = x.Telefono,
                        Celular = x.Celular,
                        Email = x.Email,
                        Titulo = x.Titulo,
                        Descripcion = x.Descripcion,
                        Imagen = x.Imagen,
                        Estado = x.Estado,
                        Uri = x.Uri,
                        ShortUrl = x.ShortUrl,
                        NombreCargo = x.Cargo.Nombre
                    }).OrderBy(x => x.IdMiembro).ToList();
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public IList<MiembroDTO> getMiembrosActivos()
        {
            using(var context = getContext())
            {
                try
                {
                    var result = context.Miembro.Where(x => x.Estado).Select(x => new MiembroDTO
                    {
                        IdMiembro = x.IdMiembro,
                        Nombre = x.Nombre,
                        IdCargo = x.IdCargo,
                        Telefono = x.Telefono,
                        Celular = x.Celular,
                        Email = x.Email,
                        Titulo = x.Titulo,
                        Descripcion = x.Descripcion,
                        Imagen = x.Imagen,
                        Estado = x.Estado,
                        Uri = x.Uri,
                        ShortUrl = x.ShortUrl,
                        NombreCargo = x.Cargo.Nombre
                    }).OrderBy(x => x.IdMiembro).ToList();
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public MiembroDTO getMiembro(int id)
        {
            using (var context = getContext())
            {
                var result = context.Miembro.Where(x => x.IdMiembro == id).Select(r => new MiembroDTO
                {
                    IdMiembro = r.IdMiembro,
                    Nombre = r.Nombre,
                    IdCargo = r.IdCargo,
                    Telefono = r.Telefono,
                    Celular = r.Celular,
                    Email = r.Email,
                    Titulo = r.Titulo,
                    Descripcion = r.Descripcion,
                    Imagen = r.Imagen,
                    Estado = r.Estado,
                    Uri = r.Uri,
                    ShortUrl = r.ShortUrl,
                    NombreCargo = r.Cargo.Nombre
                }).SingleOrDefault();
                return result;
            }
        }

        public bool add(MiembroDTO Miembro, string baseUrl = "")
        {
            using (var context = getContext())
            {
                try
                {
                    Miembro nuevo = new Miembro();
                    nuevo.IdMiembro = Miembro.IdMiembro;
                    //------ShortURL--------
                    var urlToEncode = baseUrl + "/" + Miembro.Uri;
                    nuevo.ShortUrl = ShortUrl.Shorten(urlToEncode);
                    //------End ShortURL----
                    nuevo.Nombre = Miembro.Nombre;
                    nuevo.IdCargo = Miembro.IdCargo;
                    nuevo.Telefono = Miembro.Telefono;
                    nuevo.Celular = Miembro.Telefono;
                    nuevo.Email = Miembro.Email;
                    nuevo.Titulo = Miembro.Titulo;
                    nuevo.Descripcion = Miembro.Descripcion;
                    nuevo.Imagen = Miembro.Imagen;
                    nuevo.Estado = true;
                    nuevo.Uri = Miembro.Uri;
                    //nuevo.ShortUrl = Miembro.ShortUrl;
                    context.Miembro.Add(nuevo);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public bool update(MiembroDTO Miembro, string baseUrl = "")
        {
            using (var context = getContext())
            {
                try
                {
                    var dataRow = context.Miembro.Where(x => x.IdMiembro == Miembro.IdMiembro).SingleOrDefault();
                    dataRow.IdMiembro = Miembro.IdMiembro;
                    //------ShortURL--------
                    var urlToEncode = baseUrl + "/" + Miembro.Uri;
                    dataRow.ShortUrl = ShortUrl.Shorten(urlToEncode);
                    //------End ShortURL----
                    dataRow.Nombre = Miembro.Nombre;
                    dataRow.IdCargo = Miembro.IdCargo;
                    dataRow.Telefono = Miembro.Telefono;
                    dataRow.Celular = Miembro.Telefono;
                    dataRow.Email = Miembro.Email;
                    dataRow.Titulo = Miembro.Titulo;
                    dataRow.Descripcion = Miembro.Descripcion;
                    dataRow.Imagen = Miembro.Imagen;
                    dataRow.Estado = Miembro.Estado;
                    dataRow.Uri = Miembro.Uri;
                    dataRow.ShortUrl = Miembro.ShortUrl;
                    context.SaveChanges();
                    return true;
                }
                    catch (Exception e)
                {

                        throw e;
                    }
            }
        }
        public IList<CargoDTO> getCargosViewBag(bool AsSelectList = false)
        {
            CargoBL objBL = new CargoBL();
            if (!AsSelectList)
            {
                return objBL.getCargos();
            }
            else
            {
                var lista = objBL.getCargos();
                lista.Insert(0, new CargoDTO() { IdCargo = 0, Nombre = "Seleccione el Tipo de Cargo." });
                return lista;
            }
        }

        public MiembroDTO getMiembroPorUri(string uri)
        {
            using(var context = getContext())
            {
                var result = context.Miembro.Where(x => x.Estado == true && x.Uri == uri).Select(r => new MiembroDTO
                    {
                        IdMiembro = r.IdMiembro,
                        Nombre = r.Nombre,
                        IdCargo = r.IdCargo,
                        Telefono = r.Telefono,
                        Celular = r.Celular,
                        Email = r.Email,
                        Titulo = r.Titulo,
                        Descripcion = r.Descripcion,
                        Imagen = r.Imagen,
                        Estado = r.Estado,
                        Uri = r.Uri,
                        ShortUrl = r.ShortUrl,
                        NombreCargo = r.Cargo.Nombre
                    }).SingleOrDefault();
                return result;
            }
        }
    }
}
