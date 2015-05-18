using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.EntityClient;
using System.Configuration;

namespace CCLRAbogados.Helpers
{
    public static class ConnectionStringProvider
    {
        public static EntityConnectionStringBuilder getEntityBuilder()
        {
            var originalConnectionString = ConfigurationManager.ConnectionStrings["UARMWebDBEntities"].ConnectionString;
            var entityBuilder = new EntityConnectionStringBuilder(originalConnectionString);
            var factory = DbProviderFactories.GetFactory(entityBuilder.Provider);
            var providerBuilder = factory.CreateConnectionStringBuilder();

            providerBuilder.ConnectionString = entityBuilder.ProviderConnectionString;
            providerBuilder.Add("Password", "sa");

            entityBuilder.ProviderConnectionString = providerBuilder.ToString();
            return entityBuilder;
        }
    }
}
