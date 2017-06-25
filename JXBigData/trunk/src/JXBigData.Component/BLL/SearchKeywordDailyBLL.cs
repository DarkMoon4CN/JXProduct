using JXBigData.Component.Model;
using JXBigData.Component.SqlServerDAL;
using JXUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXBigData.Component.BLL
{
    public class SearchKeywordDailyBLL
    {
        private SearchKeywordDailyBLL() { }
        private static SearchKeywordDailyBLL _instance;
        public static SearchKeywordDailyBLL Instance { get { return _instance ?? (_instance = new SearchKeywordDailyBLL()); } }
        private static readonly SearchKeywordDailyDAL dal = new SearchKeywordDailyDAL();

        public OperationResult<IList<SearchKeywordDailyInfo>> SearchKeywordDaily_DefaultGet(int sourceID, DateTime startTime, DateTime endTime)
        {
            return dal.SearchKeywordDaily_DefaultGet(sourceID,startTime,endTime);
        }

        public OperationResult<IList<SearchKeywordCountInfo>> SearchKeywordDaily_AllGet(DateTime startTime, DateTime endTime)
        {
            return dal.SearchKeywordDaily_AllGet(startTime,endTime);
        }
        
    }
}
