using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RnD.iBaaxWebApi.Startup))]


namespace RnD.iBaaxWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}