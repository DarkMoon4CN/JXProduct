using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class productevaluationMySqlBLL : IProductPlanMySqlBLL
    {
        private productevaluationMySqlBLL() { }
        private static productevaluationMySqlBLL _instance;
        private static readonly productevaluationMySqlDAL dal = new productevaluationMySqlDAL();
        public static productevaluationMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new productevaluationMySqlBLL());
            }
        }

        #region CURD
        
        public int GetMaxID()
        {
            return dal.GetMaxProductevaluationID();
        }

        public bool Add(DataTable productTable, out int errorCount)
        {
            return dal.AddProductevaluation(productTable,out errorCount);
        }

        public bool Update(DataTable productTable, out int errorCount)
        {
            return dal.UpdateProductevaluation(productTable, out errorCount);
        }

        public bool UpdateEvaluationPraiseRate()
        {
            return dal.UpdateEvaluationPraiseRate();
        }

        public bool UpdateEvaluationCount()
        {
            return dal.UpdateEvaluationCount();
        }

        #endregion
    }
}
