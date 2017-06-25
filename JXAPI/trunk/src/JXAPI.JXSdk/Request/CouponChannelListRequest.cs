using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    public class CouponChannelListRequest : Venus.IVenusRequest<CouponChannelListResponse>
    {
        /// <summary>
        /// 渠道名称 
        /// </summary>
        public string channelName { get; set; }
        /// <summary>
        /// 是否为外部合作渠道(2：不是 1：是,0全部) 
        /// </summary>
        public int isOuter { get; set; }
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
