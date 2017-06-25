using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Domain
{
    public class ShoppingCardConsumeInfo
    {
        public int CardDetailID { get; set; }
        public string CardNo { get; set; }
        public string OrderID { get; set; }
        public int UID { get; set; }
        public string UserName { get; set; }
        public decimal OrderFee { get; set; }
        public decimal RemainingSum { get; set; }
        public long? CreateTime { get; set; }
        public string Remarks { get; set; }
    }
}
