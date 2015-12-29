using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lab.emailverify.Startup))]
namespace lab.emailverify
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
