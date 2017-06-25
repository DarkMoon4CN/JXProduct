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
    public class ExpressNewsMySqlBLL : IProductPlanMySqlBLL
    {
        private ExpressNewsMySqlBLL() { }
        private static ExpressNewsMySqlBLL _instance;
        private static readonly ExpressNewsMySqlDAL dal = new ExpressNewsMySqlDAL();

        public static ExpressNewsMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new ExpressNewsMySqlBLL());
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxExpressNewsID();
        }

        public bool Add(DataTable table, out int errorCount)
        {
            return dal.AddExpressNews(table, out errorCount);
        }

        public bool Update(DataTable table, out int errorCount)
        {
            return dal.UpdateExpressNews(table, out errorCount);
        }

        #endregion
    }
}
