﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Domain
{
    public class CouponTypeInfo
    {
        /// <summary>
        /// 类型编号 
        /// </summary>
        public int typeID { get; set; }
        /// <summary>
        /// 1：现金 2：折扣 3：换物 4:满返 
        /// </summary>
        public int typeCategory { get; set; }
        /// <summary>
        /// 类型名称（现金，折扣，换物，满返） 
        /// </summary>
        public string typeName { get; set; }
        /// <summary>
        /// 0:货到付款1:网上支付 
        /// </summary>
        public string paymentMethod { get; set; }
        /// <summary>
        /// 限制使用商品（多商品ID以逗号隔开） 
        /// </summary>
        public string productInfo { get; set; }
        /// <summary>
        /// 最低商品价格
        /// </summary>
        public decimal productPriceMin { get; set; }
        /// <summary>
        /// 最低订单金额 
        /// </summary>
        public decimal orderMin { get; set; }
        /// <summary>
        /// 是否可以作用于运费 
        /// </summary>
        public int toShipFee { get; set; }
        /// <summary>
        /// 1:官网 2:H5 3:App  1或者 1,2
        /// </summary>
        public string couponChannel { get; set; }
        /// <summary>
        /// 是否可用作特例品可以:1否:0 
        /// </summary>
        public int toSpecial { get; set; }
        /// <summary>
        /// 优惠券金额
        /// </summary>
        public decimal nominalValue { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal preferential { get; set; }
        /// <summary>
        /// 使用类型 0:正常1:可多次使用 
        /// </summary>
        public int useType { get; set; }
        /// <summary>
        /// 创建时间 
        /// </summary>
        public long? createTime { get; set; }
        /// <summary>
        ///创建者 
        /// </summary>
        public string creator { get; set; }
        /// <summary>
        /// 状态（1：正常 2：作废） 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 备注 
        /// </summary>
        public string remarks { get; set; }
    }
}
