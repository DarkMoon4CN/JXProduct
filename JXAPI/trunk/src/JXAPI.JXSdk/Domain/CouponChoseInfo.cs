﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Domain
{
    public class CouponChoseInfo
    {
        public string couponCode { get; set; }
        public int isUsed { get; set; }
        public long? endTime { get; set; }
        public string typeName { get; set; }
        public decimal preferential { get; set; }
        public string title { get; set; }
        public int status { get; set; }

    }
}