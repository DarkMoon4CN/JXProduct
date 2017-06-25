using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXBigData.Component.Model
{
    public class SearchKeywordDailyInfo
    {
        public int Rowid { get; set; }
        public int ID { get; set; }
        public DateTime CreateDate { get; set; }
        public int Source { get; set; }
        public string  Keywords { get; set; }
        public int TotalQuan { get; set; }
    }
}
