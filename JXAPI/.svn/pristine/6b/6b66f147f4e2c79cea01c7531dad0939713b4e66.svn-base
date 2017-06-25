using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using JXAPI.JXSdk.Domain;

namespace JXAPI.JXSdk.Response
{
    /// <summary>
    /// 头条商品接收
    /// </summary>
    public class HeadLinesProductResponse : Venus.VenusClientResponse
    {
        [XmlElement("tips")]
        public IList<HeadlinesProductInfo> list { get; set; }

        [XmlElement("totalCount")]
        public int totalCount { get; set; }
    }
}
