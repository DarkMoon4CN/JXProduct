using JXProduct.Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JXProduct.AdminUI.Models.Product
{
    public class ProductEditModel
    {
        public string Roles { get; set; }
        public ProductInfo Product { get; set; }
        public List<KeywordInfo> keywords { get; set; }
        public ProductInstructionsInfo Instructions { get; set; }
        public IEnumerable<SelectListItem> Brands { get; set; }
        public IEnumerable<SelectListItem> Suppliers { get; set; }


        public List<ProductAuditInfo> Audit { get; set; }
        public List<ProductPropValueInfo> relateds { get; set; }

        public ManufacturerInfo Manufacturer { get; set; }
        public Dictionary<int, string> DescImages { get; set; }
        /// <summary>
        /// 当前是否具有审核权限
        /// </summary>
        public bool IsAudit { get; set; }

        //单位列表
        public List<SelectListItem> UnitList
        {
            get
            {
                string[] arr = "袋,盒,包,瓶,条,个,套,支,台,克,罐,把,片,辆,张,付,块,件,双,辆,只".Split(',');
                var list = new List<SelectListItem>();
                foreach (var a in arr)
                {
                    list.Add(new SelectListItem() { Text = a, Value = a });
                }
                return list;
            }
        }
    }
}