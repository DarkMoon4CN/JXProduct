using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    public class AccountListRequest : Venus.IVenusRequest<AccountListResponse>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int? passportID { get; set; }
        public string passport { get; set; }
        /// <summary>
        /// 用户等级
        /// </summary>
        public int? rank { get; set; }
        /// <summary>
        /// 用户状态 0：正常1：冻结
        /// </summary>
        public int? status { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public int? sourceID { get; set; }

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
