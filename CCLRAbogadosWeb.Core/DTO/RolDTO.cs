using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CCLRAbogadosWeb.Helpers;
using System.Web.Mvc;

namespace CCLRAbogadosWeb.Core.DTO
{
    [Serializable]
    public class RolDTO
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public string DescripcionRol { get; set; }
    }
}
