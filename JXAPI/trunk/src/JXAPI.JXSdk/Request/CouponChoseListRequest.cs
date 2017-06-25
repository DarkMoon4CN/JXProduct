using JXAPI.JXSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Request
{
    public class CouponChoseListRequest : Venus.IVenusRequest<CouponChoseListResponse>
    {
       public int chanel { get; set; }
       public decimal orderMoney{get;set;}
       public int toSpecial{get;set;}
       public List<string> productIds{get;set;}
       public int uid{get;set;}
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
