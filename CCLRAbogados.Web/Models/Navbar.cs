using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCLRAbogados.Web.Models
{
    public class Navbar
    {
        public string red { get; set; }
        public string orange { get; set; }
        public string yellow { get; set; }
        public string green { get; set; }
        public string lightbrown { get; set; }
        public string brown { get; set; }
        public string redActive { get; set; }
        public string orangeActive { get; set; }
        public string yellowActive { get; set; }
        public string greenActive { get; set; }
        public string lightbrownActive { get; set; }
        public string brownActive { get; set; }

        public Navbar() {
            this.red = "";
            this.orange = "";
            this.yellow = "";
            this.green = "";
            this.lightbrown = "";
            this.brown = "";
            this.redActive = "";
            this.orangeActive = "";
            this.yellowActive = "";
            this.greenActive = "";
            this.lightbrownActive = "";
            this.brownActive = "";
        }

        public void clearAll() {
            this.red = "";
            this.orange = "";
            this.yellow = "";
            this.green = "";
            this.lightbrown = "";
            this.brown = "";
            this.redActive = "";
            this.orangeActive = "";
            this.yellowActive = "";
            this.greenActive = "";
            this.lightbrownActive = "";
            this.brownActive = "";
        }

        public void activeAll(){
            this.red = "red";
            this.orange = "orange";
            this.yellow = "yellow";
            this.green = "green";
            this.lightbrown = "lightbrown";
            this.brown = "brown";
        }
    }

}