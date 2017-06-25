using JXProduct.Component.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXProduct.AdminUI.Models.Product
{
    public class RelatedModel
    {
        /// <summary>
        /// 当前的类别
        /// </summary>
        public int RelatedType { get; set; }


        public int ProductID { get; set; }
        /// <summary>
        ///  商品信息
        /// </summary>
        public ProductInfo Product { get; set; }

        /// <summary>
        /// 如果当前商品大包装,提供主商品信息
        /// </summary>
        public ProductInfo BaseProduct { get; set; }
        /// <summary>
        /// 关联商品的数据
        /// </summary>
        public List<ProductRelatedInfo> relateds { get; set; }
    }
}