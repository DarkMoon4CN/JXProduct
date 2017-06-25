using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    public class RegisterRequest : Venus.IVenusRequest<RegisterResponse>
    {
        public UserInfo userAddTemplate { get; set; }
        public ReceiverInfo receiverTemplate { get; set; }
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
