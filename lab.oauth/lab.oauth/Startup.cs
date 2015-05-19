using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lab.oauth.Startup))]
namespace lab.oauth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
