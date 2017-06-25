using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using JXAPI.JXSdk.Domain;

namespace JXAPI.JXSdk.Response
{
    /// <summary>
    /// 商品评论列表
    /// </summary>
    public class ProductEvaluationListResponse : Venus.VenusClientResponse
    {
        [XmlElement("evaluationList")]
        public IList<ProductEvaluationInfo> evaluationList { get; set; }

        [XmlElement("totalPage")]
        public int totalPage { get; set; }
        
        [XmlElement("total")]
        public int total { get; set; }
    }
}
