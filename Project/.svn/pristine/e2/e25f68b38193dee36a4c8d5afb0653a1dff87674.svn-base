using System;
using System.Collections.Generic;
using JXProduct.Component.Model;
using System.Linq;
using System.Text;
namespace JXProduct.Component.BLL
{
    /// <summary>
    /// 组合
    /// </summary>
    public class ProductComboBLL
    {
        private ProductComboBLL() { }
        private static ProductComboBLL _instance;
        public static ProductComboBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new ProductComboBLL());
            }
        }

        private static readonly JXProduct.Component.SQLServerDAL.ProductComboDAL dal = new JXProduct.Component.SQLServerDAL.ProductComboDAL();

        #region CURD

        public int ProductCombo_Insert(int mainproductid, string comboname, string creator, List<ProductComboInfo> combolist)
        {
            if (combolist == null || combolist.Count == 0 || mainproductid == 0)
                return 0;
            return dal.ProductCombo_Insert(mainproductid, comboname, creator, combolist);

        }

        public bool ProductCombo_Update(int productid, string comboname, string creator, List<ProductComboInfo> combolist)
        {
            if (combolist == null || combolist.Count == 0 || productid == 0)
                return false;
            return dal.ProductCombo_Update(productid, comboname, creator, combolist);

        }


        //删除组合中某个
        public bool ProductCombo_Delete(int productID)
        {
            return dal.ProductCombo_Delete(productID);
        }

        //获取商品组合的 子商品信息
        public List<ProductComboInfo> ProductCombo_GetByProductIDs(List<int> productIDs)
        {
            if (productIDs != null && productIDs.Count > 0)
                return dal.ProductCombo_GetByProductIDs(string.Join(",", productIDs));
            return new List<ProductComboInfo>();
        }

        public List<int> ProductCombo_Get(int childproductid)
        {
            return dal.ProductCombo_Get(childproductid);
        }

        #endregion


        #region ProductCombo_GetProductList (productIDs)

        /// <summary>
        /// 传递组合商品的ProductIDs
        /// 根据返回数据,组合出集合数据
        /// </summary>
        /// <param name="productIDs"></param>
        public Dictionary<int, List<ProductInfo>> ProductCombo_GetProductList(List<int> productIDs)
        {
            int recordCount = 0;
            var dic = new Dictionary<int, List<ProductInfo>>();

            //获取商品ID相关的所有组合商品信息
            var combolist = this.ProductCombo_GetByProductIDs(productIDs);
            if (combolist.Count == 0)
                return dic;

            //提取商品ID,获取对应的ProductInfo
            var comboidsStr = string.Join(",", combolist.Select(t => t.ComboProductID));
            var productlist = ProductBLL.Instance.Product_GetList(1, 0, "", "ProductID IN(" + comboidsStr + ")", out recordCount);

            // 合并  combo productlist 
            // 分组 
            // 得到对应的key value值
            var newresult = from t in combolist
                            join p in productlist on t.ComboProductID equals p.ProductID
                            select new
                            {
                                t.ProductID,
                                p
                            };
            var result = newresult.GroupBy(t => t.ProductID, t => t.p).ToDictionary(t => t.Key, t => t.ToList());
            return result;
        }

        #endregion
    }
}