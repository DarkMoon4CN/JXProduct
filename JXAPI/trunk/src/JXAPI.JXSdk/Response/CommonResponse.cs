﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JXAPI.JXSdk.Response
{
    /// <summary>
    /// 通用接收
    /// </summary>
    public class CommonResponse : Venus.VenusClientResponse
    {
        [XmlElement("count")]
        public int count { get; set; }

        [XmlElement("code")]
        public int code { get; set; }

        [XmlElement("success")]
        public bool success { get; set; }

        [XmlElement("message")]
        public string message { get; set; }
    }
}
