using JXAPI.JXSdk.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JXAPI.JXSdk.Response
{
    public class CouponListResponse: Venus.VenusClientResponse
    {
        [XmlElement("list")]
        public IList<CouponInfo> list { get; set; }

        [XmlElement("count")]
        public int count { get; set; }

        [XmlElement("code")]
        public int code { get; set; }
    }
}
