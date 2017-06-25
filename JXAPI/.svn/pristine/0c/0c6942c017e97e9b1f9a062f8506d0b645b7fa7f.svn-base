using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class CouponShoppingCardMySqlBLL : IMySqlBLL
    {
        private CouponShoppingCardMySqlBLL() { }
        private static CouponShoppingCardMySqlBLL _instance;
        private static readonly CouponShoppingCardMySqlDAL dal = new CouponShoppingCardMySqlDAL();
        public static CouponShoppingCardMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new CouponShoppingCardMySqlBLL());
            }
        }

        #region CURD
        
        public int GetMaxID()
        {
            return dal.GetMaxCouponShoppingCardID();
        }

        public bool Add(DataTable table, out int errorCount)
        {
            return dal.AddCouponShoppingCard(table, out errorCount);
        }

        public bool Update(DataTable table, out int errorCount)
        {
            return dal.UpdateCouponShoppingCard(table, out errorCount);
        }

        #endregion
    }
}
