using JXAPI.JXSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    public class AccountUpdateBalanceRequest : Venus.IVenusRequest<CommonResponse>
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public long orderID {get;set;}

        /// <summary>
        /// 金额
        /// </summary>
        public decimal balance { get; set; }          

        /// <summary>
        /// 账号ID
        /// </summary>
        public int passportID { get; set; }            
        /// <summary>
        /// 备注
        /// </summary>
        public string remarks { get; set; }

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
