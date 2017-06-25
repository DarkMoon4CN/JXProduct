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
    /// 健康说
    /// </summary>
    public class TipsRequest : Venus.IVenusRequest<TipsResponse>
    {
        #region 参数
        /// <summary>
        /// 健康说ID
        /// </summary>
        public int tipsId { get; set; } 

        /// <summary>
        /// 审核 类型 1:审核通过，2屏蔽
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string updater { get; set; }
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
            Venus.Utils.RequestValidator.ValidateRequired("tipsId", this.tipsId);
        }
        #endregion
    }
}
