using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using JXAPI.JXSdk.Domain;

namespace JXAPI.JXSdk.Response
{
    /// <summary>
    /// 头条评论接收
    /// </summary>
    public class HeadLinesCommentResponse : Venus.VenusClientResponse
    {
        [XmlElement("tips")]
        public IList<HeadLinesCommentInfo> list { get; set; }

        [XmlElement("count")]
        public int count { get; set; }
    }
}