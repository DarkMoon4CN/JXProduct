using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{
    [Serializable]
    public class ActivityRuleInfo
    {
        /// <summary>
        /// 活动规则明细表
        /// </summary>
        public int RuleID { get; set; }

        /// <summary>
        /// 活动编号
        /// </summary>
        public int ActID { get; set; }

        /// <summary>
        /// 满金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 满件数
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 满减金额
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 满减折扣
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// 满赠品
        /// </summary>
        public string ProductID { get; set; }

        /// <summary>
        /// 满赠券批号
        /// </summary>
        public string CouponBatchNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 最后更新者
        /// </summary>
        public string Updater { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 状态0：有效 1：失效
        /// </summary>
        public short Status { get; set; }
    }
}
