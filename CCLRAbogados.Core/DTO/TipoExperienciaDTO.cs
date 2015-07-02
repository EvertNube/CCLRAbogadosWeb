using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogados.Core.DTO
{
    [Serializable]
    public class TipoExperienciaDTO
    {
        public int IdTipoExperiencia { get; set; }
        public string Nombre { get; set; }
        public IList<ExperienciaDTO> listaExperiencia { get; set; }
    }
}
