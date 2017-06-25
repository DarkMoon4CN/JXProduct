using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.Model
{
    public class ProductRelatedInfo
    {
        public int RelatedID { get; set; }

        public int ProductID { get; set; }

        public int ChildProductID { get; set; }

        public string Name { get; set; }

        public short Type { get; set; }

        public string Creator { get; set; }

        public DateTime CreateTime { get; set; }

        public string Updater { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
