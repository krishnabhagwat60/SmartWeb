
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWeb.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinChat(string user)
        {
            await Clients.All.SendAsync("JoinChat", user);
        }

        public async Task LeaveChat(string user)
        {
            await Clients.All.SendAsync("LeaveChat", user);
        }

        public async Task SendAll(int id, string message, DateTime date, bool messageteacher)
        {
            await Clients.All.SendAsync("RecieveAll", id, message, date, messageteacher);
        }

        public async Task StageSendAll(int id, string username, string userimage, string message, string messagetype, string chaturl, DateTime date, bool messageteacher)
        {
            await Clients.All.SendAsync("RecieveAll", id, username, userimage, message, messagetype, chaturl, date, messageteacher);
        }
    }
}
