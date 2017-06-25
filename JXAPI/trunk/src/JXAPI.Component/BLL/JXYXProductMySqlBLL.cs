using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class JXYXProductMySqlBLL : IProductPlanMySqlBLL
    {
        private JXYXProductMySqlBLL() { }
        private static JXYXProductMySqlBLL _instance;
        private static readonly JXYXProductMySqlDAL dal = new JXYXProductMySqlDAL();

        public static JXYXProductMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new JXYXProductMySqlBLL());
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxJXYXProductID();
        }

        public bool Add(DataTable table, out int errorCount)
        {
            return dal.AddJXYXProduct(table, out errorCount);
        }

        public bool Update(DataTable table, out int errorCount)
        {
            return dal.UpdateJXYXProduct(table, out errorCount);
        }

        #endregion
    }
}
