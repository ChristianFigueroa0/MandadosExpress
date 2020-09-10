using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MandadosExpress.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            var connectionId = Context.ConnectionId;
            await Clients.AllExcept(connectionId).SendAsync("ReceiveMessage", message);
        }

    }
}
