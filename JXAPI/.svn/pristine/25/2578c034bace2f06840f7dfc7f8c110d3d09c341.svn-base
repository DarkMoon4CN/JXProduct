using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class ProductQuestionMySqlBLL : IProductPlanMySqlBLL
    {
        private ProductQuestionMySqlBLL() { }
        private static ProductQuestionMySqlBLL _instance;
        private static readonly ProductQuestionMySqlDAL dal = new ProductQuestionMySqlDAL();
        public static ProductQuestionMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new ProductQuestionMySqlBLL());
            }
        }

        #region CURD
        
        public int GetMaxID()
        {
            return dal.GetMaxProductQuestionID();
        }

        public bool Add(DataTable productTable, out int errorCount)
        {
            return dal.AddProductQuestion(productTable,out errorCount);
        }

        public bool Update(DataTable productTable, out int errorCount)
        {
            return dal.UpdateProductQuestion(productTable, out errorCount);
        }

        #endregion
    }
}
