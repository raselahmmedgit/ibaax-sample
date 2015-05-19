using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lab.datetimeapps.Startup))]
namespace lab.datetimeapps
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
