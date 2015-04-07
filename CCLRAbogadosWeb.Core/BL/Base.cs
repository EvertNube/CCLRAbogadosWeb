using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCLRAbogadosWeb.Data;
using CCLRAbogadosWeb.Helpers;
using System.Data.Linq;

namespace CCLRAbogadosWeb.Core
{
    public class Base
    {
        protected CCLRAbogadosWebEntities getContext()
        {
            //return new UARMWebDBEntities(ConnectionStringProvider.getEntityBuilder().ToString());
            return new CCLRAbogadosWebEntities();
        }
    }
}
