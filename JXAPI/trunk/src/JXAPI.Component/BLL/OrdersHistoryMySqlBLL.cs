using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class OrdersHistoryMySqlBLL : IMySqlBLL
    {
        #region 变量

        private static OrdersHistoryMySqlBLL _instance;
        private static readonly OrdersHistoryMySqlDAL dal = new OrdersHistoryMySqlDAL();
        private static readonly object _object = new object();

        #endregion

        private OrdersHistoryMySqlBLL() { }

        public static OrdersHistoryMySqlBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_object)
                    {
                        if(_instance == null)
                        {
                            _instance = new OrdersHistoryMySqlBLL();
                        }
                    }
                }
                return _instance;
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxOrdersHistoryId();
        }

        public bool Update(System.Data.DataTable table, out int errorCount)
        {
            return dal.UpdateOrdersHistory(table, out errorCount);
        }

        public bool Add(System.Data.DataTable table, out int errorCount)
        {
            return dal.AddOrdersHistory(table, out errorCount);
        }
        
        #endregion
    }
}
