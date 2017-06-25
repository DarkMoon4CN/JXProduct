﻿using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    public class CouponListRequest : Venus.IVenusRequest<CouponListResponse>
    {
        #region 参数

        /// <summary>
        /// 用户ID
        /// </summary>
        public int userId { get; set; }

        /// <summary>
        /// 优惠券批次号 
        /// </summary>
        public string batchID { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string couponCode { get; set; }
        /// <summary>
        /// 渠道号
        /// </summary>
        public int? channelID { get; set; }
        /// <summary>
        /// 类型编号
        /// </summary>
        public int? typeID { get; set; }
        /// <summary>
        /// 是否使用(1:未使用2已使用0全部)
        /// </summary>
        public int? isUsed { get; set; }
        /// <summary>
        /// 是否送出(1:未送出2已送出0全部)
        /// </summary>
        public int? hasSend { get; set; }
        /// <summary>
        /// 使用者
        /// </summary>
        public string userName { get; set; }

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
        }

        #endregion
    }
}
