using JXAPI.Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXAPI.Service.Models
{
    public class OrdersResponse
    {
        #region 订单信息
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        /// 下单用户
        /// </summary>
        public int UID { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal PaySum { get; set; }

        /// <summary>
        /// 配送费
        /// </summary>
        public decimal NewShipFee { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal VoucherFee { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public short PaymentMethodID { get; set; }

        /// <summary>
        /// 收件省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// 收件人电话
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 订单备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public long CreateTime { get; set; }

        /// <summary>
        /// 付款状态
        /// </summary>
        public short PayStatus { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public short OrderStatus { get; set; }

        /// <summary>
        /// 预付金额
        /// </summary>
        public decimal ReduceFee { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundFee { get; set; }

        /// <summary>
        /// 2015-10-28 FOR lm
        /// </summary>
        public decimal ProductFee { get; set; }

        /// <summary>
        /// 2015-10-28 FOR lm
        /// </summary>
        public decimal ShipMethodID { get; set; }

        /// <summary>
        /// 2015-10-28 FOR lm
        /// </summary>
        public string Invoice { get; set; }
        #endregion

        /// <summary>
        /// 订单商品
        /// </summary>
        public IList<OrderProductInfo> OrderDetails{ get; set; }
    }
}