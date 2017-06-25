using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Response;
using Venus.Utils;

namespace JXAPI.JXSdk.Request
{
    /// <summary>
    /// 意见反馈列表
    /// </summary>
    public class FeedBackListRequest : Venus.IVenusRequest<FeedBackListResponse>
    {
        #region 参数
        /// <summary>
        /// 开始时间
        /// </summary>
        public long? startTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public long? endTime { get; set; }

        /// <summary>
        /// 未审核传-1，已审核传1
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        public string channels { get; set; }

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
