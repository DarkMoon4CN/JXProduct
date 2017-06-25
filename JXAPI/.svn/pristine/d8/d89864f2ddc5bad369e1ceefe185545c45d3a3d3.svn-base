using JXAPI.JXSdk.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace JXAPI.JXSdk.Response
{
    public class CouponValidateResponse: Venus.VenusClientResponse
    {
        [XmlElement("coupon")]
        public CouponChoseInfo coupon { get; set; }

        [XmlElement("code")]
        public int code { get; set; }

        [JsonProperty("message")]
        public string ErrMsg { get; set; }
    }
}
