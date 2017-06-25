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
    public class ProductParameterPriceMySqlBLL : IProductPlanMySqlBLL
    {
        #region 变量

        private static ProductParameterPriceMySqlBLL _instance;
        private static readonly ProductParameterPriceMySqlDAL dal = new ProductParameterPriceMySqlDAL();
        private static readonly object _object = new object();

        #endregion

        private ProductParameterPriceMySqlBLL() { }

        public static ProductParameterPriceMySqlBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_object)
                    {
                        if(_instance == null)
                        {
                            _instance = new ProductParameterPriceMySqlBLL();
                        }
                    }
                }
                return _instance;
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxProductParameterPriceID();
        }

        public bool Add(DataTable productTable, out int errorCount)
        {
            return dal.AddProductParameterPrice(productTable, out errorCount);
        }

        public bool Update(DataTable productTable, out int errorCount)
        {
            return dal.UpdateProductParameterPrice(productTable, out errorCount);
        }

        #endregion
    }
}
