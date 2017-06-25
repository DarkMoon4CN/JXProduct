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
    public class CouponShoppingCardMySqlDAL
    {
        private static Database dbw = JXCouponBaseMySqlData.Writer;
        private static Database dbr = JXCouponBaseMySqlData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(CouponShoppingCardMySqlDAL));

        #region MySql 购物卡表相关操作


        /// <summary>
        /// 查询购物卡最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxCouponShoppingCardID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from shoppingcard order by CouponID DESC limit 0, 1";
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
                myLog.ErrorFormat("GetMaxCouponShoppingCardID 获取购物卡最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        /// <summary>
        /// 更新购物卡
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool UpdateCouponShoppingCard(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into shoppingcard ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},'{1}','{2}','{3}',{4},'{5}',{6},{7},'{8}','{9}','{10}','{11}',{12},'{13}','{14}','{15}',{16},'{17}',{18},{19},'{20}')",
                                     dr["CouponID"].ToInt(), dr["CardType"].ToString().Replace("\'", "\""), dr["CardNo"].ToString().Replace("\'", "\""), dr["CardPass"].ToString().Replace("\'", "\"")
                                     , dr["ProductID"].ToInt(), dr["ProductCode"].ToString().Replace("\'", "\""), dr["TotalAmount"].ToDecimal(), dr["RemainingSum"].ToDecimal()
                                     , string.IsNullOrEmpty(dr["StartTime"].ToString()) ? "null" : dr["StartTime"], dr["EndTime"].ToDateTime().ToString(), dr["Creator"].ToString().Replace("\'", "\""), string.IsNullOrEmpty(dr["CreateTime"].ToString()) ? "null" : dr["CreateTime"]
                                     , dr["UID"].ToInt(), dr["UserName"].ToString().Replace("\'", "\""), string.IsNullOrEmpty(dr["ActivationTime"].ToString()) ? "null" : dr["ActivationTime"], string.IsNullOrEmpty(dr["CheckTime"].ToString()) ? "null" : dr["CheckTime"]
                                     , dr["CheckCount"].ToShort(), dr["OrderNO"].ToString(), dr["Type"].ToShort()
                                     , dr["Status"].ToShort(), dr["Remarks"].ToString().Replace("\'", "\""));
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
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\'null\'", "null"));
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
                myLog.ErrorFormat("UpdateCouponShoppingCard 更新购物卡表失败,购物卡ID：{0}-{1},异常信息:{2}", productTable.Rows[0]["CouponID"], productTable.Rows[productTable.Rows.Count - 1]["CouponID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 添加购物卡
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool AddCouponShoppingCard(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into shoppingcard ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},'{1}','{2}','{3}',{4},'{5}',{6},{7},'{8}','{9}','{10}','{11}',{12},'{13}','{14}','{15}',{16},'{17}',{18},{19},'{20}')",
                                     dr["CouponID"].ToInt(), dr["CardType"].ToString().Replace("\'", "\""), dr["CardNo"].ToString().Replace("\'", "\""), dr["CardPass"].ToString().Replace("\'", "\"")
                                     , dr["ProductID"].ToInt(), dr["ProductCode"].ToString().Replace("\'", "\""), dr["TotalAmount"].ToDecimal(), dr["RemainingSum"].ToDecimal()
                                     , string.IsNullOrEmpty(dr["StartTime"].ToString()) ? "null" : dr["StartTime"], dr["EndTime"].ToDateTime().ToString(), dr["Creator"].ToString().Replace("\'", "\""), string.IsNullOrEmpty(dr["CreateTime"].ToString()) ? "null" : dr["CreateTime"]
                                     , dr["UID"].ToInt(), dr["UserName"].ToString().Replace("\'", "\""), string.IsNullOrEmpty(dr["ActivationTime"].ToString()) ? "null" : dr["ActivationTime"], string.IsNullOrEmpty(dr["CheckTime"].ToString()) ? "null" : dr["CheckTime"]
                                     , dr["CheckCount"].ToShort(), dr["OrderNO"].ToString(), dr["Type"].ToShort()
                                     , dr["Status"].ToShort(), dr["Remarks"].ToString().Replace("\'", "\""));
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
                    var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString().Replace("\'null\'", "null"));
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
                myLog.ErrorFormat("AddCouponShoppingCard 添加购物卡失败,购物卡ID：{0}-{1},异常信息:{2}", productTable.Rows[0]["CouponID"], productTable.Rows[productTable.Rows.Count - 1]["CouponID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        #endregion

        private string parmsKey = string.Format(@"CouponID,CardType,CardNo, CardPass,ProductID,ProductCode,TotalAmount,RemainingSum,StartTime,EndTime,Creator,CreateTime,UID,
                                                UserName,ActivationTime,CheckTime,CheckCount,OrderID,Type,Status,Remarks");
    }
}
