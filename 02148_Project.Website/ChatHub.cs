using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace _02148_Project.Website
{
    public class ChatHub : Hub
    {
        private static string conString = @"Data Source=ALEX-PC;Initial Catalog=UseThis;User ID=fuk;Password=fuk;Max Pool Size=1000";
        [HubMethodName("sendMessages")]
        public static void SendMessages()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.updateMessages();
        }
    }

}