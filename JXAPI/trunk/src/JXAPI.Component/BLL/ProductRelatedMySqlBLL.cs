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
    public class ProductRelatedMySqlBLL : IProductPlanMySqlBLL
    {
        #region 变量

        private static ProductRelatedMySqlBLL _instance;
        private static readonly ProductRelatedMySqlDAL dal = new ProductRelatedMySqlDAL();
        private static readonly object _object = new object();

        #endregion

        private ProductRelatedMySqlBLL() { }

        public static ProductRelatedMySqlBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_object)
                    {
                        if(_instance == null)
                        {
                            _instance = new ProductRelatedMySqlBLL();
                        }
                    }
                }
                return _instance;
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxProductRelatedID();
        }

        public bool Add(DataTable productTable, out int errorCount)
        {
            return dal.AddProductRelated(productTable, out errorCount);
        }

        public bool Update(DataTable productTable, out int errorCount)
        {
            return dal.UpdateProductRelated(productTable, out errorCount);
        }

        #endregion
    }
}
