﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JXAPI.JXSdk.Response
{
    public class NotifyConfigGetResponse : Venus.VenusClientResponse
    {
        [XmlElement("notifyConfig")]
        public NotifyConfig notifyConfig { get; set; }

        [XmlElement("notifyAccounts")]
        public List<NotifyAccount> notifyAccounts { get; set; }

        [XmlElement("code")]
        public int code { get; set; }
    }

    public class NotifyConfig 
    {
        public int notifySwitch { get; set; }
        public int status { get; set; }
    }
    public class NotifyAccount
    {
        public int cid { get; set; }
        public int platform { get; set; }
    }
}
