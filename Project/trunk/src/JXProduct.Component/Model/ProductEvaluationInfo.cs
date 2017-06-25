using System;
using System.Collections.Generic;
namespace JXProduct.Component.Model
{
    /// <summary>
    /// 商品评论
    /// </summary>
    public class ProductEvaluationInfo
    {
        /// <summary>
        /// 咨询ID
        ///</summary>
        public Int64 EvaluationID { get; set; }

        /// <summary>
        ///  商品ID
        ///</summary>
        public Int32 ProductID { get; set; }

        /// <summary>
        ///  金象码
        ///</summary>
        public String ProductCode { get; set; }

        /// <summary>
        ///  用户UID
        ///</summary>
        public Int32 UID { get; set; }

        /// <summary>
        ///  用户名称
        ///</summary>
        public String UserName { get; set; }

        /// <summary>
        ///  咨询时间
        ///</summary>
        public DateTime EvalTime { get; set; }

        /// <summary>
        ///  标题
        ///</summary>
        public String Title { get; set; }

        /// <summary>
        ///  内容
        ///</summary>
        public String Content { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public short Score { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public Decimal ScoreDes { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public Decimal ScoreService { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public Decimal ScoreSend { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public Decimal ScoreLogistics { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public Int32 FlowerCount { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public Int32 EggCount { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public String OrderID { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public short Status { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        ///  
        ///</summary>
        public string Source { get; set; }

    }
}
