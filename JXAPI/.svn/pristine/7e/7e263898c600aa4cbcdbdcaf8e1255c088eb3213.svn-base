using JXAPI.Component.IBLL;
using JXAPI.Component.Model;
using JXAPI.Component.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class ClassificationMySqlBLL : IProductPlanMySqlBLL
    {
        private ClassificationMySqlBLL() { }
        private static ClassificationMySqlBLL _instance;
        private static readonly ClassificationMySqlDAL dal = new ClassificationMySqlDAL();

        public static ClassificationMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new ClassificationMySqlBLL());
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxClassificationID();
        }

        public bool Add(DataTable table, out int errorCount)
        {
            return dal.AddClassification(table, out errorCount);
        }

        public bool Update(DataTable table, out int errorCount)
        {
            return dal.UpdateClassification(table, out errorCount);
        }

        #endregion
    }
}
