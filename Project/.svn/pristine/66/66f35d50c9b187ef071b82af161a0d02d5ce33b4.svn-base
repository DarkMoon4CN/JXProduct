using JXProduct.Component.DataAccess;
using JXProduct.Component.Enums;
using JXProduct.Component.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace JXProduct.Component.SQLServerDAL
{
    public class RoleMessagesDAL
    {
        private static Database dbr = JXMarketingData.Writer;
        private static Database dbw = JXMarketingData.Reader;

        /// <summary>
        /// RoleMessages_Insert Method		
        /// </summary>
        /// <param name="RoleMessagesInfo">RoleMessages object</param>
        /// <returns></returns>
        public int RoleMessages_Insert(RoleMessagesInfo roleMessageInfo)
        {
            string sqlCommand = "RoleMessages_Insert";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddOutParameter(dbCommand, "MessageID", DbType.Int32, 4);
            dbw.AddInParameter(dbCommand, "RoleID", DbType.Int32, roleMessageInfo.RoleID);
            dbw.AddInParameter(dbCommand, "RoleName", DbType.String, roleMessageInfo.RoleName);

            dbw.AddInParameter(dbCommand, "PID", DbType.Int32, roleMessageInfo.PID);
            dbw.AddInParameter(dbCommand, "Title", DbType.String, roleMessageInfo.Title);
            dbw.AddInParameter(dbCommand, "Contents", DbType.String, roleMessageInfo.Contents);

            dbw.AddInParameter(dbCommand, "TypeID", DbType.Int32, roleMessageInfo.TypeID);
            dbw.AddInParameter(dbCommand, "Status", DbType.Int16, roleMessageInfo.Status);

            dbw.AddInParameter(dbCommand, "CreatorID", DbType.Int32, roleMessageInfo.CreatorID);
            dbw.AddInParameter(dbCommand, "Creator", DbType.String, roleMessageInfo.Creator);
            dbw.AddInParameter(dbCommand, "CreateTime", DbType.DateTime, roleMessageInfo.CreateTime);

            dbw.AddInParameter(dbCommand, "Updater", DbType.String, roleMessageInfo.Updater);
            dbw.AddInParameter(dbCommand, "UpdateTime", DbType.DateTime, roleMessageInfo.UpdateTime);

            dbw.ExecuteNonQuery(dbCommand);

            return int.Parse(dbw.GetParameterValue(dbCommand, "MessageID").ToString());
        }

        /// <summary>
        /// RoleMessages_Update Method
        /// </summary>
        /// <param name="RoleMessagesInfo">RoleMessages object</param>
        /// <returns>true:成功 false:失败</returns>
        public bool RoleMessages_Update(RoleMessagesInfo roleMessageInfo)
        {
            string sqlCommand = "RoleMessages_Update";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddInParameter(dbCommand, "MessageID", DbType.Int32, roleMessageInfo.MessageID);
            dbw.AddInParameter(dbCommand, "RoleID", DbType.Int32, roleMessageInfo.RoleID);
            dbw.AddInParameter(dbCommand, "RoleName", DbType.String, roleMessageInfo.RoleName);
            dbw.AddInParameter(dbCommand, "CreatorID", DbType.Int32, roleMessageInfo.CreatorID);
            dbw.AddInParameter(dbCommand, "Creator", DbType.String, roleMessageInfo.Creator);
            dbw.AddInParameter(dbCommand, "CreateTime", DbType.DateTime, roleMessageInfo.CreateTime);
            dbw.AddInParameter(dbCommand, "Status", DbType.Int16, roleMessageInfo.Status);
            dbw.AddInParameter(dbCommand, "Contents", DbType.String, roleMessageInfo.Contents);
            dbw.AddInParameter(dbCommand, "Title", DbType.String, roleMessageInfo.Title);
            dbw.AddInParameter(dbCommand, "PID", DbType.Int32, roleMessageInfo.PID);
            dbw.AddInParameter(dbCommand, "TypeID", DbType.Int32, roleMessageInfo.TypeID);
            dbw.AddInParameter(dbCommand, "Updater", DbType.String, roleMessageInfo.Updater);
            dbw.AddInParameter(dbCommand, "UpdateTime", DbType.DateTime, roleMessageInfo.UpdateTime);

            dbw.ExecuteNonQuery(dbCommand);

            return true;

        }

        /// <summary>
        /// RoleMessages_Get Method
        /// </summary>
        /// <param name="messageID">RoleMessages Main ID</param>
        /// <returns>RoleMessagesInfo get from RoleMessages table.</returns>	
        public RoleMessagesInfo RoleMessages_Get(int messageID)
        {
            RoleMessagesInfo roleMessageInfo = null;

            string sqlCommand = "RoleMessages_Get";
            DbCommand dbCommand = dbr.GetStoredProcCommand(sqlCommand);

            dbr.AddInParameter(dbCommand, "MessageID", DbType.Int32, messageID);

            using (IDataReader dataReader = dbr.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    roleMessageInfo = RecoverModel(dataReader);
                }
            }

            return roleMessageInfo;
        }

        /// <summary>
        /// RoleMessages_Delete Method
        /// </summary>
        /// <param name="messageID">RoleMessages Main ID</param>
        /// <returns>true:成功 false:失败</returns>	
        public bool RoleMessages_Delete(int messageID)
        {
            string sqlCommand = "RoleMessages_Delete";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddInParameter(dbCommand, "MessageID", DbType.Int32, messageID);

            dbw.ExecuteNonQuery(dbCommand);

            return true;
        }

        /// <summary>
        /// RoleMessages_GetList Method
        /// </summary>
        /// <param name="pageIndex">起始页码</param>
        /// <param name="pageSize">每页数据数</param>
        /// <param name="orderType">设置排序，'':没有排序要求 0：主键升序 1：主键降序 字符串：用户自定义排序规则 如：‘SubmitTime DESC , ID DESC’</param>
        /// <param name="strWhere">查询条件(注意: 不要加 WHERE)</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>A Generic List of RoleMessagesInfo</returns>
        public IList<RoleMessagesInfo> RoleMessages_GetList(int pageIndex, int pageSize, string orderType, string strWhere, out int recordCount)
        {
            IList<RoleMessagesInfo> roleMessageInfoList = new List<RoleMessagesInfo>();

            string sqlCommand = "RoleMessages_GetList";
            DbCommand dbCommand = dbr.GetStoredProcCommand(sqlCommand);

            dbr.AddInParameter(dbCommand, "PageIndex", DbType.Int32, pageIndex);
            dbr.AddInParameter(dbCommand, "PageSize", DbType.Int32, pageSize);
            dbr.AddInParameter(dbCommand, "OrderType", DbType.String, orderType);
            dbr.AddInParameter(dbCommand, "StrWhere", DbType.String, strWhere);
            dbr.AddOutParameter(dbCommand, "RecordCount", DbType.Int32, 4);

            using (IDataReader dataReader = dbr.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    roleMessageInfoList.Add(RecoverModel(dataReader));
                }
            }

            recordCount = int.Parse(dbr.GetParameterValue(dbCommand, "RecordCount").ToString());

            return roleMessageInfoList;
        }

        /// <summary>
        /// 从 IDataReader 中恢复RoleMessages对象
        /// </summary>
        /// <param name="IDataReader"></param>
        /// <returns></returns>
        private RoleMessagesInfo RecoverModel(IDataReader dataReader)
        {
            RoleMessagesInfo roleMessageInfo = new RoleMessagesInfo();

            roleMessageInfo.MessageID = int.Parse(dataReader["MessageID"].ToString());
            roleMessageInfo.RoleID = int.Parse(dataReader["RoleID"].ToString());
            roleMessageInfo.RoleName = dataReader["RoleName"].ToString();
            roleMessageInfo.CreatorID = int.Parse(dataReader["CreatorID"].ToString());
            roleMessageInfo.Creator = dataReader["Creator"].ToString();
            roleMessageInfo.CreateTime = DateTime.Parse(dataReader["CreateTime"].ToString());
            roleMessageInfo.Status = short.Parse(dataReader["Status"].ToString());
            roleMessageInfo.Contents = dataReader["Contents"].ToString();
            roleMessageInfo.Title = dataReader["Title"].ToString();
            roleMessageInfo.PID = int.Parse(dataReader["PID"].ToString());
            roleMessageInfo.TypeID = int.Parse(dataReader["TypeID"].ToString());
            roleMessageInfo.Updater = dataReader["Updater"].ToString();
            roleMessageInfo.UpdateTime = DateTime.Parse(dataReader["UpdateTime"].ToString());

            return roleMessageInfo;
        }


        /// <summary>
        /// 是不是37_1的进度
        /// </summary>
        /// <param name="roleID">部分ID</param>
        /// <returns> true 存在  false 不存在</returns>
        public bool RoleMessages_IsExist(int roleID, int pid)
        {
            try
            {
                string sql = " SELECT COUNT(*) FROM RoleMessages ";
                sql += " WHERE RoleID={0} AND PID={1}";
                sql = string.Format(sql, roleID, pid);
                DbCommand dbCommand = dbr.GetSqlStringCommand(sql);
                int count = Convert.ToInt32(dbr.ExecuteScalar(dbCommand));
                if (count == 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// 获取出未读消息总条数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int RoleMessages_Count(string strWhere)
        {
            int count = 0;
            string sql = string.Empty;
            sql += " SELECT COUNT(*) FROM RoleMessages  WHERE Status=0  ";
            sql += strWhere;
            DbCommand dbCommand = dbr.GetSqlStringCommand(sql);
            count = Convert.ToInt32(dbr.ExecuteScalar(dbCommand));
            return count;
        }

        public bool RoleMessages_SetProcessed(int roleID, int productid)
        {
            var sql = string.Format("UPDATE rm SET rm .[Status] = 1 FROM JXMarketing.dbo.RoleMessages AS rm WHERE rm.RoleID ={0} AND rm.PID ={1} AND rm.[Status] = 0", roleID, productid);
            var cmd = dbw.GetSqlStringCommand(sql);
            var result = dbw.ExecuteNonQuery(cmd);
            return result > 0;
        }
    }
}
