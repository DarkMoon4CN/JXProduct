using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JXProduct.Component.Model;
using JXProduct.Component.DataAccess;

namespace JXProduct.Component.SQLServerDAL
{
    public class BrandDAL
    {
        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;

        /// <summary>
        /// Brand_Insert Method		
        /// </summary>
        /// <param name="BrandInfo">Brand object</param>
        /// <returns></returns>
        public int Brand_Insert(BrandInfo brandInfo)
        {
            string sqlCommand = "Brand_Insert";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);
            dbw.AddOutParameter(dbCommand, "BrandID", DbType.Int32, 4);
            dbw.AddInParameter(dbCommand, "ChineseName", DbType.String, brandInfo.ChineseName);
            dbw.AddInParameter(dbCommand, "PinyinName", DbType.String, brandInfo.PinyinName);
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

        /// <summary>
        /// Brand_Update Method
        /// </summary>
        /// <param name="BrandInfo">Brand object</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Brand_Update(BrandInfo brandInfo)
        {
            string sqlCommand = "Brand_Update";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddInParameter(dbCommand, "BrandID", DbType.Int32, brandInfo.BrandID);
            dbw.AddInParameter(dbCommand, "ChineseName", DbType.String, brandInfo.ChineseName);
            dbw.AddInParameter(dbCommand, "PinyinName", DbType.String, brandInfo.PinyinName);
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
            //dbw.AddInParameter(dbCommand, "SellCount", DbType.Int32, brandInfo.SellCount);
            //dbw.AddInParameter(dbCommand, "ProductCount", DbType.Int32, brandInfo.ProductCount);

            dbw.ExecuteNonQuery(dbCommand);

            return true;

        }

        /// <summary>
        /// Brand_Get Method
        /// </summary>
        /// <param name="brandID">Brand Main ID</param>
        /// <returns>BrandInfo get from Brand table.</returns>	
        public BrandInfo Brand_Get(int brandID)
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

        /// <summary>
        /// Brand_Delete Method
        /// </summary>
        /// <param name="brandID">Brand Main ID</param>
        /// <returns>true:成功 false:失败</returns>	
        public bool Brand_Delete(int brandID)
        {
            string sqlCommand = "Brand_Delete";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddInParameter(dbCommand, "BrandID", DbType.Int32, brandID);

            dbw.ExecuteNonQuery(dbCommand);

            return true;
        }

        /// <summary>
        /// Brand_GetList Method
        /// </summary>
        /// <param name="pageIndex">起始页码</param>
        /// <param name="pageSize">每页数据数</param>
        /// <param name="orderType">设置排序，'':没有排序要求 0：主键升序 1：主键降序 字符串：用户自定义排序规则 如：‘SubmitTime DESC , ID DESC’</param>
        /// <param name="strWhere">查询条件(注意: 不要加 WHERE)</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>A Generic List of BrandInfo</returns>
        public IList<BrandInfo> Brand_GetList(int pageIndex, int pageSize, string orderType, string strWhere, out int recordCount)
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

        /// <summary>
        /// 从 IDataReader 中恢复Brand对象
        /// </summary>
        /// <param name="IDataReader"></param>
        /// <returns></returns>
        private BrandInfo RecoverModel(IDataReader dataReader)
        {
            BrandInfo brandInfo = new BrandInfo();

            brandInfo.BrandID = int.Parse(dataReader["BrandID"].ToString());
            brandInfo.ChineseName = dataReader["ChineseName"].ToString();
            brandInfo.PinyinName = dataReader["PinyinName"].ToString();
            brandInfo.EnglishName = dataReader["EnglishName"].ToString();
            brandInfo.RegTrademark = dataReader["RegTrademark"].ToString();
            brandInfo.UnregTrademark = dataReader["UnregTrademark"].ToString();
            brandInfo.BrandType = dataReader["BrandType"].ToShort();
            brandInfo.ImageLogo = dataReader["ImageLogo"].ToString();
            brandInfo.ImageBanner = dataReader["ImageBanner"].ToString();
            brandInfo.Level = dataReader["Level"].ToShort();
            brandInfo.Path = dataReader["Path"].ToString();
            brandInfo.ParentID = dataReader["ParentID"].ToShort();
            brandInfo.PathCount = dataReader["PathCount"].ToShort();
            brandInfo.Title = dataReader["Title"].ToString();
            brandInfo.Keywords = dataReader["Keywords"].ToString();
            brandInfo.Description = dataReader["Description"].ToString();
            brandInfo.MetaDescription = dataReader["MetaDescription"].ToString();
            brandInfo.Sort = dataReader["Sort"].ToInt();
            brandInfo.Status = dataReader["Status"].ToShort();
            brandInfo.SellCount = dataReader["SellCount"].ToInt();
            brandInfo.ProductCount = dataReader["ProductCount"].ToInt();

            return brandInfo;
        }

        /// <summary>
        /// 查询多个BrandID信息 格式："1;2;3;4"
        /// </summary>
        /// <param name="brandIDs">品牌编号</param>
        /// <returns></returns>
        public List<BrandInfo> Brand_Get(string brandIDs)
        {
            var sql = "SELECT * FROM [Brand] AS c WHERE c.BrandID IN ( " + brandIDs + " ) ";
            var cmd = dbr.GetSqlStringCommand(sql);
            var list = new List<BrandInfo>();

            using (var read = dbr.ExecuteReader(CommandType.Text, sql))
            {
                while (read.Read())
                {
                    list.Add(this.RecoverModel(read));
                }
            }
            return list;
        }

        /// <summary>
        /// 以分类查询品牌信息
        /// </summary>
        /// <param name="brandIDs">品牌编号</param>
        /// <returns></returns>
        public IList<BrandInfo> Brand_GetByCFID(int cfid, int pageIndex, int pageSize, string orderType, out int recordCount)
        {
            IList<BrandInfo> brandInfoList = new List<BrandInfo>();

            string sqlCommand = "Brand_GetByCFID";
            DbCommand dbCommand = dbr.GetStoredProcCommand(sqlCommand);

            dbr.AddInParameter(dbCommand, "cfid", DbType.Int32, cfid);
            dbr.AddInParameter(dbCommand, "PageIndex", DbType.Int32, pageIndex);
            dbr.AddInParameter(dbCommand, "PageSize", DbType.Int32, pageSize);
            dbr.AddInParameter(dbCommand, "OrderType", DbType.String, orderType);
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
    }
}