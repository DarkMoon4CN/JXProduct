using JXAPI.JXSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    public class OrdersAdminRequest: Venus.IVenusRequest<CommonResponse>
    {
        public int uid { get; set; }

        public long orderID { get; set; }
        public string cause { get; set; }
        public int orderStatus { get; set; }

        /// <summary>
        /// 操作用户
        /// </summary>
        public string loginUsername { get; set; }

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
