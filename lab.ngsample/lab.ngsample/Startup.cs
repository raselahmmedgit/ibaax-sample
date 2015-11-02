using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lab.ngsample.Startup))]
namespace lab.ngsample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
