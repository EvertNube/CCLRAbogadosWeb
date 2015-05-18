using CCLRAbogados.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogados.Core.DTO
{
    public class RevistaDTO
    {
        public int IdRevista { get; set; }
        public string Titulo { get; set; }
        public string Edicion { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string Pdf { get; set; }
        public string Uri { get; set; }
        public int IdTipoRevista { get; set; }
        public bool? Estado { get; set; }
    }
}
