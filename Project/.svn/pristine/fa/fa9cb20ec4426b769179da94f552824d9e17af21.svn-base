using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JXProduct.Component.Model;
using JXProduct.Component.Enums;

namespace JXProduct.AdminUI.Models.Audit
{
    public class AuditEditPartialModel
    {
        public bool IsEdit { get; set; }
        public int MID { get; set; }
        public int PID { get; set; }

        public int Type { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public int CID { get; set; }
        public string CName
        {
            get
            {
                return JXUtil.EnumHelper.GetText<ParameterType>(this.CID);
            }
        }
        public List<ProductQualificationInfo> ManufacturerItems { get; set; }
        public List<ProductQualificationInfo> ProductItems { get; set; }
    }
}