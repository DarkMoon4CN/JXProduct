using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXAPI.Service.Models
{
    public class ShippingInfo
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        /// 快递号
        /// </summary>
        public string Express_Sn { get; set; }

        /// <summary>
        /// 快递公司代号
        /// </summary>
        public string Express_Id { get; set; }

        /// <summary>
        /// 快递公司名字
        /// </summary>
        public string Express_Name { get; set; }

        /// <summary>
        /// 发货人
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// 发货时间
        /// </summary>
        public long Shipped_Time { get; set; }
    }
}