using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class OrderLogisticsMySqlBLL : IMySqlBLL
    {
        #region 变量

        private static OrderLogisticsMySqlBLL _instance;
        private static readonly OrderLogisticsMySqlDAL dal = new OrderLogisticsMySqlDAL();
        private static readonly object _object = new object();

        #endregion

        private OrderLogisticsMySqlBLL() { }

        public static OrderLogisticsMySqlBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_object)
                    {
                        if(_instance == null)
                        {
                            _instance = new OrderLogisticsMySqlBLL();
                        }
                    }
                }
                return _instance;
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxOrderLogisticsID();
        }

        public bool Update(System.Data.DataTable table, out int errorCount)
        {
            return dal.UpdateOrderLogistics(table, out errorCount);
        }

        public bool Add(System.Data.DataTable table, out int errorCount)
        {
            return dal.AddOrderLogistics(table, out errorCount);
        }
        
        #endregion
    }
}
