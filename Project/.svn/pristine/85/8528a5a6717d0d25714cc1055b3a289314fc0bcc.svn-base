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
    class CategoryDAL
    {
        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;

        /// <summary>
        /// Category_Update Method
        /// </summary>
        /// <param name="categoryInfo">category object</param>
        /// <param name="inputTypeID">Update Type </param>
        /// <returns>true:成功 false:失败</returns>
        public bool Category_Update(CategoryInfo categoryInfo, int inputTypeID)
        {
            string outError = string.Empty;
            string sqlCommand = "[JXMarketing].[dbo].[Category_Update]";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);
            dbw.AddInParameter(dbCommand, "CFID", DbType.Int32, categoryInfo.CFID);
            dbw.AddInParameter(dbCommand, "Ids", DbType.String, categoryInfo.Brands);
            dbw.AddInParameter(dbCommand, "InputTypeID", DbType.String, inputTypeID);
            dbw.AddInParameter(dbCommand, "ChangUser", DbType.String, categoryInfo.Updater);
            dbw.AddInParameter(dbCommand, "Error", DbType.String, outError);
            dbw.ExecuteNonQuery(dbCommand);
            return true;
        }

        /// <summary>
        /// 查询单个CFID
        /// </summary>
        /// <param name="cfid">CFID</param>
        /// <returns></returns>
        public List<CategoryInfo> Category_Get(int cfid) 
        {
            var sql = "SELECT * FROM [JXMarketing].[dbo].[Category] AS c WHERE c.CFID = " + cfid;
            var cmd = dbr.GetSqlStringCommand(sql);
            var list = new List<CategoryInfo>();
  
            using (var read = dbr.ExecuteReader(CommandType.Text, sql))
            {
                while (read.Read())
                {
                    list.Add(this.ReaderJXParaModel(read));
                }
            }
            return list;
        }

        /// <summary>
        /// Reader->CategoryInfo对象
        /// </summary>
        private CategoryInfo ReaderJXParaModel(IDataReader dataReader)
        {
            var categoryInfo = new CategoryInfo();
            categoryInfo.CFID = int.Parse(dataReader["CFID"].ToString());
            categoryInfo.Brands = dataReader["Brands"].ToString();
            categoryInfo.CreateTime=dataReader["CreateTime"].ToDateTime();
            categoryInfo.Creator = dataReader["Creator"].ToString();
            categoryInfo.HotSalled = dataReader["HotSalled"].ToString();
            categoryInfo.Keywords = dataReader["Keywords"].ToString();
            categoryInfo.Recommend = dataReader["Recommend"].ToString();
            categoryInfo.Preferential = dataReader["Preferential"].ToString();
            categoryInfo.Salled =dataReader["Salled"].ToString();
            categoryInfo.Updater = dataReader["Updater"].ToString();
            categoryInfo.UpdateTime = dataReader["UpdateTime"].ToDateTime();
            categoryInfo.WeekSalled = dataReader["WeekSalled"].ToString();
            return categoryInfo;
        }

    }

    
}
