using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{
    public class ProductParameterPriceInfo
    {
        public long ParaPriceID { get; set; }

        public int MainProductID { get; set; }

        public int ChildProductID { get; set; }

        public string Prop1 { get; set; }

        public string Prop2 { get; set; }

        public string Prop3 { get; set; }

        public DateTime UpdateTime { get; set; }
             
    }
}
