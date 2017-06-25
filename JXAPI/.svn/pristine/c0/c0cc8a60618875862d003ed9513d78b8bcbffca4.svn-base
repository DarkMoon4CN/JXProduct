using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using JXAPI.JXSdk.Domain;

namespace JXAPI.JXSdk.Response
{
    /// <summary>
    /// 商品评论
    /// </summary>
    public class ProductEvaluationResponse : Venus.VenusClientResponse
    {
        [XmlElement("evaluationDetail")]
        public ProductEvaluationInfo evaluationDetail { get; set; }

        [XmlElement("evaluationKeywordList")]
        public IList<EvaluationKeyword> evaluationKeywordList { get; set; }

        [XmlElement("success")]
        public bool success { get; set; }
    }
}
