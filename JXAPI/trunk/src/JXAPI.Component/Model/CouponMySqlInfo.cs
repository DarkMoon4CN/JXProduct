using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{
    public class CouponMySqlInfo
    {
        public int ID { get; set; }
        public string BatchID { get; set; }
        public int ChannelID { get; set; }
        public int TypeID { get; set; }
        public string CouponCode { get; set; }
        public string CouponPass { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int HasSend { get; set; }
        public int UID { get; set; }
        public string UserName { get; set; }
        public DateTime UseTime { get; set; }
        public int IsUsed { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
