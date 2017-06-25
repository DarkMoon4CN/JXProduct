using JXAPI.Common;
using JXAPI.Component.IBLL;
using JXAPI.Component.Model;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class ProductMySqlBLL
    {
        private ProductMySqlBLL() { }
        private static ProductMySqlBLL _instance;
        private ProductMySqlDAL dal = new ProductMySqlDAL();
        public static ProductMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new ProductMySqlBLL());
            }
        }

        #region CURD

        #endregion

        public IList<ProductFavoriteMySqlInfo> ProductMySql_GetAll()
        {
            return dal.ProductMySql_GetAll();
        }
        public bool ProductMySql_UpIsPush(int productID, int userID, int isPush)
        {
            return dal.ProductMySql_UpIsPush(productID, userID, isPush);
        }
    }
}
