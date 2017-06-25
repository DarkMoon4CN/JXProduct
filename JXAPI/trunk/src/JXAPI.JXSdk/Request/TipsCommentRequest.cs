using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Response;

namespace JXAPI.JXSdk.Request
{
    /// <summary>
    /// 健康说评论
    /// </summary>
    public class TipsCommentRequest : Venus.IVenusRequest<TipsCommentResponse>
    {
        #region 参数
        /// <summary>
        /// 健康说ID
        /// </summary>
        public int tipsID { get; set; }

        /// <summary>
        /// 正序传asc 倒序传desc
        /// </summary>
        public string soft { get; set; }

        /// <summary>
        /// 分页
        /// </summary>
        public PageFormInfo pageform { get; set; }
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
            Venus.Utils.RequestValidator.ValidateRequired("tipsID", this.tipsID);
        }
        #endregion
    }
}
