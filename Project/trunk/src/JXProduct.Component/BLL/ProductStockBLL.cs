using JXProduct.Component.Model;
using JXProduct.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.BLL
{
    public class ProductStockBLL
    {
        private ProductStockBLL() { }
        private static ProductStockBLL _instance;
        public static ProductStockBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new ProductStockBLL());
            }
        }
        private static readonly ProductStockDAL dal = new ProductStockDAL();

        public List<ProductStockInfo> ProductStock_GetList(string productCode)
        {
            return dal.ProductStock_GetList(productCode);
        }

        public int ProductStock_Deal(ProductStockInfo info)
        {

            return dal.ProductStock_Deal(info);
        }
    }
}
