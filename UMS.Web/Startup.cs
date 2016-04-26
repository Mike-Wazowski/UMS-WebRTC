using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(UMS.Web.Startup))]

namespace UMS.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            RouteConfig.RegisterRoutes(RouteConfig.Routes);
        }
    }
}
