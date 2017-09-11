using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading;

namespace signalR_services
{
    public class chatservice : PersistentConnection
    {
        #region Campos

        // To save clients count in order to raise OnConnected event.
        private static int clientsCount = 0;

        #endregion

        #region Métodos


        protected override Task OnConnected(IRequest request, string connectionId)
        {
            string[] groupsNames = { "netDevelopers", "javaDevelopers"};
            Groups.Add(connectionId, groupsNames[new Random().Next(groupsNames.Length)]);
            Interlocked.Increment(ref clientsCount);
            return base.OnConnected(request, connectionId);
        }
        
        protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        {
            Interlocked.Decrement(ref clientsCount);
            return base.OnDisconnected(request, connectionId, stopCalled);
        }

        protected override Task OnReconnected(IRequest request, string connectionId)
        {
            return base.OnReconnected(request, connectionId);
        }

        protected override IList<string> OnRejoiningGroups(IRequest request, 
            IList<string> groups, string connectionId)
        {
            return base.OnRejoiningGroups(request, groups, connectionId);
        }

        protected async override Task OnReceived(IRequest request, string connectionId, string data)
        {
            await Connection.Broadcast($"{DateTime.Now.ToShortTimeString()},{connectionId}: {data}",
                connectionId);
        }

        #endregion

        #region  Propiedades



        #endregion

        #region Eventos



        #endregion
    }
}