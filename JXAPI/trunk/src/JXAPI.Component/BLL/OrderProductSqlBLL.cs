using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class OrderProductSqlBLL : IOrdersRelatedMySqlBLL
    {
        #region 变量

        private static OrderProductSqlBLL _instance;
        private static readonly OrderProductSqlDAL dal = new OrderProductSqlDAL();
        private static readonly object _object = new object();

        #endregion

        private OrderProductSqlBLL() { }

        public static OrderProductSqlBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_object)
                    {
                        if(_instance == null)
                        {
                            _instance = new OrderProductSqlBLL();
                        }
                    }
                }
                return _instance;
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxOrderProductID();
        }

        public bool Update(System.Data.DataTable table, out int errorCount)
        {
            return dal.UpdateOrderProduct(table, out errorCount);
        }

        public bool Add(System.Data.DataTable table, out int errorCount)
        {
            return dal.AddOrderProduct(table, out errorCount);
        }
        
        #endregion
    }
}
