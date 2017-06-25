using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class OrderProductHistoryMySqlBLL : IMySqlBLL
    {
        #region 变量
        private static OrderProductHistoryMySqlBLL _instance;
        private static readonly OrderProductHistoryMySqlDAL dal = new OrderProductHistoryMySqlDAL();
        private static readonly object _object = new object();
        #endregion

        private OrderProductHistoryMySqlBLL() { }

        public static OrderProductHistoryMySqlBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_object)
                    {
                        if(_instance == null)
                        {
                            _instance = new OrderProductHistoryMySqlBLL();
                        }
                    }
                }
                return _instance;
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxOrderProductHistoryID();
        }

        public bool Update(System.Data.DataTable table, out int errorCount)
        {
            throw new NotImplementedException();
        }

        public bool Add(System.Data.DataTable table, out int errorCount)
        {
            return dal.AddOrderProductHistory(table, out errorCount);
        }
        
        #endregion
    }
}
