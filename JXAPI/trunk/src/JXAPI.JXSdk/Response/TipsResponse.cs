using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using JXAPI.JXSdk.Domain;

namespace JXAPI.JXSdk.Response
{
    /// <summary>
    /// 健康说
    /// </summary>
    public class TipsResponse : Venus.VenusClientResponse
    {
        [XmlElement("tips")]
        public TipsInfo tips { get; set; }

        [XmlElement("success")]
        public bool success { get; set; }
    }
}
