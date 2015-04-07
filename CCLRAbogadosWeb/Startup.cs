using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CCLRAbogadosWeb.Startup))]
namespace CCLRAbogadosWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
