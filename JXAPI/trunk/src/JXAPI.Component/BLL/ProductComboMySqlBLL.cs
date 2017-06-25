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
    public class ProductComboMySqlBLL : IProductPlanMySqlBLL
    {
        #region 变量

        private static ProductComboMySqlBLL _instance;
        private static readonly ProductComboMySqlDAL dal = new ProductComboMySqlDAL();
        private static readonly object _object = new object();

        #endregion

        private ProductComboMySqlBLL() { }

        public static ProductComboMySqlBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_object)
                    {
                        if(_instance == null)
                        {
                            _instance = new ProductComboMySqlBLL();
                        }
                    }
                }
                return _instance;
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxProductComboID();
        }

        public bool Add(DataTable productTable, out int errorCount)
        {
            return dal.AddProductCombo(productTable, out errorCount);
        }

        public bool Update(DataTable productTable, out int errorCount)
        {
            return dal.UpdateProductCombo(productTable, out errorCount);
        }

        #endregion
    }
}
