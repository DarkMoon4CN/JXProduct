using JXAPI.JXSdk.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JXAPI.JXSdk.Response
{
    public class RegisterResponse : Venus.VenusClientResponse
    {
        [XmlElement("data")]
        public UserInfo data { get; set; }

        [XmlElement("code")]
        public int code { get; set; }
    }
}
