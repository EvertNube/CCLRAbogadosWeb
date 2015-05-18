using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogados.Core.DTO
{
    [Serializable]
    public class EnlaceDTO
    {
        public int IdEnlace { get; set; }
        public string Titulo { get; set; }
        public string Url { get; set; }
        public int? Orden { get; set; }
        public int? Padre { get; set; }
        public IList<EnlaceDTO> Hijos { get; set; }
        public bool? Landing { get; set; }
        public bool EsEnlaceExterno { get; set; }
        public bool OnSideBar { get; set; }
        public bool Target { get; set; }
        public string Path { get; set; }
        public bool Estado { get; set; }
    }
}
