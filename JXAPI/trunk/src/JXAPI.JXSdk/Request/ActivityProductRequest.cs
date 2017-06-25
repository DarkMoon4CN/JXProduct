using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Response;

namespace JXAPI.JXSdk.Request
{
    /// <summary>
    /// 精彩活动商品
    /// </summary>
    public class ActivityProductRequest : Venus.IVenusRequest<ActivityProductResponse>
    {
        #region 参数
        /// <summary>
        /// 活动ID
        /// </summary>
        public int actID { get; set; }

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
            Venus.Utils.RequestValidator.ValidateRequired("actID", this.actID);
        }
        #endregion
    }
}
