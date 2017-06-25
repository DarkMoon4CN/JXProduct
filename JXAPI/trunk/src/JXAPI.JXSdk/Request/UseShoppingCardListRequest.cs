using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    public class UseShoppingCardListRequest : Venus.IVenusRequest<UseShoppingCardListResponse>
    {
        /// <summary>
        ///  用户ID
        /// </summary>
        public int uid { get; set; }

        public int targetPassportID { get; set; }

        /// <summary>
        /// 1:可用列表（未过期，剩余金额大于0）0：不可用列表(包含过期的,没有钱的,卡无效的) 
        /// </summary>
        public uint isUse { get; set; }

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
