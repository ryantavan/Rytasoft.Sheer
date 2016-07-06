using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rytasoft.Sheer.Web.Startup))]
namespace Rytasoft.Sheer.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
