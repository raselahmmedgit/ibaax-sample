using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lab.emailimport.Startup))]
namespace lab.emailimport
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
