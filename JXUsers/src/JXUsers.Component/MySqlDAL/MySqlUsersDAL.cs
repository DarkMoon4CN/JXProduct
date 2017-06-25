using EFCommon.Context;
using JXUsers.Component.Enum;
using JXUsers.Component.Model.MySql;
using JXUsers.Component.Model.ResultModel;
using JXUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXUsers.Component.MySqlDAL
{
    public class MySqlUsersDAL
    {
        #region callInfo
        /// <summary>
        ///  条件查询用户信息 
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="phone">电话</param>
        /// <param name="trueName">真实姓名</param>
        /// <param name="isJoinMoblie">手机号是否联合查询 默认为 是</param>
        /// <returns></returns>
        public OperationResult<IList<UsersInfo>> Users_GetUserByParms(string mobile, string phone, string trueName, int userID, int isJoinMoblie = 1)
        {
            IList<UsersInfo> userInfoList = new List<UsersInfo>();
            try
            {
                using (var context = new MySqlProvider(DbConnectionName.MySqlUsersContext.ToString()))
                {
                    var sql = string.Empty;
                    sql += " SELECT u.UID,u.UserName,r.TrueName,r.Address,u.CreateTime,u.Status,u.Mobile ";
                    sql += " FROM Users u LEFT JOIN   Receiver r   ";
                    sql += " ON u.UID=r.UID   ";
                    sql += " INNER JOIN Usersbase b    ";
                    sql += "  on u.UID=b.UID   ";
                    sql += "  WHERE  1=1   ";
                    if (!string.IsNullOrEmpty(mobile) && mobile != "" && isJoinMoblie == 1)
                    {
                        sql += " AND  r.Mobile='{0}' OR u.Mobile='{1}' OR b.Mobile='{2}'";
                        sql = string.Format(sql, mobile, mobile, mobile, mobile);
                    }
                    else if (!string.IsNullOrEmpty(mobile) && mobile != "" && isJoinMoblie == 0)
                    {
                        sql += " AND   u.Mobile='{0}' ";
                        sql = string.Format(sql, mobile);
                    }
                    if (!string.IsNullOrEmpty(phone) && phone != "")
                    {
                        sql += " AND  r.Telephone LIKE '%{0}%' ";
                        sql = string.Format(sql, phone);
                    }
                    if (!string.IsNullOrEmpty(trueName) && trueName != "")
                    {
                        sql += " AND  r.TrueName LIKE '%{0}%'  ";
                        sql = string.Format(sql, trueName);
                    }
                    if (userID > 0)
                    {
                        sql += " AND  (r.UID ={0}  OR u.UID={1}) ";
                        sql = string.Format(sql, userID, userID);
                    }
                    sql += " GROUP BY u.UID ";
                    userInfoList = context.Database.SqlQuery<Model.ResultModel.UsersInfo>(sql).ToList();
                    return new OperationResult<IList<UsersInfo>>(OperationResultType.Success, null, userInfoList);
                }
            }
            catch (Exception ex) 
            {
                return new OperationResult<IList<UsersInfo>>(OperationResultType.Error, ex.Message, userInfoList);
            }

        }
        #endregion

        
    }
}
