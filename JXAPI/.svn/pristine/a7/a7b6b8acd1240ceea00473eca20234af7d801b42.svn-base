using JXAPI.Component.DataAccess;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.SQLServerDAL
{
    public class CouponChannelMySqlDAL
    {
        private static Database dbw = JXCouponBaseMySqlData.Writer;
        private static Database dbr = JXCouponBaseMySqlData.Reader;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(CouponChannelMySqlDAL));

        #region MySql 优惠券渠表相关操作


        /// <summary>
        /// 查询优惠券渠最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxCouponChannelID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from couponchannel order by ID DESC limit 0, 1";
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
                myLog.ErrorFormat("GetMaxCouponChannelID 获取优惠券渠最大ID失败,异常信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        /// <summary>
        /// 更新优惠券渠
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool UpdateCouponChannel(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into couponchannel ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},'{1}','{2}','{3}','{4}','{5}',{6})",
                                     dr["ChannelID"].ToInt(), dr["ChannelName"].ToString().Replace("\'", "\""), dr["SpellName"].ToString().Replace("\'", "\""), dr["Description"].ToString().Replace("\'", "\"")
                                     , dr["CreateTime"].ToDateTime().ToString(), dr["Creator"].ToString().Replace("\'", "\""), dr["IsOuter"].ToShort());
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
                myLog.ErrorFormat("UpdateCouponChannel 更新优惠券渠表失败,优惠券渠ID：{0}-{1},异常信息:{2}", productTable.Rows[0]["ChannelID"], productTable.Rows[productTable.Rows.Count - 1]["ChannelID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 添加优惠券渠
        /// </summary>
        /// <param name="productTable"></param>
        /// <param name="errorCount"></param>
        /// <returns></returns>
        public bool AddCouponChannel(DataTable productTable, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into couponchannel ( " + parmsKey + " ) values ");
                for (int i = 0; i < productTable.Rows.Count; i++)
                {
                    var dr = productTable.Rows[i];
                    var Placeholder = string.Format(@"({0},'{1}','{2}','{3}','{4}','{5}',{6})",
                                     dr["ChannelID"].ToInt(), dr["ChannelName"].ToString().Replace("\'", "\""), dr["SpellName"].ToString().Replace("\'", "\""), dr["Description"].ToString().Replace("\'", "\"")
                                     , dr["CreateTime"].ToDateTime().ToString(), dr["Creator"].ToString().Replace("\'", "\""), dr["IsOuter"].ToShort());
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
                myLog.ErrorFormat("AddCouponChannel 添加优惠券渠失败,优惠券渠ID：{0}-{1},异常信息:{2}", productTable.Rows[0]["ChannelID"], productTable.Rows[productTable.Rows.Count - 1]["ChannelID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        #endregion

        private string parmsKey = string.Format(@"ChannelID,ChannelName,SpellName, Description,CreateTime,Creator,IsOuter");
    }
}
