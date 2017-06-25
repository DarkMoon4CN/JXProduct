using JXAPI.JXSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    public class ReleaseBindRequest : Venus.IVenusRequest<CommonResponse>
    {
        public int? uid { get; set; }
        public int? userId { get; set; }
        public string userName { get; set; }
        public decimal remainingSum { get; set; }
        public string code { get; set; }

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
