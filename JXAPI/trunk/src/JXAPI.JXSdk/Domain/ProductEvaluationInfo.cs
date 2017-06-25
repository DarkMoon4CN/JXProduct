using System;
using System.Collections.Generic;

namespace JXAPI.JXSdk.Domain
{
    /// <summary>
    /// 商品评论
    /// </summary>
    public class ProductEvaluationInfo
    {
        /// <summary>
        /// 咨询ID
        ///</summary>
        public int evaluationID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int productID { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string productName { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int uid { get; set; }
        
        /// <summary>
        ///  用户名称
        ///</summary>
        public string userName { get; set; }
                
        /// <summary>
        ///  咨询时间
        ///</summary>
        public long evaTime { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        ///  内容
        ///</summary>
        public string content { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public int parentID { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public short score { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public short status { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public short source { get; set; }

        public IList<EvaluationKeyword> evaluationKeywordList { get; set; }
    }
}
