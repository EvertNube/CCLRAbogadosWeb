using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogados.Core
{
    [Serializable]
    public class PaginaDTO
    {
        public DateTime? FechaActualizacion { get; set; }
        public int? IdUsuarioActualizacion { get; set; }
        public int IdPagina { get; set; }
        public string Nombre { get; set; } 
        public string Titulo { get; set; } 
        public bool? Estado { get; set; }
        public bool EstadoNotNull { get; set; }
        public int? Padre { get; set; }
        public string Uri { get; set; }
        public bool? Landing { get; set; }
        public bool LandingNotNull { get; set; }
        public int? Orden { get; set; }
        public string Contenido { get; set; }
        public string Path { get; set; }
        public string PathNames { get; set; }
        public bool EsEnlaceExterno { get; set; }
        public string EnlaceExterno { get; set; }
        public bool OnSideBar { get; set; }
        public bool MostrarHighlights { get; set; }
        public bool OnNavigation { get; set; }
        public int? IdCategoriaPagina { get; set; }
        public bool EnlaceExternoTarget { get; set; }
        public string Cover { get; set; }
        public bool MostrarCover { get; set; }
    }
}
