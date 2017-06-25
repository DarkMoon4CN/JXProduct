using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class CouponTypeMySqlBLL : IMySqlBLL
    {
        private CouponTypeMySqlBLL() { }
        private static CouponTypeMySqlBLL _instance;
        private static readonly CouponTypeMySqlDAL dal = new CouponTypeMySqlDAL();
        public static CouponTypeMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new CouponTypeMySqlBLL());
            }
        }

        #region CURD
        
        public int GetMaxID()
        {
            return dal.GetMaxCouponTypeID();
        }

        public bool Add(DataTable table, out int errorCount)
        {
            return dal.AddCouponType(table, out errorCount);
        }

        public bool Update(DataTable table, out int errorCount)
        {
            return dal.UpdateCouponType(table, out errorCount);
        }

        #endregion
    }
}
