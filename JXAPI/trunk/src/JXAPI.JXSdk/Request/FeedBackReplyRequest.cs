using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Venus.Utils;
using JXAPI.JXSdk.Response;

namespace JXAPI.JXSdk.Request
{
    /// <summary>
    /// 意见反馈回复
    /// </summary>
    public class FeedBackReplyRequest : Venus.IVenusRequest<FeedBackResponse>
    {
        #region 参数
        /// <summary>
        /// 意见反馈ID
        /// </summary>
        public int feedBackId { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        public string replyContents { get; set; }

        /// <summary>
        /// 回复ID
        /// </summary>
        public int uid { get; set; }

        /// <summary>
        /// 回复用户
        /// </summary>
        public string userName { get; set; }
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
            Venus.Utils.RequestValidator.ValidateRequired("feedBackId", this.feedBackId);
        }

        #endregion
    }
}
