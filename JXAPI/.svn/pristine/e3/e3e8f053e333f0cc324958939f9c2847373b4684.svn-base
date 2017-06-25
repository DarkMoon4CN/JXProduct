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
    public class ProductClassificationMySqlBLL : IProductPlanMySqlBLL
    {
        #region 变量

        private static ProductClassificationMySqlBLL _instance;
        private static readonly ProductClassificationMySqlDAL dal = new ProductClassificationMySqlDAL();
        private static readonly object _object = new object();

        #endregion

        private ProductClassificationMySqlBLL() { }

        public static ProductClassificationMySqlBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_object)
                    {
                        if(_instance == null)
                        {
                            _instance = new ProductClassificationMySqlBLL();
                        }
                    }
                }
                return _instance;
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxProductClassificationID();
        }

        public bool Add(DataTable productTable, out int errorCount)
        {
            return dal.AddProductClassification(productTable, out errorCount);
        }

        public bool Update(DataTable productTable, out int errorCount)
        {
            return dal.UpdateProductClassification(productTable, out errorCount);
        }

        #endregion
    }
}
