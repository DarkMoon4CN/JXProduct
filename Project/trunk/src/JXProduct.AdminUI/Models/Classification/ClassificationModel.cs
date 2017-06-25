using JXProduct.Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXProduct.AdminUI.Models.Classification
{
    public class ClassificationModel
    {
        public List<ClassificationInfo> CFList { get; set; }
        public List<SectionInfo> SectionList { get; set; }
    }
}