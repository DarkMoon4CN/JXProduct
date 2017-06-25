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
    public class ActivityMySqlBLL : IProductPlanMySqlBLL
    {
        private ActivityMySqlBLL() { }
        private static ActivityMySqlBLL _instance;
        private static readonly ActivityMySqlDAL dal = new ActivityMySqlDAL();

        public static ActivityMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new ActivityMySqlBLL());
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxActivityID();
        }

        public bool Add(DataTable table, out int errorCount)
        {
            return dal.AddActivity(table, out errorCount);
        }

        public bool Update(DataTable table, out int errorCount)
        {
            return dal.UpdateActivity(table, out errorCount);
        }

        public bool UpdateHealthActivity(string createTime)
        {
            return dal.UpdateHealthActivity(createTime);
        }

        #endregion
    }
}
