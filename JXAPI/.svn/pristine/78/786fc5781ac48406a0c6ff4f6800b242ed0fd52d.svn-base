using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    public class AccountBalanceListRequest : Venus.IVenusRequest<AccountBalanceListResponse>
    {
        public PageFormInfo pageForm { get; set; }
        public int? passportID { get; set; }
        public int? status { get; set; }
        public long? startTime { get; set; }
        public long? endTime { get; set; }


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
