using JXAPI.JXSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    public class CouponBindRequest : Venus.IVenusRequest<CommonResponse>
    {
        /// <summary>
        /// 用户userId
        /// </summary>
        public int? userId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string pass { get; set; }

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
