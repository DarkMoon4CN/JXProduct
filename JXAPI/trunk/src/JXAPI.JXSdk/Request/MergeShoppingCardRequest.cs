using JXAPI.JXSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    public class MergeShoppingCardRequest : Venus.IVenusRequest<CommonResponse>
    {
        #region 参数
        /// <summary>
        /// 主卡
        /// </summary>
        public string mainCode{ get; set; }
        /// <summary>
        /// 主卡密码
        /// </summary>
        public string mainPass { get; set; }
        /// <summary>
        /// 副卡
        /// </summary>
        public string viceCode { get; set; }
        /// <summary>
        /// 副卡密码
        /// </summary>
        public string vicePass { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string creator { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remarks { get; set; }
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
