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

        public void SendUserList(List<OnlineUser> users)
        {
            // Call the addNewMessageToPage method to update clients.
            var context = GlobalHost.ConnectionManager.GetHubContext<UserActivityHub>();
            context.Clients.All.updateUsersOnline(users.Select(x => new { x.Email, x.FirstName, x.LastName, x.UserStatus }).ToArray());
        }

        [HubMethodName("callUser")]
        public void CallUser(string targetEmail, RTCSessionDescription offer)
        {
            var targetUser = Users.Where(x => x.Email == targetEmail).FirstOrDefault();
            var sourceUser = Users.Where(x => x.Email == Context.User.Identity.Name).FirstOrDefault();
            if(targetUser == null || sourceUser == null)
            {
#warning display error to user
                return;
            }
            else
            {
                //targetUser.UserStatus = OnlineUserStatus.Busy;
                //sourceUser.UserStatus = OnlineUserStatus.Busy;
                SendUserList(Users);
                SendOffer(targetUser, sourceUser, offer);
            }
        }

        [HubMethodName("answerUser")]
        public void AnswerUser(string callingEmail, RTCSessionDescription answer)
        {
            var answeringUser = Users.Where(x => x.Email == Context.User.Identity.Name).FirstOrDefault();
            var callingUser = Users.Where(x => x.Email == callingEmail).FirstOrDefault();
            if (answeringUser == null || callingUser == null)
            {
#warning display error to user
                return;
            }
            else
            {
                SendAnswer(answeringUser, callingUser, answer);
            }
        }

        [HubMethodName("sendCandidate")]
        public void SendCandidate(string receiverEmail, RTCIceCandidate candidate)
        {
            var receiverUser = Users.Where(x => x.Email == receiverEmail).FirstOrDefault();
            if(receiverUser != null)
            {
                var context = GlobalHost.ConnectionManager.GetHubContext<UserActivityHub>();
                context.Clients.Client(receiverUser.ClientId).incommingCandidate(candidate);
            }
        }

        private void SendAnswer(OnlineUser answeringUser, OnlineUser callingUser, RTCSessionDescription answer)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<UserActivityHub>();
            context.Clients.Client(callingUser.ClientId).incommingAnswer(answeringUser, answer);
        }

        private void SendOffer(OnlineUser targetUser, OnlineUser sourceUser, RTCSessionDescription offer)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<UserActivityHub>();
            context.Clients.Client(targetUser.ClientId).incommingCall(sourceUser, offer);
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
            SendUserList(Users);

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
            SendUserList(Users);

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
            SendUserList(Users);

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