using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using JXAPI.JXSdk.Domain;

namespace JXAPI.JXSdk.Response
{
    /// <summary>
    /// app版本接收
    /// </summary>
    public class AppVersionListResponse : Venus.VenusClientResponse
    {
        [XmlElement("list")]
        public IList<AppVersionInfo> list { get; set; }

        [XmlElement("count")]
        public int count { get; set; }
    }
}
