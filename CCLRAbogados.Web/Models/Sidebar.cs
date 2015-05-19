using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCLRAbogados.Web.Models
{
    public class Sidebar
    {
        public string controller { get; set; }
        public string action { get; set; }
        public Dictionary<string, string> links { get; set; }
        public Sidebar() {
            this.controller = "";
            this.action = "";
            this.links = null;
        }
    }
}