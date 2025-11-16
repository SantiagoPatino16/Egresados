using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Presentation.Startup))]
namespace Presentation
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
