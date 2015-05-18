using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCLRAbogados.Data;
using CCLRAbogados.Helpers;
using System.Data.Linq;

namespace CCLRAbogados.Core
{
    public class Base
    {
        protected CCLRAbogadosEntities getContext(){
            //return new UARMWebDBEntities(ConnectionStringProvider.getEntityBuilder().ToString());
            return new CCLRAbogadosEntities();
        }
    }
}
