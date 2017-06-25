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
    public class SaleMySqlBLL : IProductPlanMySqlBLL
    {
        private SaleMySqlBLL() { }
        private static SaleMySqlBLL _instance;
        private static readonly SaleMySqlDAL dal = new SaleMySqlDAL();

        public static SaleMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new SaleMySqlBLL());
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxSaleID();
        }

        public bool Add(DataTable table, out int errorCount)
        {
            return dal.AddSale(table, out errorCount);
        }

        public bool Update(DataTable table, out int errorCount)
        {
            return dal.UpdateSale(table, out errorCount);
        }

        #endregion
    }
}
