using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using JXAPI.JXSdk.Domain;

namespace JXAPI.JXSdk.Response
{
    /// <summary>
    /// 健康说列表
    /// </summary>
    public class TipsListResponse : Venus.VenusClientResponse
    {
        [XmlElement("list")]
        public IList<TipsInfo> list { get; set; }

        [XmlElement("count")]
        public int count { get; set; }
    }
}
