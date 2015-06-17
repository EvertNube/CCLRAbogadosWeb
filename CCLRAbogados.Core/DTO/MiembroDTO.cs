using CCLRAbogados.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogados.Core.DTO
{
    public class MiembroDTO
    {
        public int IdMiembro { get; set; }
        public string Nombre { get; set; }
        public int IdCargo { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public bool Estado { get; set; }
        public string Uri { get; set; }
        public string ShortUrl { get; set; }
        public string NombreCargo { get; set; }
        public string UrlFacebook { get; set; }
        public string UrlTwitter { get; set; }
        public string UrlLinkedIn { get; set; }
        public string UrlSkype { get; set; }
        public IList<ExperienciaDTO> listaExperiencia { get; set; }
    }
}
