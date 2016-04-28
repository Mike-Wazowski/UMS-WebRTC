using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UMS.Web.Hubs.HubModels
{
    public class RTCIceCandidate
    {
        public string candidate { get; set; }
        public int sdpMLineIndex { get; set; }
        public string sdpMid { get; set; }
    }
}