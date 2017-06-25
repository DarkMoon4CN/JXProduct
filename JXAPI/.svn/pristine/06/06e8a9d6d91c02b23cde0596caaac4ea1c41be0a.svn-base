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
    public class BrandMySqlBLL : IProductPlanMySqlBLL
    {
        private BrandMySqlBLL() { }
        private static BrandMySqlBLL _instance;
        private static readonly BrandMySqlDAL dal = new BrandMySqlDAL();

        public static BrandMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new BrandMySqlBLL());
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
            return dal.UpdateBrand(table, out errorCount);
        }

        #endregion
    }
}
