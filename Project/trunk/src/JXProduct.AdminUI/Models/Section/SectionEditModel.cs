using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXProduct.AdminUI.Models.Section
{
    public class SectionEditModel
    {

        public int ParentID { get; set; }
        public int SectionID { get; set; }

        public string ChineseName { get; set; }
        public string ParentChineseName { get; set; }
        public int IsKeyParent { get; set; }

    }
}