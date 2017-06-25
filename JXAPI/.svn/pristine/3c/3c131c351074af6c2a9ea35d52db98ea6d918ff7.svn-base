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
    public class ProductAllMySqlBLL : IProductPlanMySqlBLL
    {
        private ProductAllMySqlBLL() { }
        private static ProductAllMySqlBLL _instance;
        private static readonly ProductAllMySqlDAL dal = new ProductAllMySqlDAL();

        public static ProductAllMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new ProductAllMySqlBLL());
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxProductAllID();
        }

        public bool Add(DataTable table, out int errorCount)
        {
            return dal.AddProductAll(table, out errorCount);
        }

        public bool Update(DataTable table, out int errorCount)
        {
            return dal.UpdateProductAll(table, out errorCount);
        }

        #endregion
    }
}
