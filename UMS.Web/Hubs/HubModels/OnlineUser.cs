using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UMS.Web.Hubs.HubModels
{
    public class OnlineUser
    {
        public string ClientId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public OnlineUserStatus UserStatus { get; set; }

        public OnlineUser() { }
        public OnlineUser(string clientId, string email)
        {
            ClientId = clientId;
            Email = email;
        }
    }
}