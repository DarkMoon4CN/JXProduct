﻿using JXAPI.Component.DataAccess;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace JXAPI.Component.SQLServerDAL
{
    public class CouponBatchMySqlDAL
    {
        private static Database dbw = JXCouponBaseMySqlData.Writer;
        private static Database dbr = JXCouponBaseMySqlData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(CouponBatchMySqlDAL));

        #region MySql 优惠券生成批次表相关操作


        /// <summary>
        /// 查询优惠券生成批次最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxCouponBatchID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from couponbatch order by ID DESC limit 0, 1";
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
                myLog.ErrorFormat("GetMaxCouponBatchID 获取优惠券生成批次最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        /// <summary>
        /// 更新优惠券生成批次
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool UpdateCouponBatch(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into couponbatch ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}')",
                                     dr["ID"].ToInt(), dr["BatchID"].ToString().Replace("\'", "\""), dr["ChannelID"].ToInt(), dr["TypeID"].ToInt(), dr["NumCount"].ToInt()
                                     , dr["StartTime"].ToDateTime().ToString(), dr["EndTime"].ToDateTime().ToString()
                                     , dr["CreateTime"].ToDateTime().ToString(), dr["Creator"].ToString().Replace("\'", "\""), dr["Description"].ToString().Replace("\'", "\""));
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
                myLog.ErrorFormat("UpdateCouponBatch 更新优惠券生成批次表失败,优惠券生成批次ID：{0}-{1},异常信息:{2}", productTable.Rows[0]["BatchID"], productTable.Rows[productTable.Rows.Count - 1]["BatchID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 添加优惠券生成批次
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool AddCouponBatch(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into couponbatch ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                                     dr["ID"].ToInt(),dr["BatchID"].ToString().Replace("\'", "\""), dr["ChannelID"].ToInt(), dr["TypeID"].ToInt(), dr["NumCount"].ToInt()
                                     ,dr["StartTime"].ToDateTime().ToString(), dr["EndTime"].ToDateTime().ToString()
                                     , dr["CreateTime"].ToDateTime().ToString(), dr["Creator"].ToString().Replace("\'", "\""), dr["Description"].ToString().Replace("\'", "\""));
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
                myLog.ErrorFormat("AddCouponBatch 添加优惠券生成批次失败,优惠券生成批次ID：{0}-{1},异常信息:{2}", productTable.Rows[0]["BatchID"], productTable.Rows[productTable.Rows.Count - 1]["BatchID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        #endregion

        private string parmsKey = string.Format(@"ID,BatchID,ChannelID,TypeID, NumCount,StartTime,EndTime,CreateTime,Creator,Description");
    }
}