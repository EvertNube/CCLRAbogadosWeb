using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogados.Core.DTO
{
    [Serializable]
    public class CargoDTO
    {
        public int IdCargo { get; set; }
        public string Nombre { get; set; }
        public bool Plural { get; set; }
        public bool Estado { get; set; }
    }
}
