using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using JXAPI.JXSdk.Domain;

namespace JXAPI.JXSdk.Response
{
    /// <summary>
    /// 头条列表接收
    /// </summary>
    public class HeadLinesListResponse : Venus.VenusClientResponse
    {
        [XmlElement("list")]
        public IList<HeadLinesInfo> list { get; set; }

        [XmlElement("count")]
        public int count { get; set; }
    }
}