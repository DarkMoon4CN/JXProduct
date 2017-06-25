using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    public class AccountRecordListRequest : Venus.IVenusRequest<AccountRecordListResponse>
    {


        public int? passportID { get; set; }
        public string passport { get; set; }
        public long orderID { get; set; }
        public int? scoreType { get; set; }
        public int? minScore { get; set; }
        public int? maxScore { get; set; }
        public long? startTime { get; set; }
        public long? endTime { get; set; }

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
