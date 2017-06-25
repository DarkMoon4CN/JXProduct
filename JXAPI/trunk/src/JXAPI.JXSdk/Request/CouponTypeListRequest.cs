using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    public class CouponTypeListRequest : Venus.IVenusRequest<CouponTypeListResponse>
    {
        #region 参数
        
        /// <summary>
        /// 1：现金 2：折扣 3：换物 4:满返,0全部
        /// </summary>
        public int typeCategory { get; set; }
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
