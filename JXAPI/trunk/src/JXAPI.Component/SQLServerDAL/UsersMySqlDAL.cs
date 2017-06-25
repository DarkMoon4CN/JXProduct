using JXAPI.Component.DataAccess;
using JXAPI.Component.Model;
using JXAPI.Common.Utils;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.SQLServerDAL
{
    public class UsersMySqlDAL
    {
        private static Database dbw = JXUsersMySqlData.Writer;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(UsersMySqlDAL));
        
        //  表参数
        private string usersParmMysql = "UID,UserName,Mobile,Password,LastLoginTime,SourceID,LoginCount,TodayLoginCount,Status,CreateTime,UpdateTime,UnionIden";
        private string usersParmMssql = "UID,UserName,Mobile,Password,LastLoginTime,SourceID,LoginCount,TodayLoginCount,Status,CreateTime,UpdateTime,UnionIden";

        private string usersbaseParmMysql = "UID,NickName,Avatar,TrueName,Sex,Birth,Mobile,CreateTime,UpdateTime,Rank,Score,ConsumeTotal,Remaining,SafeLevel,RebateFee,IsClaim,EmployeeID,PurchaseType,AgeRange,IsUpdateAvatar";
        private string usersbaseParmMssql = "UID,NickName,Avatar,TrueName,Sex,Birth,Mobile,CreateTime,UpdateTime,Rank,Score,ConsumeTotal,Remaining,SafeLevel,RebateFee,IsClaim,EmployeeID,PurchaseType,AgeRange,IsUpdateAvatar";

        private string receiverParmMysql = "ReceiverID,UID,TrueName,Province,City,County,Address,Telephone,Mobile,Postalcode,IsDefault,CreateTime,UpdateTime,PaymentMethodID,ShipMethodID,sendTimeInfo,splitType";
        private string receiverParmMssql = "ReceiverID,UID,TrueName,Province,City,County,Address,phone,Mobile,Postalcode,IsDefault,CreateTime,UpdateTime,PaymentTypeID,ShipTypeID,ShipTimeID,IsSplit";

        private string accountParmMysql = "AccountID,account,Deposit,RemainingSum,Creator,CreateTime,Remarks,Status,UpdateTime,OrderID";
        private string accountParmMssql = "Ident,UID,Deposit,RemainingSum,Creator,CreateTime,Remarks,Status,UpdateTime,OrderNO";

        private string scoreParmMysql = "ScoreID,UID,Score,ScoreType,Method,CreateTime,OrderID";
        private string scoreParmMssql = "ScoreID,UID,Score,ScoreType,GetMethod,CreateTime,OrderNO";

        /// <summary>
        /// 数据添加
        /// </summary>
        /// <param name="dTable">添加数据</param>
        /// <param name="tableName">表名称</param>
        /// <param name="errorCount">失败数量</param>
        /// <returns></returns>
        public bool AddUsers(DataTable dTable, string tableName, out int errorCount)
        {
            errorCount = dTable.Rows.Count;
            int n = 0;
            var flag = false;
            try
            {
                string parmMysql = string.Empty;
                string parmMssql = string.Empty;
                switch (tableName.ToLower())
                {
                    case "users":
                        parmMysql = usersParmMysql;
                        parmMssql = usersParmMssql;
                        #region 用户信息重构
                        string password = string.Empty;
                        foreach (DataRow row in dTable.Rows)
                        {
                            if (!string.IsNullOrEmpty(row["Password"].ToString()))
                            {
                                if (string.IsNullOrEmpty(row["GUID1"].ToString()))
                                {
                                    password = EncryptHelper.ToMD5(row["Password"].ToString().ToLower(), 32);
                                }
                                else
                                {
                                    try
                                    {
                                        var orgPassWd = EncryptHelper.Decrypt(row["Password"].ToString(), row["GUID1"].ToString(), row["GUID2"].ToString());   //  解密Sql中的密码
                                        var firstEncrypt = EncryptHelper.ToMD5(orgPassWd.ToLower(), 32);                            //  将解密的密码第一次进行转小写后进行MD5加密
                                        password = EncryptHelper.ToMD5(firstEncrypt.ToLower(), 32);                                 //  第二次进行转小写后进行MD5加密
                                    }
                                    catch (Exception ex)
                                    {
                                        myLog.Error(string.Format("用户：{0} 解密出错：{1}", row["UID"].ToString(), ex.Message));
                                    }
                                }
                            }
                            row["Password"] = password;
                            var MobilePhone = string.Empty;
                            if (string.IsNullOrEmpty(row["UserName"].ToString()))
                            {
                                row["UserName"] = DBNull.Value;
                            }
                            else
                            {
                                if (IsTelephone(row["UserName"].ToString()))
                                {
                                    MobilePhone = row["UserName"].ToString();
                                }
                            }
                            if (string.IsNullOrEmpty(MobilePhone))
                            {
                                row["Mobile"] = DBNull.Value;
                            }
                            else
                            {
                                row["Mobile"] = MobilePhone;
                            }
                        }
                        #endregion
                        break;
                    case "usersbase":
                        parmMysql = usersbaseParmMysql;
                        parmMssql = usersbaseParmMssql;
                        foreach (DataRow row in dTable.Rows)
                        {
                            n++;
                            row["NickName"] = CollectionHelper.CreateRandomNikeNameStr(n);
                        }
                        break;
                    case "receiver":
                        parmMysql = receiverParmMysql;
                        parmMssql = receiverParmMssql;
                        break;
                    case "score":
                        parmMysql = scoreParmMysql;
                        parmMssql = scoreParmMssql;
                        break;
                    case "account":
                        parmMysql = accountParmMysql;
                        parmMssql = accountParmMssql;
                        break;
                }
                //  构造数据
                StringBuilder strPlaceholder = new StringBuilder();
                foreach (DataRow row in dTable.Rows)
                {
                    n = 0;
                    if (strPlaceholder.Length > 0)
                        strPlaceholder.Append(",(");
                    else
                        strPlaceholder.Append("(");
                    foreach (string key in parmMssql.Split(",".ToCharArray()))
                    {
                        if (n == 0)
                            strPlaceholder.Append(string.Format("'{0}'", row[key].ToString()));
                        else
                        {
                            if (tableName.Contains("users") && row[key] == DBNull.Value && (key.Contains("UserName") || key.Contains("Mobile")))
                                strPlaceholder.Append(",null");
                            else
                                strPlaceholder.Append(string.Format(",'{0}'", row[key].ToString()));
                        }
                        n++;
                    }
                    strPlaceholder.Append(")");
                }

                //  构造SQL语句
                StringBuilder sqlCommand = new StringBuilder(string.Format("insert into {0} ({1}) values {2}", tableName, parmMysql, strPlaceholder.ToString()));
                var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString());
                var result = dbw.ExecuteNonQuery(cmd);
                if (result == dTable.Rows.Count)
                {
                    errorCount = 0;
                    flag = true;
                }
                else
                {
                    errorCount = dTable.Rows.Count - result;
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("Add {0} 失败 数据：{1}-{2} 异常：{3}", tableName, dTable.Rows[0][0], dTable.Rows[dTable.Rows.Count - 1][0], ex.Message);
            }
            return flag;
        }

        /// <summary>
        /// 数据更新
        /// </summary>
        /// <param name="dTable">更新数据</param>
        /// <param name="tableName">表名称</param>
        /// <param name="errorCount">失败数量</param>
        /// <returns></returns>
        public bool UpdateUsers(DataTable dTable, string tableName, out int errorCount)
        {
            errorCount = dTable.Rows.Count;
            int n = 0;
            var flag = false;
            try
            {
                string parmMysql = string.Empty;
                string parmMssql = string.Empty;
                switch (tableName.ToLower())
                {
                    case "users":
                        parmMysql = usersParmMysql;
                        parmMssql = usersParmMssql;
                        #region 用户信息重构
                        string password = string.Empty;
                        foreach (DataRow row in dTable.Rows)
                        {
                            if (string.IsNullOrEmpty(row["GUID1"].ToString()))
                            {
                                password = EncryptHelper.ToMD5(row["Password"].ToString().ToLower(), 32);
                            }
                            else
                            {
                                try
                                {
                                    var orgPassWd = EncryptHelper.Decrypt(row["Password"].ToString(), row["GUID1"].ToString(), row["GUID2"].ToString());   //  解密Sql中的密码
                                    var firstEncrypt = EncryptHelper.ToMD5(orgPassWd.ToLower(), 32);                            //  将解密的密码第一次进行转小写后进行MD5加密
                                    password = EncryptHelper.ToMD5(firstEncrypt.ToLower(), 32);                                 //  第二次进行转小写后进行MD5加密
                                }
                                catch (Exception ex)
                                {
                                    myLog.Error(string.Format("用户：{0} 解密出错：{1}", row["UID"].ToString(), ex.Message));
                                }
                            }
                            row["Password"] = password;
                            var MobilePhone = string.Empty;
                            if (string.IsNullOrEmpty(row["UserName"].ToString()))
                            {
                                row["UserName"] = DBNull.Value;
                            }
                            else
                            {
                                if (IsTelephone(row["UserName"].ToString()))
                                {
                                    MobilePhone = row["UserName"].ToString();
                                }
                            }
                            if (string.IsNullOrEmpty(MobilePhone))
                            {
                                row["Mobile"] = DBNull.Value;
                            }
                            else
                            {
                                row["Mobile"] = MobilePhone;
                            }
                        }
                        #endregion
                        break;
                    case "usersbase":
                        parmMysql = usersbaseParmMysql;
                        parmMssql = usersbaseParmMssql;
                        foreach (DataRow row in dTable.Rows)
                        {
                            n++;
                            row["NickName"] = CollectionHelper.CreateRandomNikeNameStr(n);
                        }
                        break;
                    case "receiver":
                        parmMysql = receiverParmMysql;
                        parmMssql = receiverParmMssql;
                        break;
                    case "score":
                        parmMysql = scoreParmMysql;
                        parmMssql = scoreParmMssql;
                        break;
                    case "account":
                        parmMysql = accountParmMysql;
                        parmMssql = accountParmMssql;
                        break;
                }
                //  构造数据
                StringBuilder strPlaceholder = new StringBuilder();
                foreach (DataRow row in dTable.Rows)
                {
                    n = 0;
                    if (strPlaceholder.Length > 0)
                        strPlaceholder.Append(",(");
                    else
                        strPlaceholder.Append("(");
                    foreach (string key in parmMssql.Split(",".ToCharArray()))
                    {
                        if (n == 0)
                            strPlaceholder.Append(string.Format("'{0}'", row[key].ToString()));
                        else
                        {
                            if (tableName.Contains("users") && row[key] == DBNull.Value && (key.Contains("UserName") || key.Contains("Mobile")))
                                strPlaceholder.Append(",null");
                            else
                                strPlaceholder.Append(string.Format(",'{0}'", row[key].ToString()));
                        }
                        n++;
                    }
                    strPlaceholder.Append(")");
                }

                //  构造SQL语句
                StringBuilder sqlCommand = new StringBuilder(string.Format("replace into {0} ({1}) values {2}", tableName, parmMysql, strPlaceholder.ToString()));
                var cmd = dbw.GetSqlStringCommand(sqlCommand.ToString());
                var result = dbw.ExecuteNonQuery(cmd);
                if (result == dTable.Rows.Count)
                {
                    errorCount = 0;
                    flag = true;
                }
                else
                {
                    errorCount = dTable.Rows.Count - result;
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("Update {0} 失败 数据：{1}-{2} 异常：{3}", tableName, dTable.Rows[0][0], dTable.Rows[dTable.Rows.Count - 1][0], ex.Message);
            }
            return flag;
        }

        private bool IsTelephone(string str_handset)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_handset, @"^((\+?86)|(\(\+86\)))?1\d{10}$");
        }
    }
}
