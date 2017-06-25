﻿using JXAPI.JXSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    public class DeductMoneyRequest : Venus.IVenusRequest<CommonResponse>
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string cardNo{get;set;}

        /// <summary>
        /// 订单号
        /// </summary>
        public string orderID{get;set;}
        /// <summary>
        /// 用户名
        /// </summary>
        public string userName{get;set;}

        /// <summary>
        /// 订单费用
        /// </summary>
        public decimal orderFee{get;set;}

        /// <summary>
        /// 备注
        /// </summary>
        public string remarks{get;set;}

        /// <summary>
        /// 用户Id
        /// </summary>
        public int uid { get; set; }

        public int targetPassportID { get; set; }

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
