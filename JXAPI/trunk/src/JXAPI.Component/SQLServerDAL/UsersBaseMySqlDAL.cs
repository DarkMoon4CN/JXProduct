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
    public class UsersBaseMySqlDAL
    {
        private static Database dbw = JXUsersMySqlData.Writer;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(UsersBaseMySqlDAL));

        #region MySql 用户基本信息表表相关操作

        /// <summary>
        /// 删除用户基本信息
        /// </summary>
        /// <returns></returns>
        public bool DeleteUsersBaseInfo()
        {
            bool flag = true;
            try
            {
                var sql = @"delete from UsersBase";
                var cmd = dbw.GetSqlStringCommand(sql);
                dbw.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                myLog.InfoFormat("DeleteUsersBaseInfo 删除用户基本信息失败,异常信息:{0}", ex.Message);
                flag = false;
            }
            return flag;
        }

        ///// <summary>
        ///// 添加用户基本信息
        ///// </summary>
        ///// <param name="usersBaseInfo">用户基本信息</param>
        ///// <returns>true or false</returns>
        public bool AddUsersBaseInfo(UsersBaseInfo usersBaseInfo)
        {
            bool flag = true;
            try
            {
                string parmsKey = string.Empty;
                string strPlaceholder = string.Empty;
                parmsKey = @" UID,NickName,Avatar,TrueName,Sex,Birth,Mobile,CreateTime,UpdateTime,Rank,Score,ConsumeTotal,
                             Remaining,SafeLevel,RebateFee,IsClaim,EmployeeID,PurchaseType,AgeRange,IsUpdateAvatar";

                strPlaceholder = " @UID,@NickName,@Avatar,@TrueName,@Sex,@Birth,@Mobile,@CreateTime,@UpdateTime,@Rank,@Score,@ConsumeTotal,";
                strPlaceholder += "@Remaining,@SafeLevel,@RebateFee,@IsClaim,@EmployeeID,@PurchaseType,@AgeRange,@IsUpdateAvatar";
                var sql = " INSERT INTO UsersBase ";
                sql += " ( ";
                sql += parmsKey;
                sql += " ) ";
                sql += " VALUES ";
                sql += string.Format(" (" + strPlaceholder + ") ");

                var sex = 0;
                if (!string.IsNullOrEmpty(usersBaseInfo.Sex))
                {
                    sex = usersBaseInfo.Sex.ToInt();
                }
                var cmd = dbw.GetSqlStringCommand(sql);
                dbw.AddInParameter(cmd, "UID", DbType.Int32, usersBaseInfo.UID);
                dbw.AddInParameter(cmd, "NickName", DbType.String, CollectionHelper.CreateRandomNikeNameStr(1));
                dbw.AddInParameter(cmd, "Avatar", DbType.String, imgUrl);
                if (string.IsNullOrEmpty(usersBaseInfo.TrueName))
                {
                    dbw.AddInParameter(cmd, "TrueName", DbType.String, "");
                }
                else
                {
                    dbw.AddInParameter(cmd, "TrueName", DbType.String, usersBaseInfo.TrueName);
                }
                dbw.AddInParameter(cmd, "Sex", DbType.Int16, sex);
                dbw.AddInParameter(cmd, "Birth", DbType.Date, usersBaseInfo.Birth);
                dbw.AddInParameter(cmd, "Mobile", DbType.String, usersBaseInfo.Mobile);
                dbw.AddInParameter(cmd, "CreateTime", DbType.DateTime, usersBaseInfo.CreateTime);
                dbw.AddInParameter(cmd, "UpdateTime", DbType.DateTime, usersBaseInfo.UpdateTime);
                dbw.AddInParameter(cmd, "Rank", DbType.Int16, 0);

                dbw.AddInParameter(cmd, "Score", DbType.Int32, 0);
                dbw.AddInParameter(cmd, "ConsumeTotal", DbType.Decimal, 0);
                dbw.AddInParameter(cmd, "Remaining", DbType.Decimal, 0);
                dbw.AddInParameter(cmd, "SafeLevel", DbType.Int16, 0);
                dbw.AddInParameter(cmd, "RebateFee", DbType.Decimal, 0);
                dbw.AddInParameter(cmd, "IsClaim", DbType.Int16, 0);
                dbw.AddInParameter(cmd, "EmployeeID", DbType.Int32, 0);
                dbw.AddInParameter(cmd, "PurchaseType", DbType.Int16, 0);

                dbw.AddInParameter(cmd, "AgeRange", DbType.Int16, 0);
                dbw.AddInParameter(cmd, "IsUpdateAvatar", DbType.Int16, 0);
                var result = dbw.ExecuteNonQuery(cmd);
                if (result > 0)
                {
                    flag = true;
                }
                else
                {
                    myLog.InfoFormat("AddUsersBaseInfo 添加用户基本信息失败,用户基本信息ID:{0},受影响的行数:{1}", usersBaseInfo.UID, result);
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                myLog.ErrorFormat("AddUsersBaseInfo 添加用户基本信息失败,用户基本信息ID:{0},异常信息:{1}", usersBaseInfo.UID, ex.Message);
                flag = false;
            }
            return flag;
        }

        private const string imgUrl = "3bd9/5b8/e74a/e4da632e8_L.jpg";
        #endregion

       
    }
}
