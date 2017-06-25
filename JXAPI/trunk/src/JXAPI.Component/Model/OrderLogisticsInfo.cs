using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{
    public class OrderLogisticsInfo
    {
        public int LogisticsID { get; set; }

        public string OrderID { get; set; }

        /// <summary>
        /// 三方单号
        /// </summary>
        public int ThirdOrderID { get; set; }

        public string OrderSplitID { get; set; }

        public int HouseID { get; set; }

        public int ExpressID { get; set; }

        public string LogisticsCompany { get; set; }

        public string LogisticsNum { get; set; }

        public DateTime CreateTime { get; set; }

        public int UID { get; set; }

        public string Creator { get; set; }

        public decimal Weight { get; set; }

        public string Packaging { get; set; }

        public string Operator { get; set; }

        public DateTime OperateTime { get; set; }
    }
}
