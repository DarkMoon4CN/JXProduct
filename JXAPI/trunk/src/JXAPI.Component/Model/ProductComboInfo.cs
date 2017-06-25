using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{
    public class ProductComboInfo
    {
        public int ProductID { get; set; }

        public int ComboProductID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public short IsMain { get; set; }

        public string Creator { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
