using Microsoft.Owin;
using Owin;
using System.Web.Security;

[assembly: OwinStartupAttribute(typeof(CrystalVortex.Startup))]
namespace CrystalVortex
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
