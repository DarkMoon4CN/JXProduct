using JXProduct.Component.BLL;
using System;
using System.Collections.Generic;
using System.Text;
namespace JXProduct.Component.Model
{
    [Serializable]
    public class ProductActivityInfo
    {
        public ProductActivityInfo()
        {
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now.AddDays(7);
            this.CreateTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
        }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 全站活动说明/分类活动 ID
        /// </summary>
        public int ActID { get; set; }

        /// <summary>
        /// 全站活动说明/分类活动
        /// </summary>
        public string ActName { get; set; }

        /// <summary>
        /// 单品活动说明
        /// </summary>
        public string ActDesc { get; set; }
        /// <summary>
        ///活动库存
        /// </summary>
        public int ActStock { get; set; }

        /// <summary>
        ///活动要求数量
        /// </summary>
        public int ActQuantity { get; set; }

        /// <summary>
        ///活动要求价格
        /// </summary>
        public decimal ActPrice { get; set; }

        /// <summary>
        ///单品活动类型  包邮 = 1,满赠 = 2,满减 = 3,满返 = 4,换购 = 5,满折 = 6,直降 = 7,折扣 = 8
        /// </summary>
        public short Type { get; set; }

        /// <summary>
        /// 单品活动限制   金额：1  件数：2   折扣跟直降没有限制
        /// </summary>
        public short TypeLimit
        {
            get
            {
                short limit = 0;
                if (this.Type == 7 || this.Type == 8)
                {
                }
                else if (this.ActQuantity > 0)
                {
                    limit = 2;
                }
                else if (this.ActPrice > 0)
                {
                    limit = 1;
                }
                return limit;
            }
        }
        /// <summary>
        ///单品活动库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        ///优惠劵名称
        /// </summary>
        public string CouponBatchNo { get; set; }

        /// <summary>
        ///优惠券名称（指定CouponBatchNo时必须指定该字段）
        /// </summary>
        public string CouponName { get; set; }

        /// <summary>
        ///折扣/折扣金额
        /// </summary>
        public decimal Discount { get; set; }
        

        /// <summary>
        /// 满赠 送
        /// </summary>
        public int ProductGiftID { get; set; }
        public string ProductGiftName { get; set; }

        /// <summary>
        ///是否包邮（0：否 1：是）
        /// </summary>
        public short IsFreeShip { get; set; }

        /// <summary>
        /// 不参与其他活动类型的过滤条件，以","分隔
        /// 如：不参与优惠券和购物卡
        /// coupon,shoppingcard
        /// </summary>
        public string ExtType { get; set; }

        /// <summary>
        ///是否可用  0：否 1：是
        /// </summary>
        public short Status { get; set; }

        /// <summary>
        ///开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        ///创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///最后更新者
        /// </summary>
        public string Updater { get; set; }

        /// <summary>
        ///更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }


        /// <summary>
        /// 全场活动类型
        /// </summary>
        public int ActivityType { get; set; }
        /// <summary>
        /// 全场活动名称
        /// </summary>
        public string ActivityName { get; set; }
    }
}
