using CCLRAbogadosWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogadosWeb.Core.DTO
{
    public class TipoRevistaDTO
    {
        public int IdTipoRevista { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Uri { get; set; }
        public bool? Estado { get; set; }

        //public TipoRevistaDTO(TipoRevista tipoRevista) {
        //    IdTipoRevista = tipoRevista.IdTipoRevista;
        //    Nombre = tipoRevista.Nombre;
        //    Descripcion = tipoRevista.Descripcion;
        //    Uri = tipoRevista.Uri;
        //    Estado = tipoRevista.Estado;
        //}
    }
}
