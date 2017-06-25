using JXProduct.Component.Model;
using System.Collections.Generic;

namespace JXProduct.Component.BLL
{
    /// <summary>
    /// ProductGift
    /// </summary>
    public class ProductGiftBLL
    {
        private ProductGiftBLL()
        {
        }

        private static ProductGiftBLL _instance;

        private static readonly JXProduct.Component.SQLServerDAL.ProductGiftDAL dal = new JXProduct.Component.SQLServerDAL.ProductGiftDAL();

        public static ProductGiftBLL Instance { get { return _instance ?? (_instance = new ProductGiftBLL()); } }

        /// <summary>
        /// 保存赠品信息
        /// </summary>
        /// <param name="productgiftinfo"></param>
        public bool ProductGift_Save(ProductGiftInfo productgiftinfo)
        {
            return dal.ProductGift_Save(productgiftinfo);
        }

        /// <summary>
        /// 批量查询赠品信息
        /// </summary>
        /// <param name="productIDs"></param>
        public List<ProductGiftInfo> ProductGift_GetList(List<int> productIDs)
        {
            if (productIDs != null && productIDs.Count > 0)
            {
                var IDs = string.Join(",", productIDs);
                return dal.ProductGift_GetList(IDs);
            }
            return new List<ProductGiftInfo>();
        }
    }
}