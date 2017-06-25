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
    public class ProductStockMySqlBLL : IProductPlanMySqlBLL
    {
         #region 变量

        private static ProductStockMySqlBLL _instance;
        private static readonly ProductStockMySqlDAL dal = new ProductStockMySqlDAL();
        private static readonly object _object = new object();

        #endregion

        private ProductStockMySqlBLL() { }

        public static ProductStockMySqlBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_object)
                    {
                        if(_instance == null)
                        {
                            _instance = new ProductStockMySqlBLL();
                        }
                    }
                }
                return _instance;
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxProductStockID();
        }

        public bool Add(DataTable productTable, out int errorCount)
        {
            return dal.AddProductStock(productTable, out errorCount);
        }

        public bool Update(DataTable productTable, out int errorCount)
        {
            return dal.UpdateProductStock(productTable, out errorCount);
        }

        #endregion

        public IList<ProductOutBookMySqlInfo> ProductOutBookMySql_GetAll() 
        {
          return dal.ProductOutBookMySql_GetAll();
        }

        public bool ProductOutBookMySql_UpIsPush(int outID, int isPush) 
        {
            return dal.ProductOutBookMySql_UpIsPush(outID, isPush);
        }
    }
}
