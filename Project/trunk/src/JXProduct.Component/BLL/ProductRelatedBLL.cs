using JXProduct.Component.Enums;
using JXProduct.Component.Model;
using JXProduct.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.BLL
{
    public class ProductRelatedBLL
    {
        private ProductRelatedBLL() { }
        private static ProductRelatedBLL _instance;
        private static readonly ProductRelatedDAL dal = new ProductRelatedDAL();
        public static ProductRelatedBLL Instance
        {
            get { return _instance ?? (_instance = new ProductRelatedBLL()); }
        }

        #region Get

        public ProductRelatedInfo ProductRelated_Get(int productID, RelatedType t)
        {
            return dal.ProductRelated_GetByChildID(productID, t);
        }

        #endregion

        #region GetList + Delete

        public List<ProductRelatedInfo> ProductRelated_GetList(int productid, RelatedType t)
        {
            return dal.ProductRelated_GetList(productid, t);
        }

        /// <summary>
        /// 删除
        /// 1.正常
        /// 2.不允许删除.因为主商品下还有子商品
        /// </summary>
        public bool ProductRelated_Delete(int relatedID)
        {
            return dal.ProductRelated_Delete(relatedID);
        }

        #endregion

        #region Insert  重载

        /// <summary>
        /// 大包装生成
        /// </summary>
        public bool ProductRelated_SavePackaging(int relatedid, int productid, int quantity, decimal price, string name, string creator)
        {
            if ((relatedid > 0 || (productid > 0 && quantity > 1)) && price > 0)
            {
                return dal.ProductRelated_SavePackaging(relatedid, productid, quantity, price, name, creator);
            }
            return false;
        }

        /// <summary>
        /// 最佳推荐 Type=1
        /// </summary>
        public bool ProductRelated_SaveBestRecommend(int productid, string childProductIDs, string creator)
        {
            var list = childProductIDs.Split(',').ToList().ConvertAll(t =>
            {
                int id = 0;
                int.TryParse(t, out id);
                return id;
            });
            return dal.ProductRelated_SaveBestRecommend(productid, string.Join(",", list), creator);
        }
        /// <summary>
        /// 添加不同规格  Type=2
        /// </summary>
        public int ProductRelated_SaveDifferentSpec(List<int> productIDs, string creator)
        {
            if (productIDs != null && productIDs.Count > 0)
            {
                //调整顺序. 1，2，3，4
                //保证使用最前面的作为规格的组ID
                productIDs = productIDs.OrderBy(t => t).ToList();
                return dal.ProductRelated_SaveDifferentSpec(productIDs, creator);
            }
            return 0;
        }

        #endregion
    }
}
