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
    public class UsersScoreMySqlDAL
    {
        private static Database dbw = JXUsersMySqlData.Writer;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(UsersScoreMySqlDAL));

        #region MySql 用户积分信息表相关操作

        /// <summary>
        /// 删除用户积分信息表信息
        /// </summary>
        /// <returns></returns>
        public bool DeleteUsersScoreInfo()
        {
            bool flag = true;
            try
            {
                var sql = @"delete from score";
                var cmd = dbw.GetSqlStringCommand(sql);
                dbw.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                myLog.InfoFormat("DeleteUsersScoreReceiverInfo 删除用户积分信息失败,异常信息:{0}", ex.Message);
                flag = false;
            }
            return flag;
        }

        ///// <summary>
        ///// 添加用户积分信息
        ///// </summary>
        ///// <param name="UsersReceiver">用户积分信息</param>
        ///// <returns>true or false</returns>
        public bool AddUsersScoreInfo(UsersScoreInfo usersScoreInfo)
        {
            bool flag = true;
            try
            {
                string parmsKey = string.Empty;
                string strPlaceholder = string.Empty;
                parmsKey = @" ScoreID,UID,Score,AvailableScore,ScoreType,OrderID,Method,CreateTime";
                            
                strPlaceholder = " @ScoreID,@UID,@Score,@AvailableScore,@ScoreType,@OrderID,@Method,@CreateTime";
                var sql = " INSERT INTO score ";
                sql += " ( ";
                sql += parmsKey;
                sql += " ) ";
                sql += " VALUES ";
                sql += string.Format(" (" + strPlaceholder + ") ");
                var cmd = dbw.GetSqlStringCommand(sql);
                dbw.AddInParameter(cmd, "ScoreID", DbType.Int32, usersScoreInfo.ScoreID);
                dbw.AddInParameter(cmd, "UID", DbType.Int32, usersScoreInfo.UID);
                dbw.AddInParameter(cmd, "Score", DbType.Int32, usersScoreInfo.Score);
                dbw.AddInParameter(cmd, "AvailableScore", DbType.Int32, 0);
                dbw.AddInParameter(cmd, "ScoreType", DbType.Int16, usersScoreInfo.ScoreType);

                dbw.AddInParameter(cmd, "OrderID", DbType.Int16, usersScoreInfo.OrderID);
                dbw.AddInParameter(cmd, "Method", DbType.String, usersScoreInfo.GetMethod);
                dbw.AddInParameter(cmd, "CreateTime", DbType.DateTime, usersScoreInfo.CreateTime);
                var result = dbw.ExecuteNonQuery(cmd);
                if (result > 0)
                {
                    flag = true;
                }
                else
                {
                    myLog.InfoFormat("AddUsersScoreInfo 添加用户积分信息失败,用户ID:{0},受影响的行数:{1}", usersScoreInfo.ScoreID, result);
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("AddUsersScoreInfo 添加用户积分信息失败,用户ID:{0},异常信息:{1}", usersScoreInfo.ScoreID, ex.Message);
                flag = false;
            }
            return flag;
        }

        #endregion
    }
}
