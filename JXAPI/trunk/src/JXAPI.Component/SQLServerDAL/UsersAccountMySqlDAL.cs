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
    public class UsersAccountMySqlDAL
    {
        private static Database dbw = JXUsersMySqlData.Writer;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(UsersAccountMySqlDAL));

        #region MySql 用户帐户信息表相关操作

        /// <summary>
        /// 删除用户帐户信息表信息
        /// </summary>
        /// <returns></returns>
        public bool DeleteUsersAccountInfo()
        {
            bool flag = true;
            try
            {
                var sql = @"delete from account";
                var cmd = dbw.GetSqlStringCommand(sql);
                dbw.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                myLog.InfoFormat("DeleteUsersAccountReceiverInfo 删除用户帐户信息失败,异常信息:{0}", ex.Message);
                flag = false;
            }
            return flag;
        }

        ///// <summary>
        ///// 添加用户帐户信息
        ///// </summary>
        ///// <param name="UsersReceiver">用户帐户信息</param>
        ///// <returns>true or false</returns>
        public bool AddUsersAccountInfo(UsersAccountInfo usersAccountInfo)
        {
            bool flag = true;
            try
            {
                string parmsKey = string.Empty;
                string strPlaceholder = string.Empty;
                parmsKey = @" AccountID,account,Deposit,RemainingSum,Creator,CreateTime,Remarks,Status,UpdateTime,OrderID";

                strPlaceholder = " @AccountID,@account,@Deposit,@RemainingSum,@Creator,@CreateTime,@Remarks,@Status,@UpdateTime,@OrderID";
                var sql = " INSERT INTO account ";
                sql += " ( ";
                sql += parmsKey;
                sql += " ) ";
                sql += " VALUES ";
                sql += string.Format(" (" + strPlaceholder + ") ");
                var cmd = dbw.GetSqlStringCommand(sql);
                dbw.AddInParameter(cmd, "AccountID", DbType.Int32, usersAccountInfo.Ident);
                dbw.AddInParameter(cmd, "account", DbType.Int32, usersAccountInfo.UID);
                dbw.AddInParameter(cmd, "Deposit", DbType.Decimal, usersAccountInfo.Deposit);
                dbw.AddInParameter(cmd, "RemainingSum", DbType.Decimal, usersAccountInfo.RemainingSum);
                dbw.AddInParameter(cmd, "Creator", DbType.String, usersAccountInfo.Creator);

                dbw.AddInParameter(cmd, "CreateTime", DbType.DateTime, usersAccountInfo.CreateTime);
                dbw.AddInParameter(cmd, "Remarks", DbType.String, usersAccountInfo.Remarks);
                dbw.AddInParameter(cmd, "Status", DbType.Int16, usersAccountInfo.Status);
                dbw.AddInParameter(cmd, "UpdateTime", DbType.DateTime, usersAccountInfo.UpdateTime);
                dbw.AddInParameter(cmd, "OrderID", DbType.Int16, usersAccountInfo.OrderID);
                var result = dbw.ExecuteNonQuery(cmd);
                if (result > 0)
                {
                    flag = true;
                }
                else
                {
                    myLog.InfoFormat("AddUsersAccountInfo 添加用户帐户信息失败,用户ID:{0},受影响的行数:{1}", usersAccountInfo.Ident, result);
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("AddUsersAccountInfo 添加用户帐户信息失败,用户ID:{0},异常信息:{1}", usersAccountInfo.Ident, ex.Message);
                flag = false;
            }
            return flag;
        }

        #endregion
    }
}
