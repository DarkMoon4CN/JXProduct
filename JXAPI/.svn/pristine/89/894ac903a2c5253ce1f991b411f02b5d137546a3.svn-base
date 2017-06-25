using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class CouponShoppingCardDetailMySqlBLL : IMySqlBLL
    {
        private CouponShoppingCardDetailMySqlBLL() { }
        private static CouponShoppingCardDetailMySqlBLL _instance;
        private static readonly CouponShoppingCardDetailMySqlDAL dal = new CouponShoppingCardDetailMySqlDAL();
        public static CouponShoppingCardDetailMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new CouponShoppingCardDetailMySqlBLL());
            }
        }

        #region CURD
        
        public int GetMaxID()
        {
            return dal.GetMaxCouponShoppingCardDetailID();
        }

        public bool Add(DataTable table, out int errorCount)
        {
            return dal.AddCouponShoppingCardDetail(table, out errorCount);
        }

        public bool Update(DataTable table, out int errorCount)
        {
            return dal.UpdateCouponShoppingCardDetail(table, out errorCount);
        }

        #endregion
    }
}
