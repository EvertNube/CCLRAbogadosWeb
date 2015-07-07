using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogados.Core.DTO
{
    public class CorreoDTO
    {
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public bool Copia { get; set; }
    }
}
