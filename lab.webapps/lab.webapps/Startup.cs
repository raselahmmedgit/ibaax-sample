using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lab.webapps.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)] //for log4net
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
