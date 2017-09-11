using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(signalR_services.Configurations.StartupSignalService))]

namespace signalR_services.Configurations
{
    public class StartupSignalService
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/Signals/ChatService",
                appBuilder =>
                {
                    // Pending enable CORS to accept cross origin requests
                    // please download Microsoft.Owin.Cors nuget package.
                    appBuilder.UseCors(CorsOptions.AllowAll);
                    appBuilder.RunSignalR<signalR_services.chatservice>(
                        new Microsoft.AspNet.SignalR.ConnectionConfiguration { EnableJSONP = true });
                }
            );
            //app.MapSignalR<signalR_services.chatservice>("/Signals/ChatService");
        }
    }
}
