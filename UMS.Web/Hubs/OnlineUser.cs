using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UMS.Web.Hubs
{
    public class OnlineUser
    {
        public string ClientId { get; set; }
        public string Email { get; set; }

        public OnlineUser() { }
        public OnlineUser(string clientId, string email)
        {
            ClientId = clientId;
            Email = email;
        }
    }
}