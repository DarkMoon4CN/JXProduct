﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using JXAPI.JXSdk.Domain;

namespace JXAPI.JXSdk.Response
{
    /// <summary>
    /// 健康说评论
    /// </summary>
    public class TipsCommentResponse : Venus.VenusClientResponse
    {
        [XmlElement("tips")]
        public IList<TipsCommentInfo> list { get; set; }

        [XmlElement("count")]
        public int count { get; set; }
    }
}