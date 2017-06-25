using JXProduct.Component.DataAccess;
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
    internal class ActivityDAL
    {
        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;

        #region CURD


        /// <summary>
        /// Activity_Insert Method		
        /// </summary>
        /// <param name="ActivityInfo">Activity object</param>
        /// <returns></returns>
        public int Activity_Insert(ActivityInfo activityInfo)
        {
            string sqlCommand = "Activity_Insert";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddOutParameter(dbCommand, "ActID", DbType.Int32, 4);
            dbw.AddInParameter(dbCommand, "BriefName", DbType.String, activityInfo.BriefName);
            dbw.AddInParameter(dbCommand, "Name", DbType.String, activityInfo.Name);
            dbw.AddInParameter(dbCommand, "Description", DbType.String, activityInfo.Description);
            dbw.AddInParameter(dbCommand, "URL", DbType.String, activityInfo.URL);
            dbw.AddInParameter(dbCommand, "Type", DbType.Int16, activityInfo.Type);
            dbw.AddInParameter(dbCommand, "Limit", DbType.Int16, activityInfo.Limit);
            dbw.AddInParameter(dbCommand, "CountLimit", DbType.Int32, activityInfo.CountLimit);
            dbw.AddInParameter(dbCommand, "UserLimit", DbType.Int32, activityInfo.UserLimit);
            dbw.AddInParameter(dbCommand, "StartTime", DbType.DateTime, activityInfo.StartTime);
            dbw.AddInParameter(dbCommand, "EndTime", DbType.DateTime, activityInfo.EndTime);

            dbw.AddInParameter(dbCommand, "CreateTime", DbType.DateTime, activityInfo.CreateTime);
            dbw.AddInParameter(dbCommand, "Creator", DbType.String, activityInfo.Creator);
            dbw.AddInParameter(dbCommand, "Status", DbType.Int16, activityInfo.Status);
            dbw.AddInParameter(dbCommand, "UserType", DbType.Int16, activityInfo.UserType);

            dbw.ExecuteNonQuery(dbCommand);

            return int.Parse(dbw.GetParameterValue(dbCommand, "ActID").ToString());
        }

        /// <summary>
        /// Activity_Update Method
        /// </summary>
        /// <param name="ActivityInfo">Activity object</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Activity_Update(ActivityInfo activityInfo)
        {
            string sqlCommand = "Activity_Update";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddInParameter(dbCommand, "ActID", DbType.Int32, activityInfo.ActID);
            dbw.AddInParameter(dbCommand, "BriefName", DbType.String, activityInfo.BriefName);
            dbw.AddInParameter(dbCommand, "Name", DbType.String, activityInfo.Name);
            dbw.AddInParameter(dbCommand, "Description", DbType.String, activityInfo.Description);
            dbw.AddInParameter(dbCommand, "URL", DbType.String, activityInfo.URL);
            dbw.AddInParameter(dbCommand, "Type", DbType.Int16, activityInfo.Type);
            dbw.AddInParameter(dbCommand, "Limit", DbType.Int16, activityInfo.Limit);
            dbw.AddInParameter(dbCommand, "CountLimit", DbType.Int32, activityInfo.CountLimit);
            dbw.AddInParameter(dbCommand, "UserLimit", DbType.Int32, activityInfo.UserLimit);
            dbw.AddInParameter(dbCommand, "StartTime", DbType.DateTime, activityInfo.StartTime);
            dbw.AddInParameter(dbCommand, "EndTime", DbType.DateTime, activityInfo.EndTime);

            dbw.AddInParameter(dbCommand, "Updater", DbType.String, activityInfo.Updater);
            dbw.AddInParameter(dbCommand, "UpdateTime", DbType.DateTime, activityInfo.UpdateTime);
            dbw.AddInParameter(dbCommand, "Status", DbType.Int16, activityInfo.Status);
            dbw.AddInParameter(dbCommand, "UserType", DbType.Int16, activityInfo.UserType);

            dbw.ExecuteNonQuery(dbCommand);

            return true;

        }

        /// <summary>
        /// Activity_Get Method
        /// </summary>
        /// <param name="actID">Activity Main ID</param>
        /// <returns>ActivityInfo get from Activity table.</returns>	
        public ActivityInfo Activity_Get(int actID)
        {
            ActivityInfo activityInfo = null;

            string sqlCommand = "Activity_Get";
            DbCommand dbCommand = dbr.GetStoredProcCommand(sqlCommand);

            dbr.AddInParameter(dbCommand, "ActID", DbType.Int32, actID);

            using (IDataReader dataReader = dbr.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    activityInfo = RecoverModel(dataReader);
                }
            }

            return activityInfo;
        }

        /// <summary>
        /// Activity_Delete Method
        /// </summary>
        /// <param name="actID">Activity Main ID</param>
        /// <returns>true:成功 false:失败</returns>	
        public bool Activity_SetStatus(int actID, int status)
        {
            string sqlCommand = string.Format("UPDATE a SET a.[Status] ={1},UpdateTime =GetDate() FROM Activity AS a WHERE a.ActID ={0}", actID, status == 1 ? 0 : 1);
            DbCommand dbCommand = dbw.GetSqlStringCommand(sqlCommand);
            return dbw.ExecuteNonQuery(dbCommand) > 0;
        }

        /// <summary>
        /// Activity_GetList Method
        /// </summary>
        /// <param name="pageIndex">起始页码</param>
        /// <param name="pageSize">每页数据数</param>
        /// <param name="orderType">设置排序，'':没有排序要求 0：主键升序 1：主键降序 字符串：用户自定义排序规则 如：‘SubmitTime DESC , ID DESC’</param>
        /// <param name="strWhere">查询条件(注意: 不要加 WHERE)</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>A Generic List of ActivityInfo</returns>
        public IList<ActivityInfo> Activity_GetList(int pageIndex, int pageSize, string orderType, string strWhere, out int recordCount)
        {
            IList<ActivityInfo> activityInfoList = new List<ActivityInfo>();

            string sqlCommand = "Activity_GetList";
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
                    activityInfoList.Add(RecoverModel(dataReader));
                }
            }

            recordCount = int.Parse(dbr.GetParameterValue(dbCommand, "RecordCount").ToString());

            return activityInfoList;
        }

        /// <summary>
        /// 从 IDataReader 中恢复Activity对象
        /// </summary>
        /// <param name="IDataReader"></param>
        /// <returns></returns>
        private ActivityInfo RecoverModel(IDataReader dataReader)
        {
            ActivityInfo activityInfo = new ActivityInfo();

            activityInfo.ActID = int.Parse(dataReader["ActID"].ToString());
            activityInfo.BriefName = dataReader["BriefName"].ToString();
            activityInfo.Name = dataReader["Name"].ToString();
            activityInfo.Description = dataReader["Description"].ToString();
            activityInfo.URL = dataReader["URL"].ToString();
            activityInfo.Type = short.Parse(dataReader["Type"].ToString());
            activityInfo.Limit = short.Parse(dataReader["Limit"].ToString());
            activityInfo.CountLimit = int.Parse(dataReader["CountLimit"].ToString());
            activityInfo.UserLimit = int.Parse(dataReader["UserLimit"].ToString());

            var start = dataReader["StartTime"].ToDateTime();
            if (start.Year > 2000)
                activityInfo.StartTime = start;
            var end = dataReader["EndTime"].ToDateTime();
            if (end.Year > 2000)
                activityInfo.EndTime = end;
            activityInfo.CreateTime = DateTime.Parse(dataReader["CreateTime"].ToString());
            activityInfo.Creator = dataReader["Creator"].ToString();
            activityInfo.Updater = dataReader["Updater"].ToString();
            activityInfo.UpdateTime = DateTime.Parse(dataReader["UpdateTime"].ToString());
            activityInfo.Status = short.Parse(dataReader["Status"].ToString());
            activityInfo.UserType = short.Parse(dataReader["UserType"].ToString());
            return activityInfo;
        }

        #endregion

        #region 活动关联商品

        public int Activity_AddProduct(int actid, string productIDs)
        {
            var sql = "Activity_AddProduct";
            var cmd = dbw.GetStoredProcCommand(sql);
            dbw.AddInParameter(cmd, "ActID", DbType.Int32, actid);
            dbw.AddInParameter(cmd, "productIDs", DbType.String, productIDs);
            var result = dbw.ExecuteNonQuery(cmd);
            return result;
        }
        public int Activity_AddProduct(int actID, string cfPath, int brandID, int productID, string productCode)
        {
            var update = string.Format("UPDATE pa SET pa.ActID={0} FROM Product AS p INNER JOIN ProductActivity AS pa ON p.ProductID = pa.ProductID ", actID);
            var back = string.Format("INSERT INTO ActivityRecord( ProductID, ActID, CreateTime ) SELECT p.ProductID ,pa.ActID ,GETDATE() FROM Product AS p INNER JOIN ProductActivity AS pa ON p.ProductID = pa.ProductID");
            var sqlwhere = new StringBuilder();
            sqlwhere.AppendFormat(" WHERE 1=1");
            if (!string.IsNullOrWhiteSpace(cfPath))
            {
                sqlwhere.AppendFormat(" AND EXISTS( SELECT * FROM Classification AS c WHERE p.CFID = c.CFID  AND  c.[Path] LIKE '{0}%' ) ", cfPath);
            }
            if (brandID > 0)
            {
                sqlwhere.Append(" and BrandID=" + brandID);
            }
            if (productID > 0)
            {
                sqlwhere.Append(" and ProductID =" + productID);
            }
            if (!string.IsNullOrWhiteSpace(productCode))
            {
                sqlwhere.AppendFormat(" AND ProductCode = '{0}' ", productCode);
            }
            //先记录备份,在修改
            var sql = new StringBuilder();
            sql.Append(update);
            sql.Append(sqlwhere.ToString());
            sql.Append(";");
            sql.Append(back);
            sql.Append(sqlwhere.ToString());

            var cmd = dbw.GetSqlStringCommand(sql.ToString());
            var result = dbw.ExecuteNonQuery(cmd);
            return result;
        }

        public bool Activity_DelProduct(int actid, string productIDs)
        {
            var sql = "Activity_DelProduct";
            var cmd = dbw.GetStoredProcCommand(sql);
            dbw.AddInParameter(cmd, "ActID", DbType.Int32, actid);
            dbw.AddInParameter(cmd, "productIDs", DbType.String, productIDs);

            var result = dbw.ExecuteNonQuery(cmd);
            return result > 0;
        }

        #endregion
    }
}
