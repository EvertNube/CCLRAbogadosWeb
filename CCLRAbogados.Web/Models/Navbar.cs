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
        public string gold { get; set; }
        public string redActive { get; set; }
        public string orangeActive { get; set; }
        public string yellowActive { get; set; }
        public string greenActive { get; set; }
        public string lightbrownActive { get; set; }
        public string brownActive { get; set; }

        public string gold1Active { get; set; }
        public string gold2Active { get; set; }
        public string gold3Active { get; set; }
        public string gold4Active { get; set; }

        public string primaryActive { get; set; }
        public string successActive { get; set; }
        public string infoActive { get; set; }
        public string warningActive { get; set; }
        public string dangerActive { get; set; }


        public Navbar() {
            this.red = "";
            this.orange = "";
            this.yellow = "";
            this.green = "";
            this.lightbrown = "";
            this.brown = "";
            this.gold = "";
            this.redActive = "";
            this.orangeActive = "";
            this.yellowActive = "";
            this.greenActive = "";
            this.lightbrownActive = "";
            this.brownActive = "";

            this.gold1Active = "";
            this.gold2Active = "";
            this.gold3Active = "";
            this.gold4Active = "";

            this.primaryActive = "";
            this.successActive = "";
            this.infoActive = "";
            this.warningActive = "";
            this.dangerActive = "";
        }

        public void clearAll() {
            this.red = "";
            this.orange = "";
            this.yellow = "";
            this.green = "";
            this.lightbrown = "";
            this.brown = "";
            this.gold = "";
            this.redActive = "";
            this.orangeActive = "";
            this.yellowActive = "";
            this.greenActive = "";
            this.lightbrownActive = "";
            this.brownActive = "";

            this.gold1Active = "";
            this.gold2Active = "";
            this.gold3Active = "";
            this.gold4Active = "";

            this.primaryActive = "";
            this.successActive = "";
            this.infoActive = "";
            this.warningActive = "";
            this.dangerActive = "";
        }

        public void activeAll(){
            this.red = "red";
            this.orange = "orange";
            this.yellow = "yellow";
            this.green = "green";
            this.lightbrown = "lightbrown";
            this.brown = "brown";
            this.gold = "gold";

            this.gold1Active = "active";
            this.gold2Active = "active";
            this.gold3Active = "active";
            this.gold4Active = "active";

            this.primaryActive = "active";
            this.successActive = "active";
            this.infoActive = "active";
            this.warningActive = "active";
            this.dangerActive = "active";
        }
    }

}