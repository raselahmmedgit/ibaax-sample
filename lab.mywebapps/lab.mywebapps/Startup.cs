using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lab.mywebapps.Startup))]
namespace lab.mywebapps
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
