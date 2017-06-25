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
    public class KeywordProductMySqlDAL
    {
        private static Database dbw = JXProductMySqlData.Writer;
        private static Database dbr = JXProductMySqlData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(KeywordProductMySqlDAL));

        #region MySql关键词关联商品表相关操作

        public int GetMaxKeywordProductID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from KeywordProduct order by RelationID DESC limit 0, 1";
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
                myLog.ErrorFormat("GetMaxKeywordProductID 获取关键词关联商品最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        public bool UpdateKeywordProduct(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into KeywordProduct ( " + parmsKey + " ) values ");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var dr = table.Rows[i];
                    var Placeholder = string.Format(@"({0},{1},{2},{3})",
                                      dr["RelationID"].ToInt(), dr["KeywordID"].ToInt(), dr["ProductID"].ToInt(), dr["Sort"].ToShort());
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
                myLog.ErrorFormat("UpdateKeywordProduct 更新关键词关联商品表失败,关键词关联商品ID：{0}-{1},异常信息:{2}", table.Rows[0]["RelationID"], table.Rows[table.Rows.Count - 1]["RelationID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        public bool UpdateKeywordProductEx(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var dr = table.Rows[i];
                try
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("update KeywordProduct set ");
                    var Placeholder = string.Format(@"KeywordID = '{0}',ProductID = '{1}', Sort = '{2}'",
                                      dr["KeywordID"].ToInt(), dr["ProductID"].ToInt(), dr["Sort"].ToShort());
                    sqlCommand.Append(Placeholder);
                    sqlCommand.AppendFormat(@" where RelationID = {0}", dr["RelationID"]);
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\'null\'", "null").Replace("\\", "\\\\"));
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount++;
                        flag = false;
                        myLog.ErrorFormat("UpdateKeywordProduct 更新关键词关联商品表失败,关键词关联商品ID：{0},受影响行为0", dr["RelationID"]);
                    }
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("UpdateKeywordProduct 更新关键词关联商品表失败,关键词关联商品ID：{0},异常信息:{1}", dr["RelationID"], ex.Message);
                    flag = false;
                    errorCount++;
                }
            }
            return flag;
        }

        public bool AddKeywordProduct(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into KeywordProduct ( " + parmsKey + " ) values ");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var dr = table.Rows[i];
                    var Placeholder = string.Format(@"({0},{1},{2},{3})",
                                     dr["RelationID"].ToInt(), dr["KeywordID"].ToInt(), dr["ProductID"].ToInt(), dr["Sort"].ToShort());
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
                myLog.ErrorFormat("AddKeywordProduct 添加关键词关联商品失败,关键词关联商品ID：{0}-{1},异常信息:{2}", table.Rows[0]["RelationID"], table.Rows[table.Rows.Count - 1]["RelationID"], ex.Message);
                flag = false;
            }
            return flag;
        }


        private string parmsKey = string.Format(@"RelationID,KeywordID,ProductID, Sort");

        #endregion
    }
}
