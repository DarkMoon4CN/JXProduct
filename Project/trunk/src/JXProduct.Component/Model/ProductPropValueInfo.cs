﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.Model
{
    public class ProductSimpleInfo
    {
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ChineseName { get; set; }
        public string TradeName { get; set; }
        public string CADN { get; set; }
        public string Specifications { get; set; }
        public string BarCode { get; set; }

    }
    public class ProductPropValueInfo : ProductSimpleInfo
    {
        public int GroupID { get; set; }
        public string Spec { get; set; }
        public string Prop1 { get; set; }
        public string Prop2 { get; set; }
        public string Prop3 { get; set; }
    }
}