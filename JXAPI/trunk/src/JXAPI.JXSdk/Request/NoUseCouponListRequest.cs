﻿using JXAPI.JXSdk.Domain;
using JXAPI.JXSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    public class NoUseCouponListRequest : Venus.IVenusRequest<NoUseCouponListResponse>
    {
        /// <summary>
        ///  用户ID
        /// </summary>
        public int uid { get; set; }

        public int targetPassportID { get; set; }

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