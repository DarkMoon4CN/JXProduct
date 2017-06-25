using JXBigData.Component.Model;
using JXProduct.Component.DataAccess;
using JXUtil;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace JXBigData.Component.SqlServerDAL
{
    public class SearchKeywordDailyDAL
    {
        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;

        public OperationResult<IList<SearchKeywordDailyInfo>> SearchKeywordDaily_DefaultGet(int sourceID, DateTime startTime, DateTime endTime) 
        {
            IList<SearchKeywordDailyInfo> skInfoList = new List<SearchKeywordDailyInfo>();
            try
            {
                string sqlCommand = "JXMarketing.dbo.SearchKeywordDaily_DefaultGet";
                DbCommand cmd = dbw.GetStoredProcCommand(sqlCommand);
                dbw.AddInParameter(cmd, "SourceID", DbType.Int32, sourceID);
                dbw.AddInParameter(cmd, "StartTime", DbType.DateTime, startTime);
                dbw.AddInParameter(cmd, "EndTime", DbType.DateTime, endTime);
                using (var read = dbr.ExecuteReader(cmd))
                {
                    while (read.Read())
                    {
                        skInfoList.Add(GetSearchKeywordModel(read));
                    }
                }
                return new OperationResult<IList<SearchKeywordDailyInfo>>(OperationResultType.Success, null, skInfoList);
            }
            catch (Exception ex) 
            {
                return new OperationResult<IList<SearchKeywordDailyInfo>>(OperationResultType.Error, ex.Message);
            }
        }

        private SearchKeywordDailyInfo GetSearchKeywordModel(IDataReader read)
        {
            SearchKeywordDailyInfo skInfo = new SearchKeywordDailyInfo();
            skInfo.Rowid = read["Rowid"].ToInt();
            skInfo.TotalQuan = read["TotalQuan"].ToInt();
            skInfo.Keywords = read["Keywords"].ToString();
            return skInfo;
        }

        public OperationResult<IList<SearchKeywordCountInfo>> SearchKeywordDaily_AllGet(DateTime startTime, DateTime endTime) 
        {
            IList<SearchKeywordCountInfo> skInfoList = new List<SearchKeywordCountInfo>();
            try
            {
                string sqlCommand = "JXMarketing.dbo.SearchKeywordDaily_AllGet";
                DbCommand cmd = dbw.GetStoredProcCommand(sqlCommand);
                dbw.AddInParameter(cmd, "StartTime", DbType.DateTime, startTime);
                dbw.AddInParameter(cmd, "EndTime", DbType.DateTime, endTime);
                using (var read = dbr.ExecuteReader(cmd))
                {
                    while (read.Read())
                    {
                        skInfoList.Add(GetCountModel(read));
                    }
                }
                return new OperationResult<IList<SearchKeywordCountInfo>>(OperationResultType.Success, null, skInfoList);
            }
            catch (Exception ex)
            {
                return new OperationResult<IList<SearchKeywordCountInfo>>(OperationResultType.Error, ex.Message);
            }
        }

        private SearchKeywordCountInfo GetCountModel(IDataReader read)
        {
            SearchKeywordCountInfo skInfo = new SearchKeywordCountInfo();
            skInfo.Rowid = read["Rowid"].ToInt();
            skInfo.Keywords = read["Keywords"].ToString();
            skInfo.AllCount = read["AllCount"].ToInt();
            skInfo.Web = read["Web"].ToInt();
            skInfo.H5 = read["H5"].ToInt();
            skInfo.Android = read["Android"].ToInt();
            skInfo.IOS = read["IOS"].ToInt();
            return skInfo;
        }
    }
}
