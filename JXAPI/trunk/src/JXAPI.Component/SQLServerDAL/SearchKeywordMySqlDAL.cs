﻿using JXAPI.Component.DataAccess;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace JXAPI.Component.SQLServerDAL
{
    public class SearchKeywordMySqlDAL
    {
        private static Database dbw = JXYXMySqlData.Writer;
        private static Database dbr = JXYXMySqlData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(SearchKeywordMySqlDAL));

        #region MySql 搜索关键词推荐表相关操作


        /// <summary>
        /// 查询搜索关键词推荐最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxSearchKeywordID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from searchkeyword order by KeywordID DESC limit 0, 1";
                var cmd = dbr.GetSqlStringCommand(sqlCommand);
                if (dbr.ExecuteScalar(cmd).IsNotNULL())
                {
                    maxId = Convert.ToInt32(dbr.ExecuteScalar(cmd));
                }
                else
                {
                    maxId = 0;
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("GetMaxSearchKeywordID 获取搜索关键词推荐最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        /// <summary>
        /// 更新搜索关键词推荐
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool UpdateSearchKeyword(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into searchkeyword ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')",
                                    dr["KeywordID"].ToInt(), dr["Keywords"].ToString(), dr["FirstLetter"].ToString(), dr["Count"].ToInt()
                                    , dr["Rank"].ToInt(), dr["OrderCount"].ToInt(), dr["Related"].ToString().Replace("\'", "\""), dr["Salled"].ToString().Replace("\'", "\""), dr["Recommend"].ToString().Replace("\'", "\"")
                                    , dr["Preferential"].ToString().Replace("\'", "\""), dr["HotSalled"].ToString().Replace("\'", "\""), dr["WeekSalled"].ToString().Replace("\'", "\""), dr["Creator"].ToString().Replace("\'", "\"")
                                    , dr["CreateTime"].ToDateTime(), dr["Updater"].ToString().Replace("\'", "\""), dr["UpdateTime"].ToDateTime());
                    if (i == 0)
                    {
                        strPlaceholder = Placeholder;
                    }
                    else
                    {
                        strPlaceholder += "," + Placeholder;
                    }
                }
                if (!string.IsNullOrEmpty(strPlaceholder))
                {
                    sqlCommand.Append(strPlaceholder);
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString());
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount = productTable.Rows.Count;
                        flag = false;
                    }
                    else
                    {
                        errorCount = (productTable.Rows.Count - result > 0) ? productTable.Rows.Count - result : 0;
                        if (errorCount == 0)
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("UpdateSearchKeyword 更新搜索关键词推荐表失败,搜索关键词推荐ID：{0}-{1},异常信息:{2}", productTable.Rows[0]["KeywordID"], productTable.Rows[productTable.Rows.Count - 1]["KeywordID"], ex.Message);
                flag = false;
            }
            return flag;
        }


        /// <summary>
        /// 更新搜索关键词推荐
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool UpdateSearchKeywordEx(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            for (int i = 0; i < productTable.Rows.Count; i++)
            {
                var dr = productTable.Rows[i];
                try
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("update searchkeyword set ");
                    var Placeholder = string.Format(@"Keywords = '{0}',FirstLetter = '{1}',Count = '{2}',Rank = '{3}',OrderCount = '{4}',Related = '{5}',Salled = '{6}',Recommend = '{7}',
                                                 Preferential = '{8}',HotSalled = '{9}',WeekSalled = '{10}',Updater = '{11}',UpdateTime = '{12}'",
                                    dr["Keywords"].ToString(), dr["FirstLetter"].ToString(), dr["Count"].ToInt()
                                    , dr["Rank"].ToInt(), dr["OrderCount"].ToInt(), dr["Related"].ToString().Replace("\'", "\""), dr["Salled"].ToString().Replace("\'", "\""), dr["Recommend"].ToString().Replace("\'", "\"")
                                    , dr["Preferential"].ToString().Replace("\'", "\""), dr["HotSalled"].ToString().Replace("\'", "\""), dr["WeekSalled"].ToString().Replace("\'", "\"")
                                    , dr["Updater"].ToString().Replace("\'", "\""), dr["UpdateTime"].ToDateTime());
                    sqlCommand.Append(Placeholder);
                    sqlCommand.AppendFormat(@" where KeywordID = {0}", dr["KeywordID"]);
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\'null\'", "null").Replace("\\", "\\\\"));
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount++;
                        flag = false;
                        myLog.ErrorFormat("UpdateSearchKeyword 更新搜索关键词推荐表失败,搜索关键词推荐ID：{0},受影响行为0", dr["KeywordID"]);
                    }
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("UpdateSearchKeyword 更新搜索关键词推荐表失败,搜索关键词推荐ID：{0},异常信息:{1}", dr["KeywordID"], ex.Message);
                    flag = false;
                    errorCount++;
                }
            }
            return flag;
        }

        /// <summary>
        /// 添加搜索关键词推荐
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool AddSearchKeyword(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into searchkeyword ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')",
                                    dr["KeywordID"].ToInt(), dr["Keywords"].ToString(), dr["FirstLetter"].ToString(), dr["Count"].ToInt()
                                    , dr["Rank"].ToInt(), dr["OrderCount"].ToInt(), dr["Related"].ToString().Replace("\'", "\""), dr["Salled"].ToString().Replace("\'", "\""), dr["Recommend"].ToString().Replace("\'", "\"")
                                    , dr["Preferential"].ToString().Replace("\'", "\""), dr["HotSalled"].ToString().Replace("\'", "\""), dr["WeekSalled"].ToString().Replace("\'", "\""), dr["Creator"].ToString().Replace("\'", "\"")
                                    , dr["CreateTime"].ToDateTime(), dr["Updater"].ToString().Replace("\'", "\""), dr["UpdateTime"].ToDateTime());
                    if (i == 0)
                    {
                        strPlaceholder = Placeholder;
                    }
                    else
                    {
                        strPlaceholder += "," + Placeholder;
                    }
                }
                if (!string.IsNullOrEmpty(strPlaceholder))
                {
                    sqlCommand.Append(strPlaceholder);
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString());
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount = productTable.Rows.Count;
                        flag = false;
                    }
                    else
                    {
                        errorCount = (productTable.Rows.Count - result > 0) ? productTable.Rows.Count - result : 0;
                        if (errorCount == 0)
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("AddSearchKeyword 添加搜索关键词推荐失败,搜索关键词推荐ID：{0}-{1},异常信息:{2}", productTable.Rows[0]["KeywordID"], productTable.Rows[productTable.Rows.Count - 1]["KeywordID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        #endregion

        private string parmsKey = string.Format(@"KeywordID,Keywords,FirstLetter,Count,Rank,OrderCount,Related,Salled,Recommend,
                                                 Preferential,HotSalled,WeekSalled,Creator,CreateTime,Updater,UpdateTime");
    }
}