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
    public class CouponMySqlBLL : IMySqlBLL
    {
        private CouponMySqlBLL() { }
        private static CouponMySqlBLL _instance;
        private static readonly CouponMySqlDAL dal = new CouponMySqlDAL();
        public static CouponMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new CouponMySqlBLL());
            }
        }

        #region CURD
        
        public int GetMaxID()
        {
            return dal.GetMaxCouponID();
        }

        public bool Add(DataTable table, out int errorCount)
        {
            return dal.AddCoupon(table, out errorCount);
        }

        public bool Update(DataTable table, out int errorCount)
        {
            return dal.UpdateCoupon(table, out errorCount);
        }


        #endregion

        #region lm for push
        /// <summary>
        /// 获取将要过期的优惠券信息
        /// </summary>
        /// <param name="numberHour">时间</param>
        /// <returns></returns>
        public IList<CouponMySqlInfo> CouponMySql_GetAll(int numberHour) 
        {
            return dal.CouponMySql_GetAll(numberHour);
        }
        #endregion

    }
}
