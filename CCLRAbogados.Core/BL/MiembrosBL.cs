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
        public IList<MiembroDTO> getMiembros()
        {
            using (var context = getContext())
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
                        UrlFacebook = x.UrlFacebook,
                        UrlTwitter = x.UrlTwitter,
                        UrlLinkedIn = x.UrlLinkedIn,
                        UrlSkype = x.UrlSkype,
                        ImagenPerfil = x.ImagenPerfil,
                        VCard = x.VCard,
                        NombreCargo = x.Cargo.Nombre,
                        Orden = x.Orden
                    }).OrderBy(x => x.Orden).ToList();
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
            using (var context = getContext())
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
                        UrlFacebook = x.UrlFacebook,
                        UrlTwitter = x.UrlTwitter,
                        UrlLinkedIn = x.UrlLinkedIn,
                        UrlSkype = x.UrlSkype,
                        ImagenPerfil = x.ImagenPerfil,
                        VCard = x.VCard,
                        NombreCargo = x.Cargo.Nombre,
                        Orden = x.Orden
                    }).OrderBy(x => x.Orden).ToList();
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
                    UrlFacebook = r.UrlFacebook,
                    UrlTwitter = r.UrlTwitter,
                    UrlLinkedIn = r.UrlLinkedIn,
                    UrlSkype = r.UrlSkype,
                    ImagenPerfil = r.ImagenPerfil,
                    VCard = r.VCard,
                    NombreCargo = r.Cargo.Nombre,
                    Orden = r.Orden,
                    listaExperiencia = r.Experiencia.Select(x => new ExperienciaDTO
                    {
                        IdExperiencia = x.IdExperiencia,
                        IdTipoExperiencia = x.IdTipoExperiencia,
                        IdMiembro = x.IdMiembro,
                        Titulo = x.Titulo,
                        Texto = x.Texto,
                        Orden = x.Orden,
                        Active = x.Active
                    }).ToList()
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
                    //Numero de miembros
                    int ultimoM = 0; ultimoM = context.Miembro.Count();

                    Miembro nuevo = new Miembro();
                    nuevo.IdMiembro = Miembro.IdMiembro;
                    //------ShortURL--------
                    var urlToEncode = baseUrl + "/" + Miembro.Uri;
                    nuevo.ShortUrl = ShortUrl.Shorten(urlToEncode);
                    //------End ShortURL----
                    nuevo.Nombre = Miembro.Nombre;
                    nuevo.IdCargo = Miembro.IdCargo;
                    nuevo.Telefono = Miembro.Telefono;
                    nuevo.Celular = Miembro.Celular;
                    nuevo.Email = Miembro.Email;
                    nuevo.Titulo = Miembro.Titulo;
                    nuevo.Descripcion = Miembro.Descripcion;
                    nuevo.Imagen = Miembro.Imagen;
                    nuevo.Estado = true;
                    nuevo.Uri = Miembro.Uri;
                    nuevo.ImagenPerfil = Miembro.ImagenPerfil;
                    nuevo.VCard = Miembro.VCard;
                    //Redes Sociales
                    nuevo.UrlFacebook = Miembro.UrlFacebook;
                    nuevo.UrlTwitter = Miembro.UrlTwitter;
                    nuevo.UrlLinkedIn = Miembro.UrlLinkedIn;
                    nuevo.UrlSkype = Miembro.UrlSkype;
                    //Orden
                    nuevo.Orden = ultimoM + 1;
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
                    dataRow.Celular = Miembro.Celular;
                    dataRow.Email = Miembro.Email;
                    dataRow.Titulo = Miembro.Titulo;
                    dataRow.Descripcion = Miembro.Descripcion;
                    dataRow.Imagen = Miembro.Imagen;
                    dataRow.Estado = Miembro.Estado;
                    dataRow.Uri = Miembro.Uri;
                    dataRow.ImagenPerfil = Miembro.ImagenPerfil;
                    dataRow.VCard = Miembro.VCard;
                    //Redes Sociales
                    dataRow.UrlFacebook = Miembro.UrlFacebook;
                    dataRow.UrlTwitter = Miembro.UrlTwitter;
                    dataRow.UrlLinkedIn = Miembro.UrlLinkedIn;
                    dataRow.UrlSkype = Miembro.UrlSkype;
                    //Orden
                    dataRow.Orden = Miembro.Orden;
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
        public bool OrdenBajar(MiembroDTO Miembro)
        {
            using (var context = getContext())
            {
                try
                {
                    var actual = context.Miembro.Where(x => x.IdMiembro == Miembro.IdMiembro).SingleOrDefault();

                    var anterior = context.Miembro.Where(x => x.Orden == Miembro.Orden - 1).SingleOrDefault();

                    if (anterior == null)
                        return false;

                    if (anterior.Orden == 0)
                    { return false; }
                    else
                    {
                        var aux = actual.Orden;

                        actual.Orden = anterior.Orden;
                        anterior.Orden = aux;
                        context.SaveChanges();
                    }
                    return true;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public bool OrdenSubir(MiembroDTO Miembro)
        {
            using (var context = getContext())
            {
                try
                {
                    var actual = context.Miembro.Where(x => x.IdMiembro == Miembro.IdMiembro).SingleOrDefault();

                    var adelante = context.Miembro.Where(x => x.Orden == Miembro.Orden + 1).SingleOrDefault();

                    if (adelante == null)
                        return false;

                    if (adelante.Orden == 0)
                    { return false; }
                    else
                    {
                        var aux = actual.Orden;

                        actual.Orden = adelante.Orden;
                        adelante.Orden = aux;
                        context.SaveChanges();
                    }
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
                return objBL.getCargosActivos();
            }
            else
            {
                var lista = objBL.getCargosActivos();
                lista.Insert(0, new CargoDTO() { IdCargo = 0, Nombre = "Seleccione el Tipo de Cargo." });
                return lista;
            }
        }

        public MiembroDTO getMiembroPorUri(string uri)
        {
            using (var context = getContext())
            {
                var result = context.Miembro.AsEnumerable().Where(x => x.Estado == true && x.Uri == uri).Select(r => new MiembroDTO
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
                        UrlFacebook = r.UrlFacebook,
                        UrlTwitter = r.UrlTwitter,
                        UrlLinkedIn = r.UrlLinkedIn,
                        UrlSkype = r.UrlSkype,
                        ImagenPerfil = r.ImagenPerfil,
                        VCard = r.VCard,
                        NombreCargo = r.Cargo.Nombre,
                        listaExperiencia = r.Experiencia.Select(x => new ExperienciaDTO
                        {
                            IdExperiencia = x.IdExperiencia,
                            IdTipoExperiencia = x.IdTipoExperiencia,
                            IdMiembro = x.IdMiembro,
                            Titulo = x.Titulo,
                            Texto = x.Texto,
                            Orden = x.Orden,
                            Active = x.Active
                        }).OrderBy(x => x.IdTipoExperiencia).ToList()
                    }).SingleOrDefault();
                return result;
            }
        }

        //Experiencias de Miembros
        public IList<ExperienciaDTO> getExperiencias()
        {
            using (var context = getContext())
            {
                try
                {
                    var result = context.Experiencia.Select(x => new ExperienciaDTO
                    {
                        IdExperiencia = x.IdExperiencia,
                        IdTipoExperiencia = x.IdTipoExperiencia,
                        IdMiembro = x.IdMiembro,
                        Titulo = x.Titulo,
                        Texto = x.Texto,
                        Orden = x.Orden,
                        Active = x.Active
                    }).ToList();
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public ExperienciaDTO getExperiencia(int id)
        {
            using (var context = getContext())
            {
                try
                {
                    var result = context.Experiencia.Where(x => x.IdExperiencia == id).Select(x => new ExperienciaDTO
                        {
                            IdExperiencia = x.IdExperiencia,
                            IdTipoExperiencia = x.IdTipoExperiencia,
                            IdMiembro = x.IdMiembro,
                            Titulo = x.Titulo,
                            Texto = x.Texto,
                            Orden = x.Orden,
                            Active = x.Active,
                            NombreMiembro = x.Miembro.Nombre,
                            NombreTipoExperiencia = x.TipoExperiencia.Nombre
                        }).SingleOrDefault();
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public bool addExperiencia(ExperienciaDTO exper)
        {
            using (var context = getContext())
            {
                try
                {
                    Experiencia nuevo = new Experiencia();
                    nuevo.IdExperiencia = exper.IdMiembro;
                    nuevo.IdTipoExperiencia = exper.IdTipoExperiencia;
                    nuevo.IdMiembro = exper.IdMiembro;
                    nuevo.Titulo = exper.Titulo;
                    nuevo.Texto = exper.Texto;
                    nuevo.Orden = exper.Orden;
                    nuevo.Active = true;

                    context.Experiencia.Add(nuevo);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public bool updateExperiencia(ExperienciaDTO exper)
        {
            using (var context = getContext())
            {
                try
                {
                    var dataRow = context.Experiencia.Where(x => x.IdExperiencia == exper.IdExperiencia).SingleOrDefault();
                    dataRow.IdTipoExperiencia = exper.IdTipoExperiencia;
                    dataRow.IdMiembro = exper.IdMiembro;
                    dataRow.Titulo = exper.Titulo;
                    dataRow.Texto = exper.Texto;
                    dataRow.Orden = exper.Orden;
                    dataRow.Active = exper.Active;
                    context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }

        public TipoExperienciaDTO getTipoExperiencia(int id)
        {
            using(var context = getContext())
            {
                try
                {
                    var result = context.TipoExperiencia.Where(x => x.IdTipoExperiencia == id).Select(x => new TipoExperienciaDTO
                        {
                            IdTipoExperiencia = x.IdTipoExperiencia,
                            Nombre = x.Nombre
                        }).SingleOrDefault();
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public IList<TipoExperienciaDTO> getTipoExperiencias()
        {
            using (var context = getContext())
            {
                try
                {
                    var result = context.TipoExperiencia.Select(x => new TipoExperienciaDTO
                    {
                        IdTipoExperiencia = x.IdTipoExperiencia,
                        Nombre = x.Nombre
                    }).ToList();
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public IList<ExperienciaDTO> getExperienciasPorMiembroYPorTipo(int idMiembro, int idTipoExperiencia)
        {
            using(var context = getContext())
            {
                try
                {
                    var result = context.Experiencia.Where(x => x.IdMiembro == idMiembro && x.IdTipoExperiencia == idTipoExperiencia).Select(x => new ExperienciaDTO
                    {
                        IdExperiencia = x.IdExperiencia,
                        IdTipoExperiencia = x.IdTipoExperiencia,
                        IdMiembro = x.IdMiembro,
                        Titulo = x.Titulo,
                        Texto = x.Texto,
                        Orden = x.Orden,
                        Active = x.Active
                    }).ToList();
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public IList<TipoExperienciaDTO> getExperienciasPorMiembro(int id)
        {
            using(var context = getContext())
            {
                try
                {
                    var result = context.TipoExperiencia.Select(x => new TipoExperienciaDTO
                    {
                        IdTipoExperiencia = x.IdTipoExperiencia,
                        Nombre = x.Nombre,
                        listaExperiencia = x.Experiencia.Where(r => r.IdMiembro == id).Select(r => new ExperienciaDTO
                        {
                            IdExperiencia = r.IdExperiencia,
                            IdTipoExperiencia = r.IdTipoExperiencia,
                            IdMiembro = r.IdMiembro,
                            Titulo = r.Titulo,
                            Texto = r.Texto,
                            Orden = r.Orden,
                            Active = r.Active
                        }).ToList()
                    }).ToList();
                    return result;
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
        }

        public IList<TipoExperienciaDTO> getExperienciasActivasPorMiembro(int id)
        {
            using (var context = getContext())
            {
                try
                {
                    var result = context.TipoExperiencia.Select(x => new TipoExperienciaDTO
                    {
                        IdTipoExperiencia = x.IdTipoExperiencia,
                        Nombre = x.Nombre,
                        listaExperiencia = x.Experiencia.Where(r => r.IdMiembro == id && r.Active == true).Select(r => new ExperienciaDTO
                        {
                            IdExperiencia = r.IdExperiencia,
                            IdTipoExperiencia = r.IdTipoExperiencia,
                            IdMiembro = r.IdMiembro,
                            Titulo = r.Titulo,
                            Texto = r.Texto,
                            Orden = r.Orden,
                            Active = r.Active
                        }).ToList()
                    }).ToList();
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public IList<TipoExperienciaDTO> getTipoExperienciasViewBag(bool AsSelectList = false)
        {
            if (!AsSelectList)
            {
                return getTipoExperiencias();
            }
            else
            {
                var lista = getTipoExperiencias();
                lista.Insert(0, new TipoExperienciaDTO() { IdTipoExperiencia = 0, Nombre = "Seleccione el Tipo de Cargo." });
                return lista;
            }
        }
        //
    }
}
