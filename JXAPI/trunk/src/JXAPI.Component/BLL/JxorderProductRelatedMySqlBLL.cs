using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class JxorderProductRelatedMySqlBLL : IProductPlanMySqlBLL
    {
        private JxorderProductRelatedMySqlBLL() { }
        private static JxorderProductRelatedMySqlBLL _instance;
        private static readonly JxorderProductRelatedMySqlDAL dal = new JxorderProductRelatedMySqlDAL();
        public static JxorderProductRelatedMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new JxorderProductRelatedMySqlBLL());
            }
        }

        #region CURD
        
        public int GetMaxID()
        {
            return dal.GetMaxJxorderProductRelatedID();
        }

        public bool Add(DataTable productTable, out int errorCount)
        {
            return dal.AddJxorderProductRelated(productTable,out errorCount);
        }

        public bool Update(DataTable productTable, out int errorCount)
        {
            return dal.UpdateJxorderProductRelated(productTable, out errorCount);
        }

        #endregion
    }
}
