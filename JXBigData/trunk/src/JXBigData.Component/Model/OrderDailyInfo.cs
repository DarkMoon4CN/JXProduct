using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXBigData.Component.Model
{
    public class OrderDailyInfo
    {
        public DateTime CreateDate { get; set; }
        public int Source { get; set; }
        public string SourceName { get; set; }
        public int OrderQuan { get; set; }
        public decimal OrderAmount { get; set; }
        public int  PayQuan { get; set; }
        public decimal PayAmount { get; set; }
        public int SendQuan { get; set; }
        public decimal SendAmount { get; set; }
    }
}
