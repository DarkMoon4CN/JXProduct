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
    public class SaleMySqlDAL
    {
        private static Database dbw = JXActivityMySqlData.Writer;
        private static Database dbr = JXActivityMySqlData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(SaleMySqlDAL));

        #region MySql促销商品相关操作

        public int GetMaxSaleID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from Sale order by SaleID DESC limit 0, 1";
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
                myLog.ErrorFormat("GetMaxSaleID 获取促销商品最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        public bool UpdateSale(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into Sale ( " + parmsKey + " ) values ");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var dr = table.Rows[i];
                    var Placeholder = string.Format(@"({0},{1},'{2}',{3},'{4}','{5}','{6}','{7}','{8}','{9}')",
                 dr["SaleID"].ToInt(), dr["DataID"].ToInt(), dr["Selling"].ToString().Replace("\'", "\""), dr["Sort"].ToShort()
                 , dr["CodeID"].ToString().Replace("\'", "\""), dr["KeyValue"].ToString().Replace("\'", "\""), dr["Creator"].ToString().Replace("\'", "\""), dr["CreateTime"].ToDateTime().ToString()
                 , dr["Updater"].ToString().Replace("\'", "\""), dr["UpdateTime"].ToDateTime().ToString());
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
                myLog.ErrorFormat("UpdateSale 更新促销商品表失败,促销商品ID：{0}-{1},异常信息:{2}", table.Rows[0]["SaleID"], table.Rows[table.Rows.Count - 1]["SaleID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        public bool UpdateSaleEx(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var dr = table.Rows[i];
                try
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("update Sale set ");
                    var Placeholder = string.Format(@"DataID = '{0}',Selling = '{1}', Sort = '{2}',CodeID = '{3}',KeyValue = '{4}',Creator = '{5}',CreateTime = '{6}',Updater = '{7}',UpdateTime = '{8}'",
                                          dr["DataID"].ToInt(), dr["Selling"].ToString().Replace("\'", "\""), dr["Sort"].ToShort()
                                         , dr["CodeID"].ToString().Replace("\'", "\""), dr["KeyValue"].ToString().Replace("\'", "\""), dr["Creator"].ToString().Replace("\'", "\""), dr["CreateTime"].ToDateTime().ToString()
                                         , dr["Updater"].ToString().Replace("\'", "\""), dr["UpdateTime"].ToDateTime().ToString());
                    sqlCommand.Append(Placeholder);
                    sqlCommand.AppendFormat(@" where SaleID = {0}", dr["SaleID"]);
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\'null\'", "null").Replace("\\", "\\\\"));
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount++;
                        flag = false;
                        myLog.ErrorFormat("UpdateSale 更新促销商品表失败,促销商品ID：{0},受影响行为0", dr["SaleID"]);
                    }
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("UpdateSale 更新促销商品表失败,促销商品ID：{0},异常信息:{1}", dr["SaleID"], ex.Message);
                    flag = false;
                    errorCount++;
                }
            }
            return flag;
        }

        public bool AddSale(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into Sale ( " + parmsKey + " ) values ");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var dr = table.Rows[i];
                    var Placeholder = string.Format(@"({0},{1},'{2}',{3},'{4}','{5}','{6}','{7}','{8}','{9}')",
                                     dr["SaleID"].ToInt(), dr["DataID"].ToInt(), dr["Selling"].ToString().Replace("\'", "\""), dr["Sort"].ToShort()
                                     , dr["CodeID"].ToString().Replace("\'", "\""), dr["KeyValue"].ToString().Replace("\'", "\""), dr["Creator"].ToString().Replace("\'", "\""), dr["CreateTime"].ToDateTime().ToString()
                                     , dr["Updater"].ToString().Replace("\'", "\""), dr["UpdateTime"].ToDateTime().ToString());
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
                myLog.ErrorFormat("AddSale 添加促销商品失败,促销商品ID：{0}-{1},异常信息:{2}", table.Rows[0]["SaleID"], table.Rows[table.Rows.Count - 1]["SaleID"], ex.Message);
                flag = false;
            }
            return flag;
        }


        private string parmsKey = string.Format(@"SaleID,DataID,Selling, Sort,CodeID,KeyValue,Creator,CreateTime,Updater,UpdateTime");

        #endregion
    }
}
