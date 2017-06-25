﻿using JXAPI.JXSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    public class CouponValidateRequest: Venus.IVenusRequest<CouponValidateResponse>
    {
        public int userId { get; set; }
        public int chanel { get; set; }
        public decimal orderMoney { get; set; }
        public int? toSpecial { get; set; }
        public List<string> productIds { get; set; }
        public string code { get; set; }
        public string pass { get; set; }
        #region ITopRequest Members

       public string GetApiName()
       {
           return string.Empty;
       }

       public IDictionary<string, string> GetParameters()
       {
           return null;
       }

       public void Validate()
       {
       }

       #endregion
    }
}