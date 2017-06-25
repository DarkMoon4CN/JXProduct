using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using JXAPI.JXSdk.Domain;

namespace JXAPI.JXSdk.Response
{
    /// <summary>
    /// 精彩活动接收
    /// </summary>
    public class ActivityListResponse : Venus.VenusClientResponse
    {
        [XmlElement("list")]
        public IList<ActivityInfo> list { get; set; }

        [XmlElement("count")]
        public int count { get; set; }

        [XmlElement("products")]
        public string products { get; set; }
    }
}
