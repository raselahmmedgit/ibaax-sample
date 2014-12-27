using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RnD.iBaaxMvc.Startup))]
namespace RnD.iBaaxMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
