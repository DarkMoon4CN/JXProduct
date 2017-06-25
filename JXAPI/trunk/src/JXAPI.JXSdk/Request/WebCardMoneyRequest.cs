using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JXAPI.JXSdk.Response;

namespace JXAPI.JXSdk.Request
{
    public class WebCardMoneyRequest : Venus.IVenusRequest<CommonResponse>
    {
        public long orderId { get; set; }

        public int userId { get; set; }

        public decimal orderFee { get; set; }

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
