using CCLRAbogados.Core.DTO;
using CCLRAbogados.Data;
using CCLRAbogados.Helpers;
using System;
using System.Collections.Generic;
//using System.Data.Objects.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogados.Core.BL
{
    public class UsuariosBL: Base
    {
        public IList<UsuarioDTO> getUsuarios() {
            using (var context = getContext())
            {
                var result = from r in context.Usuario
                             where r.Estado == true & r.IdRol != CONSTANTES.SUPER_ADMIN_ROL
                             select new UsuarioDTO{
                                IdUsuario = r.IdUsuario,
                                Nombre = r.Nombre,
                                Email = r.Email,
                                Cuenta = r.Cuenta,
                                Pass = r.Pass,
                                Estado = r.Estado,
                                IdRol = r.IdRol ?? 0
                             };
                return result.AsEnumerable<UsuarioDTO>().OrderByDescending(x => x.IdUsuario).ToList<UsuarioDTO>();
            }
        }
        public bool add(UsuarioDTO user)
        {
            using (var context = getContext())
            {
                try{
                    Usuario usuario = new Usuario();
                    usuario.Nombre = user.Nombre;
                    usuario.Email = user.Email;
                    usuario.Cuenta = user.Cuenta;
                    usuario.Pass = Encrypt.GetCrypt(user.Pass);
                    usuario.IdRol = user.IdRol >= 2 ? user.IdRol : 3;
                    usuario.Estado = true;
                    usuario.FechaRegistro = DateTime.Now;
                    context.Usuario.Add(usuario);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception e){
                    throw e;
                }
            }
        }
        public bool validateUsuario(UsuarioDTO user)
        {
            using (var context = getContext())
            {
                if (user.Cuenta != null && user.Email != null)
                {
                    var result = from r in context.Usuario
                                 where r.Cuenta == user.Cuenta || r.Email == user.Email
                                 select r;
                    if (result.FirstOrDefault<Usuario>() == null)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public IEnumerable<RolDTO> getRoles() {
            using (var context = getContext())
            {
                var result = from r in context.Rol
                             where r.IdRol != CONSTANTES.SUPER_ADMIN_ROL
                             select new RolDTO { 
                                IdRol = r.IdRol,
                                Nombre = r.Nombre
                             };
                return result.ToList<RolDTO>();
            }
        }

        public UsuarioDTO getUsuarioByCuenta(UsuarioDTO user)
        {
            using (var context = getContext())
            {
                var result = from r in context.Usuario
                             where r.Estado == true & r.Cuenta == user.Cuenta
                             select new UsuarioDTO { 
                                IdUsuario = r.IdUsuario,
                                Nombre = r.Nombre,
                                IdRol = r.IdRol ?? 0,
                                Estado = r.Estado,
                                Email = r.Email,
                                Pass = r.Pass,
                                Cuenta = r.Cuenta
                             };
                return result.SingleOrDefault<UsuarioDTO>();
            }
        }

        public bool isValidUser(UsuarioDTO user)
        {
            using (var context = getContext())
            {
                var result = from r in context.Usuario
                             where r.Estado == true & r.Cuenta == user.Cuenta
                             select r;
                Usuario usuario = result.SingleOrDefault<Usuario>();
                if (usuario != null){
                    if (Encrypt.comparetoCrypt(user.Pass, usuario.Pass))
                        return true;
                }
            }
            return false;
        }



        public UsuarioDTO getUsuario(int id)
        {
            using (var context = getContext())
            {
                var result = from r in context.Usuario
                             where r.IdUsuario == id
                             select new UsuarioDTO{ 
                                Cuenta = r.Cuenta,
                                Email = r.Email,
                                Estado = r.Estado,
                                IdRol = r.IdRol ?? 0,
                                IdUsuario = r.IdUsuario,
                                Nombre = r.Nombre,
                                Pass = r.Pass
                             };
                return result.SingleOrDefault();
            }
        }

        public bool update(UsuarioDTO user, string passUser, string passChange, UsuarioDTO currentUser)
        {
            using (var context = getContext())
            {
                try
                {
                    Usuario usuario = context.Usuario.Where(x => x.IdUsuario == user.IdUsuario).SingleOrDefault();
                    if (usuario != null)
                    {
                        usuario.Nombre = user.Nombre;
                        usuario.IdRol = user.IdRol >= 2 ? user.IdRol : 3;
                        usuario.Estado = user.Estado;
                        if (!String.IsNullOrWhiteSpace(passUser) && !String.IsNullOrWhiteSpace(passChange))
                        {
                            if ((currentUser.IdRol <= 2 || currentUser.IdUsuario == user.IdUsuario)
                                && Encrypt.comparetoCrypt(passUser, currentUser.Pass))
                            {
                                usuario.Pass = Encrypt.GetCrypt(passChange);
                            }
                            else return false;
                        }
                        context.SaveChanges();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    //throw e;
                    return false;
                }
                return false;
            }
        }
    }
}
