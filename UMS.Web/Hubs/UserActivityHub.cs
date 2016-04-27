using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using UMS.Database.DAL;
using System.Data.Entity;
using UMS.Web.Hubs.HubModels;
using AutoMapper;

namespace UMS.Web.Hubs
{
    [HubName("userActivityHub")]
    public class UserActivityHub : Hub
    {
        public static List<OnlineUser> Users = new List<OnlineUser>();
        public readonly IUmsDbContext _context;

        public UserActivityHub()
        {
            try
            {
                _context = (IUmsDbContext)Autofac.Integration.Mvc.AutofacDependencyResolver.Current.GetService(typeof(IUmsDbContext));
            }
            catch(InvalidOperationException ex)//User closed tab. We catch this exception so OnDisconnected method can run
            {
            }
        }

        public void Send(List<OnlineUser> users)
        {
            // Call the addNewMessageToPage method to update clients.
            var context = GlobalHost.ConnectionManager.GetHubContext<UserActivityHub>();
            context.Clients.All.updateUsersOnlineCount(users.Select(x => new { x.Email, x.FirstName, x.LastName, x.UserStatus }).ToArray());
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            string clientId = GetClientId();
            var email = Context.User.Identity.Name;
            if (Users.Where(x => x.ClientId == clientId || x.Email == email).FirstOrDefault() == null)
            {
                var user = _context.Users.Where(x => x.Email == email).AsNoTracking().FirstOrDefault();
                if (user != null)
                {
                    var onlineUserModel = Mapper.Map<OnlineUser>(user);
                    onlineUserModel.ClientId = clientId;
                    Users.Add(onlineUserModel);
                }
            }

            // Send the current count of users
            Send(Users);

            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            string clientId = GetClientId();
            var email = Context.User.Identity.Name;
            if (Users.Where(x => x.ClientId == clientId || x.Email == email).FirstOrDefault() == null)
            {
                var user = _context.Users.Where(x => x.Email == email).AsNoTracking().FirstOrDefault();
                if (user != null)
                {
                    var onlineUserModel = Mapper.Map<OnlineUser>(user);
                    onlineUserModel.ClientId = clientId;
                    Users.Add(onlineUserModel);
                }
            }

            // Send the current count of users
            Send(Users);

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
            Send(Users);

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