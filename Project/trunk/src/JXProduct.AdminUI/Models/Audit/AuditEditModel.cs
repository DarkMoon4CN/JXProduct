using JXProduct.Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXProduct.AdminUI.Models.Audit
{
    public class AuditEditModel
    {
        public string Roles { get; set; }

        public ProductInfo product { get; set; }
        public ProductAuditInfo audit { get; set; }


        //分类下Qid
        public List<QualificationCategoryInfo> Category { get; set; }

        //企业相关资质
        public List<ProductQualificationInfo> ManufacturerItems { get; set; }
        //商品相关资质
        public List<ProductQualificationInfo> ProductItems { get; set; }

        /// <summary>
        /// 商品多分类
        /// </summary>
        public List<ProductClassificationInfo> productCF { get; set; }
        /// <summary>
        /// 商品的所有的分类
        /// </summary>
        public IList<ClassificationInfo> cfList { get; set; }

        /// <summary>
        /// 商品说明书
        /// </summary>
        public ProductInstructionsInfo instructions { get; set; }

        /// <summary>
        /// 厂家信息
        /// </summary>
        public QualificationInfo Manufacturer { get; set; }
    }
}