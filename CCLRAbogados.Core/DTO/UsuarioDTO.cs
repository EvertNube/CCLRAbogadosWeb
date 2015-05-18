﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogados.Core.DTO
{
    [Serializable]
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Cuenta { get; set; }
        public string Pass { get; set; }
        public bool Estado { get; set; }
        public int IdRol { get; set; }
    }
}
