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
    public class KeywordRelationMySqlBLL : IProductPlanMySqlBLL
    {
        private KeywordRelationMySqlBLL() { }
        private static KeywordRelationMySqlBLL _instance;
        private static readonly KeywordRelationMySqlDAL dal = new KeywordRelationMySqlDAL();

        public static KeywordRelationMySqlBLL Instance
        {
            get
            {
                return _instance ?? (_instance = new KeywordRelationMySqlBLL());
            }
        }

        #region CURD

        public int GetMaxID()
        {
            return dal.GetMaxKeywordRelationID();
        }

        public bool Add(DataTable table, out int errorCount)
        {
            return dal.AddKeywordRelation(table, out errorCount);
        }

        public bool Update(DataTable table, out int errorCount)
        {
            return dal.UpdateKeywordRelation(table, out errorCount);
        }

        #endregion
    }
}
