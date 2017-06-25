using JXAPI.Component.IBLL;
using JXAPI.Component.Model;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class OrdersMySqlBLL : IMySqlBLL
    {
        #region 变量

        private static OrdersMySqlBLL _instance;
        private static readonly OrdersMySqlDAL dal = new OrdersMySqlDAL();
        private static readonly object _object = new object();

        #endregion

        private OrdersMySqlBLL() { }

        public static OrdersMySqlBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_object)
                    {
                        if(_instance == null)
                        {
                            _instance = new OrdersMySqlBLL();
                        }
                    }
                }
                return _instance;
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxOrdersId();
        }
        
        public bool Update(System.Data.DataTable table, out int errorCount)
        {
            return dal.UpdateOrders(table, out errorCount);
        }
        
        public bool Add(System.Data.DataTable table, out int errorCount)
        {
            return dal.AddOrders(table, out errorCount);
        }
        
        #endregion


        /// <summary>
        /// 查询出所有将要超时订单 
        /// </summary>
        /// <param name="numberHour">订单超时时间</param>
        /// <returns></returns>
        public IList<OrdersInfo> OrdersTimeOutMySql_GetAll(int numberHour) 
        {
            return dal.OrdersTimeOutMySql_GetAll(numberHour);
        }
    }
}
