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
    public class ActivityRuleMySqlBLL : IProductPlanMySqlBLL
    {
        private ActivityRuleMySqlBLL() { }
        private static ActivityRuleMySqlBLL _instance;
        private static readonly ActivityRuleMySqlDAL dal = new ActivityRuleMySqlDAL();

        public static ActivityRuleMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new ActivityRuleMySqlBLL());
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxActivityRuleID();
        }

        public bool Add(DataTable table, out int errorCount)
        {
            return dal.AddActivityRule(table, out errorCount);
        }

        public bool Update(DataTable table, out int errorCount)
        {
            return dal.UpdateActivityRule(table, out errorCount);
        }

        #endregion
    }
}
