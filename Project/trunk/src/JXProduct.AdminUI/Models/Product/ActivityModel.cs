﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using JXProduct.Component.Model;

namespace JXProduct.AdminUI.Models.Product
{
    public class ActivityModel
    {
        public ActivityModel()
        {
            this.Type = "0";
            this.StartDate = DateTime.Now.ToString("yyyy-MM-dd HH:00");
            this.EndDate = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd HH:00");
        }
        public int ProductID { get; set; }


        public string Type { get; set; }

        public string ActDesc { get; set; }
        public int ActQuantity { get; set; }
        public decimal ActPrice { get; set; }


        //优惠劵
        public string CouponBatchNo { get; set; }
        public string CouponName { get; set; }

        //折扣
        public decimal Discount { get; set; }


        //赠品
        public int ProductGiftID { get; set; }
        public string ProductGiftName { get; set; }

        ////包邮
        //public short IsFreeShip { get; set; }

        ////可用
        //public short Status { get; set; }

        //不予其他混用
        public bool isCoupon { get; set; }
        public bool isBuyCard { get; set; }


        /// <summary>
        ///  开始日期
        ///</summary>
        public string StartDate { get; set; }

        /// <summary>
        ///  结束日期
        ///</summary>
        public string EndDate { get; set; }

        public string Msg { get; set; }


        public ProductInfo Product { get; set; }
    }
}