using JXAPI.JXSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    public class AccountEnableStutusRequest :Venus.IVenusRequest<CommonResponse>
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int passportID { get; set; }

        /// <summary>
        ///  0启用 1;冻结 默认0
        /// </summary>
        public int status { get; set; }
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
