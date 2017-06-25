using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JXAPI.JXSdk.Response
{
    public class UserExistResponse : Venus.VenusClientResponse
    {
        [XmlElement("success")]
        public bool success { get; set; }

        [XmlElement("code")]
        public int code { get; set; }
    }
}
