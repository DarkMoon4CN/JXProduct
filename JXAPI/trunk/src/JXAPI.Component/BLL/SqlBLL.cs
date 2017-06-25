using JXAPI.Component.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.BLL
{
    public class SqlBLL : ISqlBLL
    {
        private static readonly JXAPI.Component.SQLServerDAL.SqlDAL dal = new JXAPI.Component.SQLServerDAL.SqlDAL();
        public static SqlBLL Instance
        {
            get
            {
                return new SqlBLL();
            }
        }

        #region CURD

        public System.Data.DataTable GetAddList(int ID, string updateTime, int pageSize, string tableName)
        {
            return dal.GetAddList(ID,updateTime,pageSize,tableName);
        }

        public System.Data.DataTable GetUpdateList(int MaxID, int NowID, string updateTime, int pageSize, string tableName)
        {
            return dal.GetUpdateList(MaxID,NowID,updateTime,pageSize,tableName);
        }
        
        #endregion

        /// <summary>
        /// 关键词列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public System.Data.DataTable Keyword_GetListForSearch(string strWhere)
        {
            return dal.Keyword_GetListForSearch(strWhere);
        }

        /// <summary>
        /// 搜索关键字
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable SearchKeyword_GetListForSearch()
        {
            return dal.SearchKeyword_GetListForSearch();
        }

        /// <summary>
        /// 修改搜索关键词
        /// <param name="related"></param>
        /// <param name="salled">热销推荐（销量）</param>
        /// <param name="recommend">精品推荐（毛利率）</param>
        /// <param name="preferential">优惠推荐（折扣）</param>
        /// <param name="hotSalled">金象热卖（折扣销量）</param>
        /// <param name="keywordID"></param>
        /// </summary>
        public int Update_SearchKeyword(string related, string salled, string recommend, string preferential, string hotSalled, int productCount, int keywordID)
        {
            return dal.Update_SearchKeyword(related, salled, recommend, preferential, hotSalled, productCount, keywordID);
        }
    }
}
