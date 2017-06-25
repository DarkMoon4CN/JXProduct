using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.JXSdk.Domain
{
    [Serializable]
    public class OrderLogisticsResponseInfo
    {
        public OrderLogisticsInfo info { get; set; }
    }

    [Serializable]
    public class OrderLogisticsInfo
    {
        public int expressId { get; set; }
        public string expressCode { get; set; }
        public string expressName { get; set; }

        public IList<LogisticsInfo> list { get; set; }
    }

    [Serializable]
    public class LogisticsInfo
    {
        public string cdate { get; set; }

        public string process { get; set; }
    }
}
