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
    internal class BrandDAL
    {
        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;

        #region CURD


        internal int Brand_Insert(BrandInfo brandInfo)
        {
            string sqlCommand = "Brand_Insert";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddOutParameter(dbCommand, "BrandID", DbType.Int32, 4);
            dbw.AddInParameter(dbCommand, "ChineseName", DbType.String, brandInfo.ChineseName);
            dbw.AddInParameter(dbCommand, "EnglishName", DbType.String, brandInfo.EnglishName);
            dbw.AddInParameter(dbCommand, "RegTrademark", DbType.String, brandInfo.RegTrademark);
            dbw.AddInParameter(dbCommand, "UnregTrademark", DbType.String, brandInfo.UnregTrademark);
            dbw.AddInParameter(dbCommand, "BrandType", DbType.Int16, brandInfo.BrandType);
            dbw.AddInParameter(dbCommand, "ImageLogo", DbType.String, brandInfo.ImageLogo);
            dbw.AddInParameter(dbCommand, "ImageBanner", DbType.String, brandInfo.ImageBanner);
            dbw.AddInParameter(dbCommand, "Level", DbType.Int16, brandInfo.Level);
            dbw.AddInParameter(dbCommand, "Path", DbType.String, brandInfo.Path);
            dbw.AddInParameter(dbCommand, "ParentID", DbType.Int32, brandInfo.ParentID);
            dbw.AddInParameter(dbCommand, "PathCount", DbType.Int16, brandInfo.PathCount);
            dbw.AddInParameter(dbCommand, "Title", DbType.String, brandInfo.Title);
            dbw.AddInParameter(dbCommand, "Keywords", DbType.String, brandInfo.Keywords);
            dbw.AddInParameter(dbCommand, "Description", DbType.String, brandInfo.Description);
            dbw.AddInParameter(dbCommand, "MetaDescription", DbType.String, brandInfo.MetaDescription);
            dbw.AddInParameter(dbCommand, "Sort", DbType.Int32, brandInfo.Sort);
            dbw.AddInParameter(dbCommand, "Status", DbType.Int16, brandInfo.Status);

            dbw.ExecuteNonQuery(dbCommand);

            return int.Parse(dbw.GetParameterValue(dbCommand, "BrandID").ToString());
        }


        internal bool Brand_Update(BrandInfo brandInfo)
        {
            string sqlCommand = "Brand_Update";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddInParameter(dbCommand, "BrandID", DbType.Int32, brandInfo.BrandID);
            dbw.AddInParameter(dbCommand, "ChineseName", DbType.String, brandInfo.ChineseName);
            dbw.AddInParameter(dbCommand, "EnglishName", DbType.String, brandInfo.EnglishName);
            dbw.AddInParameter(dbCommand, "RegTrademark", DbType.String, brandInfo.RegTrademark);
            dbw.AddInParameter(dbCommand, "UnregTrademark", DbType.String, brandInfo.UnregTrademark);
            dbw.AddInParameter(dbCommand, "BrandType", DbType.Int16, brandInfo.BrandType);
            dbw.AddInParameter(dbCommand, "ImageLogo", DbType.String, brandInfo.ImageLogo);
            dbw.AddInParameter(dbCommand, "ImageBanner", DbType.String, brandInfo.ImageBanner);
            dbw.AddInParameter(dbCommand, "Level", DbType.Int16, brandInfo.Level);
            dbw.AddInParameter(dbCommand, "Path", DbType.String, brandInfo.Path);
            dbw.AddInParameter(dbCommand, "ParentID", DbType.Int32, brandInfo.ParentID);
            dbw.AddInParameter(dbCommand, "PathCount", DbType.Int16, brandInfo.PathCount);
            dbw.AddInParameter(dbCommand, "Title", DbType.String, brandInfo.Title);
            dbw.AddInParameter(dbCommand, "Keywords", DbType.String, brandInfo.Keywords);
            dbw.AddInParameter(dbCommand, "Description", DbType.String, brandInfo.Description);
            dbw.AddInParameter(dbCommand, "MetaDescription", DbType.String, brandInfo.MetaDescription);
            dbw.AddInParameter(dbCommand, "Sort", DbType.Int32, brandInfo.Sort);
            dbw.AddInParameter(dbCommand, "Status", DbType.Int16, brandInfo.Status);

            dbw.ExecuteNonQuery(dbCommand);

            return true;

        }

        internal BrandInfo Brand_Get(int brandID)
        {
            BrandInfo brandInfo = null;

            string sqlCommand = "Brand_Get";
            DbCommand dbCommand = dbr.GetStoredProcCommand(sqlCommand);

            dbr.AddInParameter(dbCommand, "BrandID", DbType.Int32, brandID);

            using (IDataReader dataReader = dbr.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    brandInfo = RecoverModel(dataReader);
                }
            }

            return brandInfo;
        }

        internal IList<BrandInfo> Brand_GetList(int pageIndex, int pageSize, string orderType, string strWhere, out int recordCount)
        {
            IList<BrandInfo> brandInfoList = new List<BrandInfo>();

            string sqlCommand = "Brand_GetList";
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
                    brandInfoList.Add(RecoverModel(dataReader));
                }
            }

            recordCount = int.Parse(dbr.GetParameterValue(dbCommand, "RecordCount").ToString());

            return brandInfoList;
        }

        private BrandInfo RecoverModel(IDataReader dataReader)
        {
            var brandInfo = new BrandInfo();

            brandInfo.BrandID = int.Parse(dataReader["BrandID"].ToString());
            brandInfo.ChineseName = dataReader["ChineseName"].ToString();
            brandInfo.EnglishName = dataReader["EnglishName"].ToString();
            brandInfo.RegTrademark = dataReader["RegTrademark"].ToString();
            brandInfo.UnregTrademark = dataReader["UnregTrademark"].ToString();
            brandInfo.BrandType = short.Parse(dataReader["BrandType"].ToString());
            brandInfo.ImageLogo = dataReader["ImageLogo"].ToString();
            brandInfo.ImageBanner = dataReader["ImageBanner"].ToString();
            brandInfo.Level = short.Parse(dataReader["Level"].ToString());
            brandInfo.Path = dataReader["Path"].ToString();
            brandInfo.ParentID = int.Parse(dataReader["ParentID"].ToString());
            brandInfo.PathCount = short.Parse(dataReader["PathCount"].ToString());
            brandInfo.Title = dataReader["Title"].ToString();
            brandInfo.Keywords = dataReader["Keywords"].ToString();
            brandInfo.Description = dataReader["Description"].ToString();
            brandInfo.MetaDescription = dataReader["MetaDescription"].ToString();
            brandInfo.Sort = dataReader["Sort"].ToInt();
            brandInfo.Status = dataReader["Status"].ToShort();

            return brandInfo;
        }

        #endregion
    }
}
