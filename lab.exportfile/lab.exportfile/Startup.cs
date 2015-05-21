using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lab.exportfile.Startup))]
namespace lab.exportfile
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
