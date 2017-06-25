using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXProduct.AdminUI.Models.Classification
{
    public class ClassificationEditModel
    {
        public int ParentID { get; set; }
        public int CFID { get; set; }

        public string ChineseName { get; set; }
        public string ParentChineseName { get; set; }

        public int IsKeyParent { get; set; }
    }
}