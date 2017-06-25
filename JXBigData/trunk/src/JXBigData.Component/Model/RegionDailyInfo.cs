using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXBigData.Component.Model
{
    public class RegionDailyInfo
    {
        public int ID { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public int TotalQuan { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
