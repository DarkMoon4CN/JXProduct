using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using JXAPI.JXSdk.Domain;

namespace JXAPI.JXSdk.Response
{
    /// <summary>
    /// 健康头条接收
    /// </summary>
    public class HeadLinesResponse : Venus.VenusClientResponse
    {
        [XmlElement("tips")]
        public HeadLinesInfo headLines { get; set; }

        [XmlElement("code")]
        public int code { get; set; }

        [XmlElement("success")]
        public bool success { get; set; }
    }
}