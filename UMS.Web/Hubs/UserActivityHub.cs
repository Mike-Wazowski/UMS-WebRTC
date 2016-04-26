using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace UMS.Web.Hubs
{
    [HubName("userActivityHub")]
    public class UserActivityHub : Hub
    {
        public static List<OnlineUser> Users = new List<OnlineUser>();

        public void Send(string users)
        {
            // Call the addNewMessageToPage method to update clients.
            var context = GlobalHost.ConnectionManager.GetHubContext<UserActivityHub>();
            context.Clients.All.updateUsersOnlineCount(users);
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            string clientId = GetClientId();

            if(Users.Where(x => x.ClientId == clientId).FirstOrDefault() == null)
            {
                Users.Add(new OnlineUser(clientId, Context.User.Identity.Name));
            }

            // Send the current count of users
            Send(string.Join(";\n\r", Users.Select(x => x.Email).ToArray()));

            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            string clientId = GetClientId();
            if (Users.Where(x => x.ClientId == clientId).FirstOrDefault() == null)
            {
                Users.Add(new OnlineUser(clientId, Context.User.Identity.Name));
            }

            // Send the current count of users
            Send(string.Join(";\n\r", Users.Select(x => x.Email).ToArray()));

            return base.OnReconnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            string clientId = GetClientId();
            var user = Users.Where(x => x.ClientId == clientId).FirstOrDefault();
            if (user != null)
            {
                Users.Remove(user);
            }

            // Send the current count of users
            Send(string.Join(";\n\r", Users.Select(x => x.Email).ToArray()));

            return base.OnDisconnected(stopCalled);
        }

        private string GetClientId()
        {
            string clientId = "";
            if (Context.QueryString["clientId"] != null)
            {
                // clientId passed from application 
                clientId = this.Context.QueryString["clientId"];
            }

            if (string.IsNullOrEmpty(clientId.Trim()))
            {
                clientId = Context.ConnectionId;
            }

            return clientId;
        }
    }
}