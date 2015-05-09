using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lab.webapps.Startup))]
namespace lab.webapps
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
