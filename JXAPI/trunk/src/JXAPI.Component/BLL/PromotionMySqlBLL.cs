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
    public class PromotionMySqlBLL : IProductPlanMySqlBLL
    {
        private PromotionMySqlBLL() { }
        private static PromotionMySqlBLL _instance;
        private static readonly PromotionMySqlDAL dal = new PromotionMySqlDAL();

        public static PromotionMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new PromotionMySqlBLL());
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxPromotionID();
        }

        public bool Add(DataTable table, out int errorCount)
        {
            return dal.AddPromotion(table, out errorCount);
        }

        public bool Update(DataTable table, out int errorCount)
        {
            return dal.UpdatePromotion(table, out errorCount);
        }

        #endregion
    }
}
