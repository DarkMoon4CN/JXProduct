using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{
    public class ProductOutBookInfo
    {
        /// <summary>
        /// （缺货）预定ID
        /// </summary>
        public int OutID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 商品中文名
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 金象码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TradePrice { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UID { get; set; }

        /// <summary>
        /// 顾客姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 顾客手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 预定数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 顾客留言
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 预定时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Updater { get; set; }

        /// <summary>
        /// 是否回复（0：否 1：是）
        /// </summary>
        public short Status { get; set; }

        /// <summary>
        /// 是否订购（0：否 1：是）
        /// </summary>
        public short HasBuy { get; set; }
    }
}
