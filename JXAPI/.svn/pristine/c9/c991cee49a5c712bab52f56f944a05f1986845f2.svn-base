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
    public class JXYXProductMySqlDAL
    {
        private static Database dbw = JXYXMySqlData.Writer;
        private static Database dbr = JXYXMySqlData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(JXYXProductMySqlDAL));

        #region MySql商品推荐表相关操作

        public int GetMaxJXYXProductID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from product order by ProductID DESC limit 0, 1";
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
                myLog.ErrorFormat("GetMaxJXYXProductID 获取商品推荐表最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        public bool UpdateJXYXProduct(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into product ( " + parmsKey + " ) values ");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var dr = table.Rows[i];
                    var Placeholder = string.Format(@"({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')",
                                     dr["ProductID"].ToInt(), dr["AlsoSee"].ToString().Replace("\'", "\""), dr["AlsoPurchased"].ToString().Replace("\'", "\""), dr["Purchased"].ToString().Replace("\'", "\""), dr["AlsoFavorite"].ToString().Replace("\'", "\"")
                                     , dr["RelateBrand"].ToString().Replace("\'", "\""), dr["RelateClass"].ToString().Replace("\'", "\""), dr["PriceEval"].ToString().Replace("\'", "\""), dr["BrandEval"].ToString().Replace("\'", "\""), dr["CategoryEval"].ToString().Replace("\'", "\"")
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
                myLog.ErrorFormat("UpdateJXYXProduct 更新商品推荐表表失败,商品推荐表ID：{0}-{1},异常信息:{2}", table.Rows[0]["ProductID"], table.Rows[table.Rows.Count - 1]["ProductID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        public bool UpdateJXYXProductEx(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var dr = table.Rows[i];
                try
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("update product set ");
                    var Placeholder = string.Format(@"AlsoSee = '{0}',AlsoPurchased = '{1}',BuyAlsoSee = '{2}',AlsoFavorite = '{3}',RelateBrand = '{4}',RelateClass = '{5}',PriceEval = '{6}',BrandEval = '{7}',CategoryEval = '{8}',
                                                      CreateTime = '{9}',Updater = '{10}',UpdateTime = '{11}'",
                                     dr["AlsoSee"].ToString().Replace("\'", "\""), dr["AlsoPurchased"].ToString().Replace("\'", "\""), dr["Purchased"].ToString().Replace("\'", "\""), dr["AlsoFavorite"].ToString().Replace("\'", "\"")
                                     , dr["RelateBrand"].ToString().Replace("\'", "\""), dr["RelateClass"].ToString().Replace("\'", "\""), dr["PriceEval"].ToString().Replace("\'", "\""), dr["BrandEval"].ToString().Replace("\'", "\""), dr["CategoryEval"].ToString().Replace("\'", "\"")
                                     , dr["CreateTime"].ToDateTime().ToString(), dr["Updater"].ToString().Replace("\'", "\""), dr["UpdateTime"].ToDateTime().ToString());
                    sqlCommand.Append(Placeholder);
                    sqlCommand.AppendFormat(@" where ProductID = {0}", dr["ProductID"]);
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\'null\'", "null").Replace("\\", "\\\\"));
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount++;
                        flag = false;
                        myLog.ErrorFormat("UpdateJXYXProduct 更新商品推荐表表失败,商品推荐表ID：{0},受影响行为0", dr["ProductID"]);
                    }
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("UpdateJXYXProduct 更新商品推荐表表失败,商品推荐表ID：{0},异常信息:{1}", dr["ProductID"], ex.Message);
                    flag = false;
                    errorCount++;
                }
            }
            return flag;
        }

        public bool AddJXYXProduct(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into product ( " + parmsKey + " ) values ");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var dr = table.Rows[i];
                    var Placeholder = string.Format(@"({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')",
                                     dr["ProductID"].ToInt(), dr["AlsoSee"].ToString().Replace("\'", "\""), dr["AlsoPurchased"].ToString().Replace("\'", "\""), dr["Purchased"].ToString().Replace("\'", "\""), dr["AlsoFavorite"].ToString().Replace("\'", "\"")
                                     , dr["RelateBrand"].ToString().Replace("\'", "\""), dr["RelateClass"].ToString().Replace("\'", "\""), dr["PriceEval"].ToString().Replace("\'", "\""), dr["BrandEval"].ToString().Replace("\'", "\""), dr["CategoryEval"].ToString().Replace("\'", "\"")
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
                myLog.ErrorFormat("AddJXYXProduct 添加商品推荐表失败,商品推荐表ID：{0}-{1},异常信息:{2}", table.Rows[0]["ProductID"], table.Rows[table.Rows.Count - 1]["ProductID"], ex.Message);
                flag = false;
            }
            return flag;
        }


        private string parmsKey = string.Format(@"ProductID,AlsoSee,AlsoPurchased,BuyAlsoSee,AlsoFavorite,RelateBrand,RelateClass,PriceEval,BrandEval,CategoryEval,
                                                CreateTime,Updater,UpdateTime");

        #endregion
    }
}
