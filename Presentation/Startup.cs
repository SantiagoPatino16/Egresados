using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(Presentation.Startup))]
namespace Presentation
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Mapea SignalR y genera /signalr/hubs
            app.MapSignalR();
        }
    }
}