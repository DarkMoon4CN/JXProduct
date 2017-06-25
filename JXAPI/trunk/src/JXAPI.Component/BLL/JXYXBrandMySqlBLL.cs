using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class JXYXBrandMySqlBLL : IProductPlanMySqlBLL
    {
        private JXYXBrandMySqlBLL() { }
        private static JXYXBrandMySqlBLL _instance;
        private static readonly JXYXBrandMySqlDAL dal = new JXYXBrandMySqlDAL();
        public static JXYXBrandMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new JXYXBrandMySqlBLL());
            }
        }

        #region CURD
        
        public int GetMaxID()
        {
            return dal.GetMaxBrandID();
        }

        public bool Add(DataTable table, out int errorCount)
        {
            return dal.AddBrand(table, out errorCount);
        }

        public bool Update(DataTable table, out int errorCount)
        {
            errorCount = 0;
            return true;
        }

        #endregion
    }
}
