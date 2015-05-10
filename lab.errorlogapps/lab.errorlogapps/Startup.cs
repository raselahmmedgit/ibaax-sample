using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lab.errorlogapps.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace lab.errorlogapps
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
