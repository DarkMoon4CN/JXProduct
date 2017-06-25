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
    public class KeywordProductMySqlBLL : IProductPlanMySqlBLL
    {
        private KeywordProductMySqlBLL() { }
        private static KeywordProductMySqlBLL _instance;
        private static readonly KeywordProductMySqlDAL dal = new KeywordProductMySqlDAL();

        public static KeywordProductMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new KeywordProductMySqlBLL());
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxKeywordProductID();
        }

        public bool Add(DataTable table, out int errorCount)
        {
            return dal.AddKeywordProduct(table, out errorCount);
        }

        public bool Update(DataTable table, out int errorCount)
        {
            return dal.UpdateKeywordProduct(table, out errorCount);
        }

        #endregion
    }
}
