using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(salescampaignschedule.web.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace salescampaignschedule.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
