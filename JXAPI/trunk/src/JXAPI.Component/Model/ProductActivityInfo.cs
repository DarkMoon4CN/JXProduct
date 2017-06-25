using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{
    public class ProductActivityInfo
    {
        public int ProductID { get; set; }

        public int ActID { get; set; }

        public string ActName { get; set; }

        public int ActStock { get; set; }

        public int ActQuantity { get; set; }

        public decimal ActPrice { get; set; }

        public string ActDesc { get; set; }

        public short Type { get; set; }

        public short TypeLimit { get; set; }

        public string CouponBatchNo { get; set; }

        public string CouponName { get; set; }

        public decimal Discount { get; set; }

        public int ProductGiftID { get; set; }

        public short IsFreeShip { get; set; }

        public string ExtType { get; set; }

        public short Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Creator { get; set; }

        public DateTime CreateTime { get; set; }

        public string Updater { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
