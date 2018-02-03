using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

[assembly: OwinStartup(typeof(SupportAsu.Web.Startup))]
namespace SupportAsu.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login"),
                //ExpireTimeSpan = TimeSpan.FromSeconds(10)
            });
        }
    }
}