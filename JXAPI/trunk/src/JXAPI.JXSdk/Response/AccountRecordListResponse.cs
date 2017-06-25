using JXAPI.JXSdk.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JXAPI.JXSdk.Response
{
    public class AccountRecordListResponse : Venus.VenusClientResponse
    {
        [XmlElement("templates")]
        public IList<AccountRecordInfo> templates { get; set; }

        [XmlElement("totalNumber")]
        public int totalNumber { get; set; }

        [XmlElement("code")]
        public int code { get; set; }
    }
}
