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
    public class ExpressNewsDAL
    {
        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;


        /// <summary>
        /// ExpressNews_Insert Method		
        /// </summary>
        /// <param name="expressNewsInfo">ExpressNews_Insert object</param>
        /// <returns></returns>
        public int ExpressNews_Insert(ExpressNewsInfo expressNewsInfo)
        {
            string sqlCommand = "[JXMarketing].[dbo].[ExpressNews_Insert]";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);
            dbw.AddOutParameter(dbCommand, "NewsID", DbType.Int32, 4);
            dbw.AddInParameter(dbCommand, "Author", DbType.String, expressNewsInfo.Author);
            dbw.AddInParameter(dbCommand, "Type", DbType.Int32, expressNewsInfo.Type);
            dbw.AddInParameter(dbCommand, "Source", DbType.String, expressNewsInfo.Source);
            dbw.AddInParameter(dbCommand, "Link", DbType.String, expressNewsInfo.Link);
            dbw.AddInParameter(dbCommand, "Title", DbType.String, expressNewsInfo.Title);
            dbw.AddInParameter(dbCommand, "Keywords", DbType.String, expressNewsInfo.Keywords);
            dbw.AddInParameter(dbCommand, "Abstract", DbType.String, expressNewsInfo.Abstract);
            dbw.AddInParameter(dbCommand, "Content", DbType.String, expressNewsInfo.Content);
            dbw.AddInParameter(dbCommand, "CreateTime", DbType.DateTime, expressNewsInfo.CreateTime);
            dbw.AddInParameter(dbCommand, "Creator", DbType.String, expressNewsInfo.Creator);
            dbw.AddInParameter(dbCommand, "Updater", DbType.String, expressNewsInfo.Updater);
            dbw.AddInParameter(dbCommand, "UpdateTime", DbType.DateTime, expressNewsInfo.UpdateTime);
            dbw.ExecuteNonQuery(dbCommand);
            return int.Parse(dbw.GetParameterValue(dbCommand, "NewsID").ToString());
        }

        /// <summary>
        /// ExpressNews_GetList Method
        /// </summary>
        /// <param name="pageIndex">起始页码</param>
        /// <param name="pageSize">每页数据数</param>
        /// <param name="orderType">设置排序，'':没有排序要求 0：主键升序 1：主键降序 字符串：用户自定义排序规则 如：‘SubmitTime DESC , ID DESC’</param>
        /// <param name="strWhere">查询条件(注意: 不要加 WHERE)</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>A Generic List of ExpressNewsInfo</returns>
        public IList<ExpressNewsInfo> ExpressNews_GetList(int pageIndex, int pageSize, string orderType, string strWhere, out int recordCount)
        {
            IList<ExpressNewsInfo> expressNewsInfoList = new List<ExpressNewsInfo>();
            string sqlCommand = "[JXMarketing].[dbo].[ExpressNews_GetList]";
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
                    expressNewsInfoList.Add(RecoverModel(dataReader));
                }
            }

            recordCount = int.Parse(dbr.GetParameterValue(dbCommand, "RecordCount").ToString());

            return expressNewsInfoList;
        }

        /// <summary>
        /// 从 IDataReader 中恢复ExpressNewsInfo对象
        /// </summary>
        /// <param name="IDataReader"></param>
        /// <returns></returns>
        private ExpressNewsInfo RecoverModel(IDataReader dataReader)
        {
            ExpressNewsInfo expressNewsInfo = new ExpressNewsInfo();

            expressNewsInfo.NewsID = int.Parse(dataReader["NewsID"].ToString());
            expressNewsInfo.Author = dataReader["Author"].ToString();
            expressNewsInfo.Type = dataReader["Type"].ToInt();
            expressNewsInfo.Source = dataReader["Source"].ToString();
            expressNewsInfo.Link = dataReader["Link"].ToString();
            expressNewsInfo.Title = dataReader["Title"].ToString();
            expressNewsInfo.Keywords = dataReader["Keywords"].ToString();
            expressNewsInfo.Abstract = dataReader["Abstract"].ToString();
            expressNewsInfo.Content = dataReader["Content"].ToString();
            expressNewsInfo.CreateTime = dataReader["CreateTime"].ToDateTime();
            expressNewsInfo.Creator = dataReader["Creator"].ToString();
            expressNewsInfo.Updater = dataReader["Updater"].ToString();
            expressNewsInfo.Title = dataReader["Title"].ToString();
            expressNewsInfo.Keywords = dataReader["Keywords"].ToString();
            expressNewsInfo.UpdateTime = dataReader["UpdateTime"].ToDateTime();
            expressNewsInfo.IsStick = dataReader["IsStick"].ToShort();
            expressNewsInfo.IsRed = dataReader["IsRed"].ToShort();

            return expressNewsInfo;
        }

        /// <summary>
        /// ExpressNews_Update Method
        /// </summary>
        /// <param name="ExpressNews_Update">ExpressNews_Update object</param>
        /// <returns>true:成功 false:失败</returns>
        public bool ExpressNews_Update(ExpressNewsInfo expressNewsInfo)
        {
            string sqlCommand = "[JXMarketing].[dbo].[ExpressNews_Update]";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddInParameter(dbCommand, "NewsID", DbType.Int32, expressNewsInfo.NewsID);
            dbw.AddInParameter(dbCommand, "Author", DbType.String, expressNewsInfo.Author);
            dbw.AddInParameter(dbCommand, "Type", DbType.Int32, expressNewsInfo.Type);
            dbw.AddInParameter(dbCommand, "Source", DbType.String, expressNewsInfo.Source);
            dbw.AddInParameter(dbCommand, "Link", DbType.String, expressNewsInfo.Link);
            dbw.AddInParameter(dbCommand, "Title", DbType.String, expressNewsInfo.Title);
            dbw.AddInParameter(dbCommand, "Keywords", DbType.String, expressNewsInfo.Keywords);
            dbw.AddInParameter(dbCommand, "Abstract", DbType.String, expressNewsInfo.Abstract);
            dbw.AddInParameter(dbCommand, "Content", DbType.String, expressNewsInfo.Content);
            dbw.AddInParameter(dbCommand, "Updater", DbType.String, expressNewsInfo.Updater);
            dbw.AddInParameter(dbCommand, "IsStick", DbType.Int16, expressNewsInfo.IsStick);
            dbw.AddInParameter(dbCommand, "IsRed", DbType.Int16, expressNewsInfo.IsRed);

            dbw.ExecuteNonQuery(dbCommand);

            return true;

        }

        /// <summary>
        /// ExpressNews_Delete Method
        /// </summary>
        /// <param name="newsID">ExpressNews Main ID</param>
        /// <returns>true:成功 false:失败</returns>	
        public bool ExpressNews_Delete(int newsID)
        {
            string sqlCommand = "[JXMarketing].[dbo].[ExpressNews_Delete]";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddInParameter(dbCommand, "NewsID", DbType.Int32, newsID);

            dbw.ExecuteNonQuery(dbCommand);

            return true;
        }


        /// <summary>
        ///查询单个ExpressNews
        /// </summary>
        /// <param name="newsID">ExpressNews Main ID</param>
        /// <returns>ExpressNewsInfo get from ExpressNews table.</returns>	
        public List<ExpressNewsInfo> ExpressNews_Get(int newsID)
        {
            var sql = "SELECT * FROM [JXMarketing].[dbo].[ExpressNews] AS e WHERE e.NewsID = " + newsID;
            var cmd = dbr.GetSqlStringCommand(sql);
            var list = new List<ExpressNewsInfo>();
            using (var read = dbr.ExecuteReader(CommandType.Text, sql))
            {
                while (read.Read())
                {
                    list.Add(this.RecoverModel(read));
                }
            }
            return list;
        }	
    }
}
