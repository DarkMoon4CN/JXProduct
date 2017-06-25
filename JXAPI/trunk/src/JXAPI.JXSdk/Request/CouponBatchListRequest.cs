﻿using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    public class CouponBatchListRequest : Venus.IVenusRequest<CouponBatchListResponse>
    {

        /// <summary>
        /// 渠道名称 
        /// </summary>
        public string batchID { get; set; }
        /// <summary>
        /// 是否为外部合作渠道(2：不是 1：是,0全部) 
        /// </summary>
        public int channelID { get; set; }

        /// <summary>
        /// 起始日期
        /// </summary>
        public string startTime { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string endTime { get; set; }

        /// <summary>
        /// 页面大小  page size
        /// </summary>
        public PageFormInfo pageForm { get; set; }

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