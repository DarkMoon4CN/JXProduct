using JXAPI.JXSdk.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JXAPI.JXSdk.Response
{
    public class CouponChoseListResponse : Venus.VenusClientResponse
    {
        [XmlElement("list")]
        public IList<CouponChoseInfo> list { get; set; }

        [XmlElement("code")]
        public int code { get; set; }

        [XmlElement("success")]
        public bool success { get; set; }
    }
}
