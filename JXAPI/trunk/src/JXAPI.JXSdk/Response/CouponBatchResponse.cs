using JXAPI.JXSdk.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JXAPI.JXSdk.Response
{
    public class CouponBatchResponse : Venus.VenusClientResponse
    {
        [XmlElement("template")]
        public CouponBatchInfo template { get; set; }

        [XmlElement("code")]
        public int code { get; set; }

        [XmlElement("success")]
        public bool success { get; set; }
    }
}
