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
    public partial class ProductStockMySqlDAL
    {
        private static Database dbw = JXActivityMySqlData.Writer;
        private static Database dbr = JXActivityMySqlData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(ProductStockMySqlDAL));

        #region MySql 商品库存表相关操作

        /// <summary>
        /// 查询商品库存库最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxProductStockID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from productstock order by StockID DESC limit 0, 1";
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
                myLog.ErrorFormat("GetMaxProductStockID 获取商品库存最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        /// <summary>
        /// 更新商品库存
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool UpdateProductStock(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into productstock ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},'{1}',{2},{3},{4},{5},{6},{7},'{8}')",
                                     dr["StockID"].ToInt(), dr["ProductCode"].ToString().Replace("\'", "\""), dr["HouseID"].ToInt(), dr["TypeID"].ToShort()
                                     , dr["Stock"].ToInt(), dr["OutStock"].ToInt(), dr["FreezedStock"].ToInt(), dr["UsableStock"].ToInt(), dr["ChangeTime"].ToDateTime().ToString());
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
                myLog.ErrorFormat("UpdateProductStock 更新商品库存表失败,商品库存ID：{0}-{1},异常信息:{2}", productTable.Rows[0]["StockID"], productTable.Rows[productTable.Rows.Count - 1]["StockID"], ex.Message);
                flag = false;
            }
            return flag;
        }


        /// <summary>
        /// 更新商品库存
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool UpdateProductStockEx(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            for (int i = 0; i < productTable.Rows.Count; i++)
            {
                var dr = productTable.Rows[i];
                try
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("update productstock set ");
                    var Placeholder = string.Format(@"ProductCode = '{0}',HouseID = '{1}', TypeID = '{2}',Stock = '{3}',OutStock = '{4}',
                                                      FreezedStock = '{5}',UsableStock = '{6}',ChangeTime = '{7}'",
                                     dr["ProductCode"].ToString().Replace("\'", "\""), dr["HouseID"].ToInt(), dr["TypeID"].ToShort()
                                     , dr["Stock"].ToInt(), dr["OutStock"].ToInt(), dr["FreezedStock"].ToInt(), dr["UsableStock"].ToInt(), dr["ChangeTime"].ToDateTime().ToString());
                    sqlCommand.Append(Placeholder);
                    sqlCommand.AppendFormat(@" where StockID = {0}", dr["StockID"]);
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\'null\'", "null").Replace("\\", "\\\\"));
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount++;
                        flag = false;
                        myLog.ErrorFormat("UpdateProductStock 更新商品库存表失败,商品库存ID：{0},受影响行为0", dr["StockID"]);
                    }
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("UpdateProductStock 更新商品库存表失败,商品库存ID：{0},异常信息:{1}", dr["StockID"], ex.Message);
                    flag = false;
                    errorCount++;
                }
            }
            return flag;
        }

        /// <summary>
        /// 添加商品库存
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool AddProductStock(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into productstock ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},'{1}',{2},{3},{4},{5},{6},{7},'{8}')",
                                     dr["StockID"].ToInt(), dr["ProductCode"].ToString().Replace("\'", "\""), dr["HouseID"].ToInt(), dr["TypeID"].ToShort()
                                     , dr["Stock"].ToInt(), dr["OutStock"].ToInt(), dr["FreezedStock"].ToInt(), dr["UsableStock"].ToInt(), dr["ChangeTime"].ToDateTime().ToString());
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
                myLog.ErrorFormat("AddProductStock 添加商品库存失败,商品库存ID：{0}-{1},异常信息:{2}", productTable.Rows[0]["StockID"], productTable.Rows[productTable.Rows.Count - 1]["StockID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        #endregion

        private string parmsKey = string.Format(@"StockID,ProductCode,HouseID, TypeID,Stock,OutStock,
                                           FreezedStock,UsableStock,ChangeTime");
    }
}
