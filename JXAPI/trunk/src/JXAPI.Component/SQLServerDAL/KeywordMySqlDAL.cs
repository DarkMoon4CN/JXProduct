using JXAPI.Component.DataAccess;
using JXAPI.Component.Model;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.SQLServerDAL
{
    public class KeywordMySqlDAL
    {
        private static Database dbw = JXProductMySqlData.Writer;
        private static Database dbr = JXProductMySqlData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(KeywordMySqlDAL));

        #region MySql关键词表相关操作

        public int GetMaxKeywordID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from Keyword order by KeywordID DESC limit 0, 1";
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
                myLog.ErrorFormat("GetMaxKeywordID 获取关键词信息最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        public bool UpdateKeyword(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into Keyword ( " + parmsKey + " ) values ");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var dr = table.Rows[i];
                    var Placeholder = string.Format(@"({0},'{1}','{2}','{3}',{4},{5},{6},{7},'{8}','{9}','{10}','{11}')",
                 dr["KeywordID"].ToInt(), dr["ChineseName"].ToString().Replace("\'", "\""), dr["PinyinName"].ToString().Replace("\'", "\""), dr["FirstLetter"].ToString().Replace("\'", "\"")
                 , dr["ProductCount"].ToInt(), dr["RelationCount"].ToInt(), dr["Status"].ToShort(), dr["TypeID"].ToShort()
                 , dr["Creator"].ToString().Replace("\'", "\""), dr["CreateTime"].ToDateTime().ToString(), dr["Updater"].ToString().Replace("\'", "\""), dr["UpdateTime"].ToDateTime().ToString());
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
                        errorCount = table.Rows.Count;
                        flag = false;
                    }
                    else
                    {
                        errorCount = (table.Rows.Count - result > 0) ? table.Rows.Count - result : 0;
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
                myLog.ErrorFormat("UpdateKeyword 更新关键词信息表失败,关键词信息ID：{0}-{1},异常信息:{2}", table.Rows[0]["KeywordID"], table.Rows[table.Rows.Count - 1]["KeywordID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        public bool UpdateKeywordEx(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var dr = table.Rows[i];
                try
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("update Keyword set ");
                    var Placeholder = string.Format(@"ChineseName = '{0}',PinyinName = '{1}', FirstLetter = '{2}',ProductCount = '{3}',RelationCount = '{4}',Status = '{5}',TypeID = '{6}',
                                                Creator = '{7}',CreateTime = '{8}',Updater = '{9}',UpdateTime = '{10}'",
                                        dr["ChineseName"].ToString().Replace("\'", "\""), dr["PinyinName"].ToString().Replace("\'", "\""), dr["FirstLetter"].ToString().Replace("\'", "\"")
                                        , dr["ProductCount"].ToInt(), dr["RelationCount"].ToInt(), dr["Status"].ToShort(), dr["TypeID"].ToShort()
                                        , dr["Creator"].ToString().Replace("\'", "\""), dr["CreateTime"].ToDateTime().ToString(), dr["Updater"].ToString().Replace("\'", "\""), dr["UpdateTime"].ToDateTime().ToString());
                    sqlCommand.Append(Placeholder);
                    sqlCommand.AppendFormat(@" where KeywordID = {0}", dr["KeywordID"]);
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\'null\'", "null").Replace("\\", "\\\\"));
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount++;
                        flag = false;
                        myLog.ErrorFormat("UpdateKeyword 更新关键词信息表失败,关键词信息ID：{0},受影响行为0", dr["KeywordID"]);
                    }
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("UpdateKeyword 更新关键词信息表失败,关键词信息ID：{0},异常信息:{1}", dr["KeywordID"], ex.Message);
                    flag = false;
                }
            }

            return flag;
        }

        public bool AddKeyword(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into Keyword ( " + parmsKey + " ) values ");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var dr = table.Rows[i];
                    var Placeholder = string.Format(@"({0},'{1}','{2}','{3}',{4},{5},{6},{7},'{8}','{9}','{10}','{11}')",
                                     dr["KeywordID"].ToInt(), dr["ChineseName"].ToString().Replace("\'", "\""), dr["PinyinName"].ToString().Replace("\'", "\""), dr["FirstLetter"].ToString().Replace("\'", "\"")
                                     , dr["ProductCount"].ToInt(), dr["RelationCount"].ToInt(), dr["Status"].ToShort(), dr["TypeID"].ToShort()
                                     , dr["Creator"].ToString().Replace("\'", "\""), dr["CreateTime"].ToDateTime().ToString(), dr["Updater"].ToString().Replace("\'", "\""), dr["UpdateTime"].ToDateTime().ToString());
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
                        errorCount = table.Rows.Count;
                        flag = false;
                    }
                    else
                    {
                        errorCount = (table.Rows.Count - result > 0) ? table.Rows.Count - result : 0;
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
                myLog.ErrorFormat("AddKeyword 添加关键词信息失败,关键词信息ID：{0}-{1},异常信息:{2}", table.Rows[0]["KeywordID"], table.Rows[table.Rows.Count - 1]["KeywordID"], ex.Message);
                flag = false;
            }
            return flag;
        }


        private string parmsKey = string.Format(@"KeywordID,ChineseName,PinyinName, FirstLetter,ProductCount,RelationCount,Status,TypeID,
                                                Creator,CreateTime,Updater,UpdateTime");

        #endregion
    }
}
