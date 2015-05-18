using CCLRAbogados.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogados.Core.DTO
{
    public class TipoRevistaDTO
    {
        public int IdTipoRevista { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Uri { get; set; }
        public bool? Estado { get; set; }
    }
}
