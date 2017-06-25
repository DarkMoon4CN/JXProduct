using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class OrderProductMySqlBLL : IMySqlBLL
    {
        #region 变量

        private static OrderProductMySqlBLL _instance;
        private static readonly OrderProductMySqlDAL dal = new OrderProductMySqlDAL();
        private static readonly object _object = new object();

        #endregion

        private OrderProductMySqlBLL() { }

        public static OrderProductMySqlBLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_object)
                    {
                        if(_instance == null)
                        {
                            _instance = new OrderProductMySqlBLL();
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
