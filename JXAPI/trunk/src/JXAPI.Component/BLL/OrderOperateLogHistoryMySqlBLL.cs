using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class OrderOperateLogHistoryMySqlBLL : IMySqlBLL
    {
        #region 变量
        private static OrderOperateLogHistoryMySqlBLL _instance;
        private static readonly OrderOperateLogHistoryMySqlDAL dal = new OrderOperateLogHistoryMySqlDAL();
        private static readonly object _object = new object();
        #endregion

        private OrderOperateLogHistoryMySqlBLL() { }

        public static OrderOperateLogHistoryMySqlBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_object)
                    {
                        if(_instance == null)
                        {
                            _instance = new OrderOperateLogHistoryMySqlBLL();
                        }
                    }
                }
                return _instance;
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxOrderOperateLogHistoryId();
        }

        public bool Update(System.Data.DataTable table, out int errorCount)
        {
            throw new NotImplementedException();
        }

        public bool Add(System.Data.DataTable table, out int errorCount)
        {
            return dal.AddOrderOperateLogHistory(table, out errorCount);
        }
        
        #endregion
    }
}
