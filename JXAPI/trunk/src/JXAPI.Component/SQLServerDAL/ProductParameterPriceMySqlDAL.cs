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
    public class ProductParameterPriceMySqlDAL
    {
        private static Database dbw = JXProductMySqlData.Writer;
        private static Database dbr = JXProductMySqlData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(ProductParameterPriceMySqlDAL));

        #region MySql 商品属性报价表相关操作

        /// <summary>
        /// 查询商品属性报价最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxProductParameterPriceID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from productparameterprice order by PriceParaID DESC limit 0, 1";
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
                myLog.ErrorFormat("GetMaxProductParameterPriceID 获取商品属性报价最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        /// <summary>
        /// 更新商品属性报价
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool UpdateProductParameterPrice(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into productparameterprice ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},{1},{2},'{3}','{4}','{5}')",
                                      Convert.ToInt64(dr["ParaPriceID"]), dr["MainProductID"].ToInt(), dr["ChildProductID"].ToInt(), dr["Prop1"].ToString().Replace("\'", "\"")
                                     , dr["Prop2"].ToString().Replace("\'", "\""), dr["Prop3"].ToString().Replace("\'", "\""));
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
                myLog.ErrorFormat("UpdateProductParameterPrice 更新商品属性报价表失败,商品属性报价ID：{0}-{1},异常信息:{2}", productTable.Rows[0]["ParaPriceID"], productTable.Rows[productTable.Rows.Count - 1]["ParaPriceID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 更新商品属性报价
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool UpdateProductParameterPriceEx(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            for (int i = 0; i < productTable.Rows.Count; i++)
            {
                var dr = productTable.Rows[i];
                try
                {
                    StringBuilder sqlCommand = new StringBuilder();
                    sqlCommand.Append("update productparameterprice set ");
                    var Placeholder = string.Format(@"MainProductID = '{0}',ChildProductID = '{1}', Prop1 = '{2}',Prop2 = '{3}',Prop3 = '{4}'",
                                      Convert.ToInt64(dr["ParaPriceID"]), dr["MainProductID"].ToInt(), dr["ChildProductID"].ToInt(), dr["Prop1"].ToString().Replace("\'", "\"")
                                     , dr["Prop2"].ToString().Replace("\'", "\""), dr["Prop3"].ToString().Replace("\'", "\""));
                    sqlCommand.Append(Placeholder);
                    sqlCommand.AppendFormat(@" where PriceParaID = {0}", Convert.ToInt64(dr["ParaPriceID"]));
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\'null\'", "null").Replace("\\", "\\\\"));
                    var result = dbw.ExecuteNonQuery(cmd);
                    if (result <= 0)
                    {
                        errorCount++;
                        flag = false;
                        myLog.ErrorFormat("UpdateProductParameterPrice 更新商品属性报价表失败,商品属性报价ID：{0},受影响行为0", dr["ParaPriceID"]);
                    }
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("UpdateProductParameterPrice 更新商品属性报价表失败,商品属性报价ID：{0},异常信息:{1}", dr["ParaPriceID"], ex.Message);
                    flag = false;
                    errorCount++;
                }
            }
            return flag;
        }

        /// <summary>
        /// 添加商品属性报价
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool AddProductParameterPrice(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into productparameterprice ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},{1},{2},'{3}','{4}','{5}')",
                                     Convert.ToInt64(dr["ParaPriceID"]), dr["MainProductID"].ToInt(), dr["ChildProductID"].ToInt(), dr["Prop1"].ToString().Replace("\'", "\"")
                                     , dr["Prop2"].ToString().Replace("\'", "\""), dr["Prop3"].ToString().Replace("\'", "\""));
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
                myLog.ErrorFormat("AddProductParameterPrice 添加商品属性报价失败,商品属性报价ID：{0}-{1},异常信息:{2}", productTable.Rows[0]["ParaPriceID"], productTable.Rows[productTable.Rows.Count - 1]["ParaPriceID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        #endregion

        private string parmsKey = string.Format(@"PriceParaID,MainProductID,ChildProductID, Prop1,Prop2,Prop3 ");
    }
}
