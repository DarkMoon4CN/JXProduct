using JXAPI.Component.DataAccess;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace JXAPI.Component.SQLServerDAL
{
    public class OrderOperateLogSqlDAL
    {
        private static Database dbr = JXThirdPartyData.Reader;
        private static Database dbw = JXThirdPartyData.Writer;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(OrderOperateLogSqlDAL));

        public int GetMaxOrderOperateLogID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select max(LogID) from OrderOperateLog";
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
                myLog.ErrorFormat("GetMaxOrderOperateLogID 获取订单操作日志表最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        /// <summary>
        /// 更新订单操作日志表
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool UpdateOrderOperateLog(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            for (int i = 0; i < productTable.Rows.Count; i++)
            {
                var dr = productTable.Rows[i];
                try
                {
                    string strPlaceholder = string.Empty;
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("update OrderOperateLog set ");
                    var Placeholder = string.Format(@"UID = '{0}',UserType = '{1}',UserName = '{2}',Tittle = '{3}',Content = '{4}',
                                                      OldStatus = '{5}',NewStatus = '{6}',CreateTime = '{7}',Status = '{8}'",
                                                   dr["UID"].ToInt(), dr["UserType"].ToShort(), dr["UserName"].ToString().Replace("\'", "\""), dr["Tittle"].ToString().Replace("\'", "\""), dr["Content"].ToString().Replace("\'", "\""), Convert.ToBoolean(dr["OldStatus"]) ? 1 : 0
                                                   , Convert.ToBoolean(dr["NewStatus"]) ? 1 : 0, dr["CreateTime"].ToDateTime().ToString(), Convert.ToBoolean(dr["Status"]) ? 1 : 0);
                    sqlCommand.Append(strPlaceholder);
                    sqlCommand.AppendFormat(" where LogID = {0}", dr["LogID"].ToInt());
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\\", "\\\\"));
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount++;
                        flag = false;
                        myLog.ErrorFormat("UpdateOrderOperateLog 更新订单操作日志失败-受影响行为0,订单操作日志ID：{0}}", dr["LogID"]);
                    }
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("UpdateOrderOperateLog 更新订单操作日志失败,订单操作日志ID：{0},异常信息:{1}", dr["LogID"], ex.Message);
                    flag = false;
                    errorCount++;
                }
            }
            return flag;
        }

        /// <summary>
        /// 添加订单操作日志表
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool AddOrderOperateLog(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            for (int i = 0; i < productTable.Rows.Count; i++)
            {
                var dr = productTable.Rows[i];
                try
                {
                    string strPlaceholder = string.Empty;
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("insert into OrderOperateLog ( " + parmsKey + " ) values ");
                    var Placeholder = string.Format(@"('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                        dr["LogID"].ToInt(), DateTime.Now.ToString("yyMMddHHmmssfff"), dr["UID"].ToInt(), dr["UserType"].ToShort(), dr["UserName"].ToString().Replace("\'", "\""), dr["Tittle"].ToString().Replace("\'", "\""), dr["Content"].ToString().Replace("\'", "\""), Convert.ToBoolean(dr["OldStatus"]) ? 1 : 0
                        , Convert.ToBoolean(dr["NewStatus"]) ? 1 : 0, dr["CreateTime"].ToDateTime().ToString(), Convert.ToBoolean(dr["Status"]) ? 1 : 0);
                    sqlCommand.Append(strPlaceholder);
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\\", "\\\\"));
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount++;
                        flag = false;
                        myLog.ErrorFormat("AddOrderOperateLog 添加订单操作日志失败-受影响行为0,订单操作日志ID：{0}}", dr["LogID"]);
                    }
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("AddOrderOperateLog 添加订单操作日志失败,订单操作日志ID：{0},异常信息:{1}", dr["LogID"], ex.Message);
                    flag = false;
                    errorCount++;
                }
            }
            return flag;
        }

        private string parmsKey = string.Format(@"LogID,OrderID, UID,UserType,UserName,Tittle,Content,OldStatus
                                                  ,NewStatus,CreateTime,Status");
    }
}
