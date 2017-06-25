using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class OrderOperateLogSqlBLL : IOrdersRelatedMySqlBLL
    {
        #region 变量

        private static OrderOperateLogSqlBLL _instance;
        private static readonly OrderOperateLogSqlDAL dal = new OrderOperateLogSqlDAL();
        private static readonly object _object = new object();

        #endregion

        private OrderOperateLogSqlBLL() { }

        public static OrderOperateLogSqlBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_object)
                    {
                        if(_instance == null)
                        {
                            _instance = new OrderOperateLogSqlBLL();
                        }
                    }
                }
                return _instance;
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxOrderOperateLogID();
        }

        public bool Update(System.Data.DataTable table, out int errorCount)
        {
            return dal.UpdateOrderOperateLog(table, out errorCount);
        }

        public bool Add(System.Data.DataTable table, out int errorCount)
        {
            return dal.AddOrderOperateLog(table, out errorCount);
        }
        
        #endregion
    }
}
