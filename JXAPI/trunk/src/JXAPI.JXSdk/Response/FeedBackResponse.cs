﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using JXAPI.JXSdk.Domain;

namespace JXAPI.JXSdk.Response
{
    /// <summary>
    /// 意见反馈
    /// </summary>
    public class FeedBackResponse : Venus.VenusClientResponse
    {
        [XmlElement("dto")]
        public FeedBackInfo dto { get; set; }

        [XmlElement("success")]
        public bool success { get; set; }
    }
}