using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogados.Core.DTO
{
    [Serializable]
    public class ExperienciaDTO
    {
        public int IdExperiencia { get; set; }
        public int IdTipoExperiencia { get; set; }
        public int IdMiembro { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public int Orden { get; set; }
        public bool Active { get; set; }
    }
}
