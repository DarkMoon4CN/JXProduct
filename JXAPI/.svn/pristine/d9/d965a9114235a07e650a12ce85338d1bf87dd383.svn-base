using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class CouponBatchMySqlBLL : IMySqlBLL
    {
        private CouponBatchMySqlBLL() { }
        private static CouponBatchMySqlBLL _instance;
        private static readonly CouponBatchMySqlDAL dal = new CouponBatchMySqlDAL();
        public static CouponBatchMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new CouponBatchMySqlBLL());
            }
        }

        #region CURD
        
        public int GetMaxID()
        {
            return dal.GetMaxCouponBatchID();
        }

        public bool Add(DataTable table, out int errorCount)
        {
            return dal.AddCouponBatch(table, out errorCount);
        }

        public bool Update(DataTable table, out int errorCount)
        {
            return dal.UpdateCouponBatch(table, out errorCount);
        }

        #endregion
    }
}
