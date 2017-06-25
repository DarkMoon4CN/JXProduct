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
    public class ActivityRuleMySqlDAL
    {
        private static Database dbw = JXActivityMySqlData.Writer;
        private static Database dbr = JXActivityMySqlData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(ActivityRuleMySqlDAL));

        #region MySql活动规则相关操作

        public int GetMaxActivityRuleID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from ActivityRule order by RuleID DESC limit 0, 1";
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
                myLog.ErrorFormat("GetMaxActivityRuleID 获取活动规则最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        public bool UpdateActivityRule(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into ActivityRule ( " + parmsKey + " ) values ");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var dr = table.Rows[i];
                    var Placeholder = string.Format(@"({0},{1},{2},{3},{4},{5},'{6}','{7}','{8}','{9}','{10}','{11}',{12})",
                                     dr["RuleID"].ToInt(), dr["ActID"].ToInt(), dr["Amount"].ToDecimal(), dr["Quantity"].ToInt()
                                     , dr["DiscountAmount"].ToDecimal(), dr["Discount"].ToDecimal(), dr["ProductID"].ToString().Replace("\'", "\""), dr["CouponBatchNo"].ToString().Replace("\'", "\"")
                                     , dr["CreateTime"].ToDateTime().ToString(), dr["Creator"].ToString().Replace("\'", "\""), dr["Updater"].ToString().Replace("\'", "\"")
                                     , dr["UpdateTime"].ToDateTime().ToString(), dr["Status"].ToShort());
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
                myLog.ErrorFormat("UpdateActivityRule 更新活动规则表失败,活动规则ID：{0}-{1},异常信息:{2}", table.Rows[0]["RuleID"], table.Rows[table.Rows.Count - 1]["RuleID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        public bool UpdateActivityRuleEx(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var dr = table.Rows[i];
                try
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("update ActivityRule set ");
                    var Placeholder = string.Format(@"ActID = '{0}',Amount = '{1}', Quantity = '{2}',DiscountAmount = '{3}',Discount = '{4}',ProductID = '{5}',CouponBatchNo = '{6}',CreateTime = '{7}',Creator = '{8}',
                                                Updater = '{9}',UpdateTime = '{10}',Status = '{11}'",
                                     dr["ActID"].ToInt(), dr["Amount"].ToDecimal(), dr["Quantity"].ToInt()
                                     , dr["DiscountAmount"].ToDecimal(), dr["Discount"].ToDecimal(), dr["ProductID"].ToString().Replace("\'", "\""), dr["CouponBatchNo"].ToString().Replace("\'", "\"")
                                     , dr["CreateTime"].ToDateTime().ToString(), dr["Creator"].ToString().Replace("\'", "\""), dr["Updater"].ToString().Replace("\'", "\"")
                                     , dr["UpdateTime"].ToDateTime().ToString(), dr["Status"].ToShort());
                    sqlCommand.Append(Placeholder);
                    sqlCommand.AppendFormat(@" where RuleID = {0}", dr["RuleID"]);
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\'null\'", "null").Replace("\\", "\\\\"));
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount++;
                        flag = false;
                        myLog.ErrorFormat("UpdateActivityRule 更新活动规则表失败,活动规则ID：{0},受影响行为0", dr["RuleID"]);
                    }
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("UpdateActivityRule 更新活动规则表失败,活动规则ID：{0},异常信息:{1}", dr["RuleID"], ex.Message);
                    flag = false;
                    errorCount++;
                }
            }
            return flag;
        }

        public bool AddActivityRule(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into ActivityRule ( " + parmsKey + " ) values ");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var dr = table.Rows[i];
                    var Placeholder = string.Format(@"({0},{1},{2},{3},{4},{5},'{6}','{7}','{8}','{9}','{10}','{11}',{12})",
                                     dr["RuleID"].ToInt(), dr["ActID"].ToInt(), dr["Amount"].ToDecimal(), dr["Quantity"].ToInt()
                                     , dr["DiscountAmount"].ToDecimal(), dr["Discount"].ToDecimal(), dr["ProductID"].ToString().Replace("\'", "\""), dr["CouponBatchNo"].ToString().Replace("\'", "\"")
                                     , dr["CreateTime"].ToDateTime().ToString(), dr["Creator"].ToString().Replace("\'", "\""), dr["Updater"].ToString().Replace("\'", "\"")
                                     , dr["UpdateTime"].ToDateTime().ToString(), dr["Status"].ToShort());
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
                myLog.ErrorFormat("AddActivityRule 添加活动规则失败,活动规则ID：{0}-{1},异常信息:{2}", table.Rows[0]["RuleID"], table.Rows[table.Rows.Count - 1]["RuleID"], ex.Message);
                flag = false;
            }
            return flag;
        }


        private string parmsKey = string.Format(@"RuleID,ActID,Amount, Quantity,DiscountAmount,Discount,ProductID,CouponBatchNo,CreateTime,Creator,
                                                Updater,UpdateTime,Status");

        #endregion
    }
}
