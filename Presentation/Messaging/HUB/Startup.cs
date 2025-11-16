using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Presentation.Messaging.HUB.Startup))]
namespace Presentation.Messaging.HUB
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
