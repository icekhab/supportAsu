using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SupportAsu.WebApi.Startup))]

namespace SupportAsu.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);  // cors for owin token pipeline
            ConfigureAuth(app);
            //ConfigureMobileApp(app);
        }
    }
}