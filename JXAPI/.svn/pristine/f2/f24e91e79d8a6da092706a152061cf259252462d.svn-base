using JXAPI.JXSdk.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JXAPI.JXSdk.Response
{
    public class AccountListResponse : Venus.VenusClientResponse
    {
        [XmlElement("list")]
        public IList<AccountInfo> accountTemplates { get; set; }

        [XmlElement("totalNumber")]
        public int totalNumber { get; set; }

        [XmlElement("code")]
        public int code { get; set; }
    }
}
