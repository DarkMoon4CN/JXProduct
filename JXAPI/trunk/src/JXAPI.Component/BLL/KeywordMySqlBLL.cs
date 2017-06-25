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
    public class KeywordMySqlBLL : IProductPlanMySqlBLL
    {
        private KeywordMySqlBLL() { }
        private static KeywordMySqlBLL _instance;
        private static readonly KeywordMySqlDAL dal = new KeywordMySqlDAL();

        public static KeywordMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new KeywordMySqlBLL());
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxKeywordID();
        }

        public bool Add(DataTable table, out int errorCount)
        {
            return dal.AddKeyword(table, out errorCount);
        }

        public bool Update(DataTable table, out int errorCount)
        {
            return dal.UpdateKeyword(table, out errorCount);
        }

        #endregion
    }
}
