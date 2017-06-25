using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class CouponChannelMySqlBLL : IMySqlBLL
    {
        private CouponChannelMySqlBLL() { }
        private static CouponChannelMySqlBLL _instance;
        private static readonly CouponChannelMySqlDAL dal = new CouponChannelMySqlDAL();
        public static CouponChannelMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new CouponChannelMySqlBLL());
            }
        }

        #region CURD
        
        public int GetMaxID()
        {
            return dal.GetMaxCouponChannelID();
        }

        public bool Add(DataTable table, out int errorCount)
        {
            return dal.AddCouponChannel(table, out errorCount);
        }

        public bool Update(DataTable table, out int errorCount)
        {
            return dal.UpdateCouponChannel(table, out errorCount);
        }

        #endregion
    }
}
