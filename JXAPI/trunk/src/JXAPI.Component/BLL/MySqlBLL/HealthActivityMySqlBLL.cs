using JXAPI.Common;
using JXAPI.Component.Model;
using JXAPI.Component.SQLServerDAL.MySqlDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL.MySqlBLL
{
    public class HealthActivityMySqlBLL
    {
        private HealthActivityMySqlBLL() { }
        private JXHealthActivityMySqlDAL dal = new JXHealthActivityMySqlDAL();
        public static HealthActivityMySqlBLL Instance
        {
            get
            {
                return new HealthActivityMySqlBLL();
            }
        }

        #region CURD

        public OperationResult<bool> UpdateHealthActivity(string createTime)
        {
            return dal.UpdateHealthActivity(createTime); 
        }

        public IList<ProductFavoriteMySqlInfo> ProductMySql_GetAll()
        {
            return dal.ProductMySql_GetAll();
        }

        #endregion

    }
}
