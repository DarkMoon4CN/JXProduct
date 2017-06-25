using JXAPI.Component.IBLL;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class ClassificationParameterToPriceMySqlBLL : IProductPlanMySqlBLL
    {
        private ClassificationParameterToPriceMySqlBLL() { }
        private static ClassificationParameterToPriceMySqlBLL _instance;
        private static readonly ClassificationParameterToPriceMySqlDAL dal = new ClassificationParameterToPriceMySqlDAL();
        public static ClassificationParameterToPriceMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new ClassificationParameterToPriceMySqlBLL());
            }
        }

        #region CURD
        
        public int GetMaxID()
        {
            return dal.GetMaxClassificationParameterToPriceID();
        }

        public bool Add(DataTable productTable, out int errorCount)
        {
            return dal.AddClassificationParameterToPrice(productTable,out errorCount);
        }

        public bool Update(DataTable productTable, out int errorCount)
        {
            return dal.UpdateClassificationParameterToPrice(productTable, out errorCount);
        }

        #endregion
    }
}
