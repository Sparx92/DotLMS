using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DotLms.Web.Startup))]
namespace DotLms.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
