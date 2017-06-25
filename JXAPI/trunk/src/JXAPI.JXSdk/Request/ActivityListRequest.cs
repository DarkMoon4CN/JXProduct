using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Response;

namespace JXAPI.JXSdk.Request
{
    /// <summary>
    /// 精彩活动列表
    /// </summary>
    public class ActivityListRequest : Venus.IVenusRequest<ActivityListResponse>
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
        /// 活动ID
        /// </summary>
        public int actID { get; set; }

        /// <summary>
        /// 1，从高到底，2从低到高
        /// </summary>
        public int sort { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public int categoryID { get; set; }

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
