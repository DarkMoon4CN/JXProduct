using System;
using System.Collections.Generic;
using JXProduct.Component.Model;
namespace JXProduct.Component.BLL
{
    /// <summary>
    /// ProductParameterPrice 
    /// </summary>
    public class ProductParameterPriceBLL
    {
        private ProductParameterPriceBLL() { }
        private static ProductParameterPriceBLL _instance;
        public static ProductParameterPriceBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProductParameterPriceBLL();
                }
                return _instance;
            }
        }

        private static readonly JXProduct.Component.SQLServerDAL.ProductParameterPriceDAL dal = new JXProduct.Component.SQLServerDAL.ProductParameterPriceDAL();

        public int ProductParameterPrice_Insert(ProductParameterPriceInfo productparameterpriceinfo)
        {
            return dal.ProductParameterPrice_Insert(productparameterpriceinfo);
        }


        public bool ProductParameterPrice_Delete(int parapriceID)
        {
            return dal.ProductParameterPrice_Delete(parapriceID);
        }

        public ProductParameterPriceInfo ProductParameterPrice_Get(ProductParameterPriceInfo info)
        {
            return dal.ProductParameterPrice_Get(info);
        }

        public bool ProductParameterPrice_Update(int parapriceID, int childproductid)
        {
            return dal.ProductParameterPrice_Update(parapriceID, childproductid);
        }


        public List<ProductParameterPriceInfo> ProductParameterPrice_GetList(int productid, int pageindex, int pagesize, out int recordCount)
        {
            return dal.ProductParameterPrice_GetList(productid, pageindex, pagesize, out recordCount);
        }

        public List<ProductPropValueInfo> ProductPropValue_GetGroupList(int productid)
        {
            return dal.ProductPropValue_GetGroupList(productid);
        }

        public bool ProductPropValue_Save(ProductPropValueInfo info)
        {
            return dal.ProductPropValue_Save(info);
        }
    }
}
