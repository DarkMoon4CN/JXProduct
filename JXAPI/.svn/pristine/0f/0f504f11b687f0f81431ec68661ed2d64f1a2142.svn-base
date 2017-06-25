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
    public class PromotionMySqlDAL
    {
        private static Database dbw = JXActivityMySqlData.Writer;
        private static Database dbr = JXActivityMySqlData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(PromotionMySqlDAL));

        #region MySql展位资源表相关操作

        public int GetMaxPromotionID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from Promotion order by PromotionID DESC limit 0, 1";
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
                myLog.ErrorFormat("GetMaxPromotionID 获取展位资源最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        public bool UpdatePromotion(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into Promotion ( " + parmsKey + " ) values ");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var dr = table.Rows[i];
                    var Placeholder = string.Format(@"({0},'{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}','{10}')",
                 dr["PromotionID"].ToInt(), dr["ChineseName"].ToString().Replace("\'", "\""), dr["Picture"].ToString().Replace("\'", "\""), dr["Links"].ToString().Replace("\'", "\"")
                 , dr["Sort"].ToShort(), dr["CodeID"].ToString().Replace("\'", "\""), dr["KeyValue"].ToString().Replace("\'", "\""), dr["Creator"].ToString().Replace("\'", "\"")
                 , dr["CreateTime"].ToDateTime().ToString(), dr["Updater"].ToString().Replace("\'", "\""), dr["UpdateTime"].ToDateTime().ToString());
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
                myLog.ErrorFormat("UpdatePromotion 更新展位资源表失败,展位资源ID：{0}-{1},异常信息:{2}", table.Rows[0]["PromotionID"], table.Rows[table.Rows.Count - 1]["PromotionID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        public bool UpdatePromotionEx(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var dr = table.Rows[i];
                try
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("update Promotion set ");
                    var Placeholder = string.Format(@"ChineseName = '{0}',Picture = '{1}', Links = '{2}',Sort = '{3}',CodeID = '{4}',KeyValue = '{5}',Creator = '{6}',CreateTime = '{7}',
                                                      Updater = '{8}',UpdateTime = '{9}'",
                                         dr["ChineseName"].ToString().Replace("\'", "\""), dr["Picture"].ToString().Replace("\'", "\""), dr["Links"].ToString().Replace("\'", "\"")
                                        , dr["Sort"].ToShort(), dr["CodeID"].ToString().Replace("\'", "\""), dr["KeyValue"].ToString().Replace("\'", "\""), dr["Creator"].ToString().Replace("\'", "\"")
                                        , dr["CreateTime"].ToDateTime().ToString(), dr["Updater"].ToString().Replace("\'", "\""), dr["UpdateTime"].ToDateTime().ToString());
                    sqlCommand.Append(Placeholder);
                    sqlCommand.AppendFormat(@" where PromotionID = {0}", dr["PromotionID"]);
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\'null\'", "null").Replace("\\", "\\\\"));
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount++;
                        flag = false;
                        myLog.ErrorFormat("UpdatePromotion 更新展位资源表失败,展位资源ID：{0},受影响行为0", dr["PromotionID"]);
                    }
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("UpdatePromotion 更新展位资源表失败,展位资源ID：{0},异常信息:{1}", dr["PromotionID"], ex.Message);
                    flag = false;
                    errorCount++;
                }
            }
            return flag;
        }

        public bool AddPromotion(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into Promotion ( " + parmsKey + " ) values ");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var dr = table.Rows[i];
                    var Placeholder = string.Format(@"({0},'{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}','{10}')",
                                     dr["PromotionID"].ToInt(), dr["ChineseName"].ToString().Replace("\'", "\""), dr["Picture"].ToString().Replace("\'", "\""), dr["Links"].ToString().Replace("\'", "\"")
                                     , dr["Sort"].ToShort(), dr["CodeID"].ToString().Replace("\'", "\""), dr["KeyValue"].ToString().Replace("\'", "\""), dr["Creator"].ToString().Replace("\'", "\"")
                                     , dr["CreateTime"].ToDateTime().ToString(), dr["Updater"].ToString().Replace("\'", "\""), dr["UpdateTime"].ToDateTime().ToString());
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
                myLog.ErrorFormat("AddPromotion 添加展位资源失败,展位资源ID：{0}-{1},异常信息:{2}", table.Rows[0]["PromotionID"], table.Rows[table.Rows.Count - 1]["PromotionID"], ex.Message);
                flag = false;
            }
            return flag;
        }


        private string parmsKey = string.Format(@"PromotionID,ChineseName,Picture, Links,Sort,CodeID,KeyValue,Creator,CreateTime,
                                                Updater,UpdateTime");

        #endregion
    }
}
