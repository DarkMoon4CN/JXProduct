﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JXProduct.AdminUI.Models.Product
{
    public class PriceModel
    {
        [Required]
        public int ProductID { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "请填写正确的市场价")]
        [DataType(DataType.Currency, ErrorMessage = "请填写正确的市场价")]
        public decimal MarketPrice { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "请填写正确的金象价")]
        [DataType(DataType.Currency, ErrorMessage = "请填写正确的金象价")]
        public decimal TradePrice { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "请填写正确的移动端价格")]
        [DataType(DataType.Currency, ErrorMessage = "请填写正确的移动端价格")]
        public decimal MobilePrice { get; set; }


        public bool ValidatePrice(decimal costprice)
        {
            return MarketPrice >= TradePrice && MarketPrice >= MobilePrice && TradePrice > costprice && MobilePrice > costprice;

        }
    }
}