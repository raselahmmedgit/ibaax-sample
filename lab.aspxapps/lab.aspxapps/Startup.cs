using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lab.aspxapps.Startup))]
namespace lab.aspxapps
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
