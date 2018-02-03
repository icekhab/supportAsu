using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SupportAsu.WebApi.Startup))]

namespace SupportAsu.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //ConfigureMobileApp(app);
        }
    }
}