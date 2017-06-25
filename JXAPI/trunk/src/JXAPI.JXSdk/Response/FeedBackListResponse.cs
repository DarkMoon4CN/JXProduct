using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using JXAPI.JXSdk.Domain;

namespace JXAPI.JXSdk.Response
{
    /// <summary>
    /// 意见反馈列表接收
    /// </summary>
    public class FeedBackListResponse : Venus.VenusClientResponse
    {
        [XmlElement("list")]
        public IList<FeedBackInfo> list { get; set; }

        [XmlElement("count")]
        public int count { get; set; }
    }
}
