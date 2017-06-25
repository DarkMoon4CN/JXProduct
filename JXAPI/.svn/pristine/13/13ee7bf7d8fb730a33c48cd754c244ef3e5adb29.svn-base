using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class JxorderProductMySqlBLL : IProductPlanMySqlBLL
    {
        private JxorderProductMySqlBLL() { }
        private static JxorderProductMySqlBLL _instance;
        private static readonly JxorderProductMySqlDAL dal = new JxorderProductMySqlDAL();
        public static JxorderProductMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new JxorderProductMySqlBLL());
            }
        }

        #region CURD
        
        public int GetMaxID()
        {
            return dal.GetMaxJxorderProductID();
        }

        public bool Add(DataTable productTable, out int errorCount)
        {
            return dal.AddJxorderProduct(productTable,out errorCount);
        }

        public bool Update(DataTable productTable, out int errorCount)
        {
            return dal.UpdateJxorderProduct(productTable, out errorCount);
        }

        #endregion
    }
}
