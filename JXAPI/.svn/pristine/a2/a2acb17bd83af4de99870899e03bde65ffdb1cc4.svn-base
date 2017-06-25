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
    public class UsersReceiverMySqlDAL
    {
        private static Database dbw = JXUsersMySqlData.Writer;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(UsersReceiverMySqlDAL));

        #region MySql 用户收货人信息表相关操作

        public int GetMaxUsersReceiverID()
        {
            int maxId = 0;
            try
            {
                string sqlCommand = "select * from receiver order by ReceiverID DESC limit 0, 1";
                var cmd = dbw.GetSqlStringCommand(sqlCommand);
                if (dbw.ExecuteScalar(cmd).IsNotNULL())
                {
                    maxId = Convert.ToInt32(dbw.ExecuteScalar(cmd));
                }
                else
                {
                    maxId = 0;
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("GetMaxUsersReceiverID 获取品牌信息最大ID失败,用户收货人信息:{0}", ex.Message);
                maxId = -1;
            }
            return maxId;
        }

        public bool UpdateUsersReceiver(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("replace into receiver ( " + parmsKey + " ) values ");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var dr = table.Rows[i];
                    var Placeholder = string.Format(@"({0},{1},'{2}',{3},{4},{5},'{6}','{7}','{8}','{9}',{10},'{11}','{12}',{13},{14},{15},{16})",
                                     dr["ReceiverID"].ToInt(), dr["UID"].ToInt(), dr["TrueName"].ToString().Replace("\'", "\""), dr["Province"].ToString().Replace("\'", "\"")
                                     , dr["City"].ToString().Replace("\'", "\""), dr["County"].ToString().Replace("\'", "\""), dr["Address"].ToString().Replace("\'", "\""), dr["Phone"].ToString().Replace("\'", "\"")
                                     , dr["Mobile"].ToString().Replace("\'", "\""), dr["Postalcode"].ToString().Replace("\'", "\""), dr["IsDefault"].ToShort(), dr["CreateTime"].ToDateTime().ToString()
                                     , dr["UpdateTime"].ToDateTime().ToString(), dr["PaymentTypeID"].ToShort(), dr["ShipTypeID"].ToShort(), dr["ShipTimeID"].ToShort()
                                     , dr["IsSplit"].ToShort());
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
                        if (result != table.Rows.Count)
                        {
                            errorCount = table.Rows.Count - result;
                            flag = false;
                        }
                        else
                        {
                            errorCount = 0;
                            flag = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("UpdateUsersReceiver 更新品牌信息表失败,品牌信息ID：{0}-{1},用户收货人信息:{2}", table.Rows[0]["ReceiverID"], table.Rows[table.Rows.Count - 1]["ReceiverID"], ex.Message);
                flag = false;
            }
            return flag;
        }

        public bool AddUsersReceiver(DataTable table, out int errorCount)
        {
            var flag = true;
            errorCount = 0;
            try
            {
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("insert into receiver ( " + parmsKey + " ) values ");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var dr = table.Rows[i];
                    var Placeholder = string.Format(@"({0},{1},'{2}',{3},{4},{5},'{6}','{7}','{8}','{9}',{10},'{11}','{12}',{13},{14},{15},{16})",
                                     dr["ReceiverID"].ToInt(), dr["UID"].ToInt(), dr["TrueName"].ToString().Replace("\'", "\""), dr["Province"].ToString().Replace("\'", "\"")
                                     , dr["City"].ToString().Replace("\'", "\""), dr["County"].ToString().Replace("\'", "\""), dr["Address"].ToString().Replace("\'", "\""), dr["Phone"].ToString().Replace("\'", "\"")
                                     , dr["Mobile"].ToString().Replace("\'", "\""), dr["Postalcode"].ToString().Replace("\'", "\""), dr["IsDefault"].ToShort(), dr["CreateTime"].ToDateTime().ToString()
                                     , dr["UpdateTime"].ToDateTime().ToString(), dr["PaymentTypeID"].ToShort(), dr["ShipTypeID"].ToShort(), dr["ShipTimeID"].ToShort()
                                     , dr["IsSplit"].ToShort());
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
                        if (result != table.Rows.Count)
                        {
                            errorCount = table.Rows.Count - result;
                            flag = false;
                        }
                        else
                        {
                            errorCount = 0;
                            flag = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("AddUsersReceiver 添加品牌信息失败,品牌信息ID：{0}-{1},用户收货人信息:{2}", table.Rows[0]["ReceiverID"], table.Rows[table.Rows.Count - 1]["ReceiverID"], ex.Message);
                flag = false;
            }
            return flag;
        }


        private string parmsKey = string.Format(@"ReceiverID,UID,TrueName,Province,City,County,Address,Telephone,
                             Mobile,Postalcode,IsDefault,CreateTime,UpdateTime,PaymentMethodID,ShipMethodID,sendTimeInfo,splitType");

        #endregion
    }
}
