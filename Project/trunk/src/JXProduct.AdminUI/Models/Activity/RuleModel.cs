﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace JXProduct.AdminUI.Models.Activity
{
    public class RuleModel
    {
        public RuleModel()
        {
            this.RuleID = 0;
            this.ActID = 0;
        }
        public int? RuleID { get; set; }

        [Required(ErrorMessage = "没有参数,活动ID")]
        public int ActID { get; set; }
        [Required(ErrorMessage = "参数丢失,没有活动类型")]
        public short ActType { get; set; }

        [RegularExpression("^[1-9]\\d*$", ErrorMessage = ("金额输入错误"))]
        public decimal? Amount { get; set; }

        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "数量输入错误")]
        public int? Quantity { get; set; }

        [RegularExpression("^[1-9]\\d*$", ErrorMessage = ("请输入一个数字"))]
        public decimal? DiscountAmount { get; set; }

        [Range(0, 10, ErrorMessage = "数字应为0-10区间内")]
        public decimal? Discount { get; set; }

        [RegularExpression("^[1-9]\\d*$", ErrorMessage = ("请输入商品的ID"))]
        public string ProductID { get; set; }

        [StringLength(16, ErrorMessage = "优惠劵长度不正确")]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = ("请输入正确的优惠劵编号"))]
        public string CouponBatchNo { get; set; }
    }
}