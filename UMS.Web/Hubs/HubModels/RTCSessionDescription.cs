﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UMS.Web.Hubs.HubModels
{
    public class RTCSessionDescription
    {
        public string type { get; set; }
        public string sdp { get; set; }
    }
}