using System;
using System.Collections.Generic;
using JXProduct.Component.Model;
using JXProduct.Component.Enums;
using System.Text;
namespace JXProduct.Component.BLL
{
    /// <summary>
    /// 商品关键字
    /// </summary>
    public class ProductKeywordBLL
    {
        private ProductKeywordBLL() { }
        private static ProductKeywordBLL _instance;
        private static readonly JXProduct.Component.SQLServerDAL.ProductKeywordDAL dal = new JXProduct.Component.SQLServerDAL.ProductKeywordDAL();

        public static ProductKeywordBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new ProductKeywordBLL());
            }
        }

        public IList<ProductKeywordInfo> ProductKeyword_Get(int productid)
        {
            return dal.ProductKeyword_Get(productid);
        }

        public bool ProductKeyword_Insert(int productid, int keywordid, int cfid, string creator)
        {
            return dal.ProductKeyword_Insert(productid, keywordid, cfid, creator);
        }

        public bool ProductKeyword_Insert(int productid, Dictionary<int, int> dic, string creator)
        {
            if (dic.Count > 0)
            {
                foreach (var d in dic)
                    this.ProductKeyword_Insert(productid, d.Key, d.Value, creator);
                return true;
            }
            return false;
        }

    }
}