﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Response;

namespace JXAPI.JXSdk.Request
{
    /// <summary>
    /// 健康头条商品
    /// </summary>
    public class HeadLinesProductRequest : Venus.IVenusRequest<HeadLinesProductResponse>
    {
        #region 参数
        /// <summary>
        /// 头条ID
        /// </summary>
        public int headID { get; set; }

        /// <summary>
        /// 分页
        /// </summary>
        public PageFormInfo pageForm { get; set; }
        #endregion

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
            Venus.Utils.RequestValidator.ValidateRequired("headID", this.headID);
        }
        #endregion
    }
}
