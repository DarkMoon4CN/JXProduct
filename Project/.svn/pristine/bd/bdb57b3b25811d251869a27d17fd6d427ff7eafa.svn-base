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
    public class ClassificationDAL
    {
        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;


        #region CURD

        /// <summary>
        /// Classification_Insert Method		
        /// </summary>
        /// <returns></returns>
        internal int Classification_Insert(ClassificationInfo cfinfo)
        {
            string sqlCommand = "Classification_Insert";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddInParameter(dbCommand, "ChineseName", DbType.String, cfinfo.ChineseName);
            dbw.AddInParameter(dbCommand, "PinyinName", DbType.String, cfinfo.PinyinName);
            dbw.AddInParameter(dbCommand, "ImagesLogo", DbType.String, cfinfo.ImagesLogo);
            dbw.AddInParameter(dbCommand, "Level", DbType.Int16, cfinfo.Level);
            dbw.AddInParameter(dbCommand, "Path", DbType.String, cfinfo.Path);
            dbw.AddInParameter(dbCommand, "ParentID", DbType.Int32, cfinfo.ParentID);
            dbw.AddInParameter(dbCommand, "PathCount", DbType.Int16, cfinfo.PathCount);
            dbw.AddInParameter(dbCommand, "Title", DbType.String, cfinfo.Title);
            dbw.AddInParameter(dbCommand, "Keywords", DbType.String, cfinfo.Keywords);
            dbw.AddInParameter(dbCommand, "Description", DbType.String, cfinfo.Description);
            dbw.AddInParameter(dbCommand, "Sort", DbType.Int32, cfinfo.Sort);
            dbw.AddInParameter(dbCommand, "Status", DbType.Int16, cfinfo.Status);
            dbw.AddInParameter(dbCommand, "IsTop", DbType.Int16, cfinfo.IsTop);
            dbw.AddInParameter(dbCommand, "IsChannelHot", DbType.Int16, cfinfo.IsChannelHot);
            dbw.AddInParameter(dbCommand, "Creator", DbType.String, cfinfo.Creator);
            dbw.AddInParameter(dbCommand, "CreateTime", DbType.DateTime, cfinfo.CreateTime);


            var result = Convert.ToInt32(dbw.ExecuteScalar(dbCommand).ToString());
            return result;
        }

        /// <summary>
        /// Classification_Update Method
        /// </summary>
        /// <param name="ClassificationInfo">Classification object</param>
        /// <returns>true:成功 false:失败</returns>
        internal int Classification_Update(ClassificationInfo cfinfo)
        {
            string sqlCommand = "Classification_Update";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);
            dbw.AddInParameter(dbCommand, "CFID", DbType.Int32, cfinfo.CFID);
            dbw.AddInParameter(dbCommand, "ChineseName", DbType.String, cfinfo.ChineseName);
            dbw.AddInParameter(dbCommand, "PinyinName", DbType.String, cfinfo.PinyinName);

            dbw.AddInParameter(dbCommand, "Updater", DbType.String, cfinfo.Updater);
            dbw.AddInParameter(dbCommand, "UpdateTime", DbType.DateTime, cfinfo.UpdateTime);

            //dbw.AddInParameter(dbCommand, "ImagesLogo", DbType.String, cfinfo.ImagesLogo);
            //dbw.AddInParameter(dbCommand, "Level", DbType.Int16, cfinfo.Level);
            //dbw.AddInParameter(dbCommand, "Path", DbType.String, cfinfo.Path);
            //dbw.AddInParameter(dbCommand, "ParentID", DbType.Int32, cfinfo.ParentID);
            //dbw.AddInParameter(dbCommand, "PathCount", DbType.Int16, cfinfo.PathCount);
            //dbw.AddInParameter(dbCommand, "Title", DbType.String, cfinfo.Title);
            //dbw.AddInParameter(dbCommand, "Keywords", DbType.String, cfinfo.Keywords);
            //dbw.AddInParameter(dbCommand, "Description", DbType.String, cfinfo.Description);
            //dbw.AddInParameter(dbCommand, "Sort", DbType.Int32, cfinfo.Sort);
            //dbw.AddInParameter(dbCommand, "Status", DbType.Int16, cfinfo.Status);
            //dbw.AddInParameter(dbCommand, "IsTop", DbType.Int16, cfinfo.IsTop);
            //dbw.AddInParameter(dbCommand, "IsChannelHot", DbType.Int16, cfinfo.IsChannelHot);
            //return int.Parse(dbw.ExecuteScalar(dbCommand).ToString());

            return dbw.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Classification_Get Method
        /// </summary>
        /// <returns>ClassificationInfo get from Classification table.</returns>	
        internal ClassificationInfo Classification_Get(Int32 cfid)
        {
            ClassificationInfo cfinfo = null;

            string sqlCommand = "Classification_Get";
            DbCommand dbCommand = dbr.GetStoredProcCommand(sqlCommand);

            dbr.AddInParameter(dbCommand, "CFID", DbType.Int32, cfid);

            using (IDataReader read = dbr.ExecuteReader(dbCommand))
            {
                while (read.Read())
                {
                    cfinfo = RecoverModel(read);
                }
            }

            return cfinfo;
        }

        /// <summary>
        /// Classification_Delete Method
        /// </summary>
        /// <returns>true:成功 false:失败</returns>	
        internal bool Classification_Delete(int cfid)
        {
            string sqlCommand = "Classification_Delete";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);


            dbr.AddInParameter(dbCommand, "CFID", DbType.Int32, cfid);
            dbw.ExecuteNonQuery(dbCommand);

            return true;
        }

        /// <summary>
        /// Classification_GetList Method
        /// </summary>
        /// <param name="pageIndex">起始页码</param>
        /// <param name="pageSize">每页数据数</param>
        /// <param name="orderType">设置排序，'':没有排序要求 0：主键升序 1：主键降序 字符串：用户自定义排序规则 如：‘SubmitTime DESC , ID DESC’</param>
        /// <param name="strWhere">查询条件(注意: 不要加 WHERE)</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>A Generic List of ClassificationInfo</returns>
        internal IList<ClassificationInfo> Classification_GetList(int pageIndex, int pageSize, string orderType, string strWhere, out int recordCount)
        {
            IList<ClassificationInfo> cfinfolist = new List<ClassificationInfo>();

            string sqlCommand = "Classification_GetList";
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
                    cfinfolist.Add(RecoverModel(dataReader));
                }
            }

            recordCount = int.Parse(dbr.GetParameterValue(dbCommand, "RecordCount").ToString());

            return cfinfolist;
        }

        internal List<ClassificationInfo> Classification_GetPathList(int cfid)
        {
            var sql = "Classification_GetPathList";
            var cmd = dbr.GetStoredProcCommand(sql);
            dbr.AddInParameter(cmd, "CFID", DbType.Int32, cfid);
            var list = new List<ClassificationInfo>();
            using (var read = dbr.ExecuteReader(cmd))
            {
                while (read.Read())
                {
                    list.Add(RecoverModel(read));
                }
            }
            return list;
        }

        /// <summary>
        /// 从 IDataReader 中恢复Classification对象
        /// </summary> 
        /// <param name="IDataReader"></param>
        /// <returns></returns>
        private ClassificationInfo RecoverModel(IDataReader dataReader)
        {
            ClassificationInfo cfinfo = new ClassificationInfo();

            cfinfo.CFID = int.Parse(dataReader["CFID"].ToString());
            cfinfo.ChineseName = dataReader["ChineseName"].ToString();
            cfinfo.PinyinName = dataReader["PinyinName"].ToString();
            cfinfo.ImagesLogo = dataReader["ImagesLogo"].ToString();
            cfinfo.Level = byte.Parse(dataReader["Level"].ToString());
            cfinfo.Path = dataReader["Path"].ToString();
            cfinfo.ParentID = int.Parse(dataReader["ParentID"].ToString());
            cfinfo.PathCount = dataReader["PathCount"].ToShort();
            cfinfo.Title = dataReader["Title"].ToString();
            cfinfo.Keywords = dataReader["Keywords"].ToString();
            cfinfo.Description = dataReader["Description"].ToString();
            cfinfo.Sort = int.Parse(dataReader["Sort"].ToString());
            cfinfo.Status = byte.Parse(dataReader["Status"].ToString());
            cfinfo.IsTop = dataReader["IsTop"].ToShort();
            cfinfo.IsChannelHot = dataReader["IsChannelHot"].ToShort();
            cfinfo.KeywordID = dataReader["KeywordID"].ToInt();
            cfinfo.IsKeyParent = dataReader["IsKeyParent"].ToShort();

            return cfinfo;
        }

        #endregion

        #region 报价属性

        internal List<ClassificationParameterToPriceInfo> ClassificationParameterToPrice_Get(int cfid)
        {
            var sql = "ClassificationParameterToPrice_Get";
            var cmd = dbr.GetStoredProcCommand(sql);
            var list = new List<ClassificationParameterToPriceInfo>();
            dbr.AddInParameter(cmd, "@cfid", DbType.Int32, cfid);
            using (var read = dbr.ExecuteReader(cmd))
            {
                while (read.Read())
                {
                    list.Add(this.ReadClassificationParameterToPriceModel(read));
                }
            }
            return list;
        }

        private ClassificationParameterToPriceInfo ReadClassificationParameterToPriceModel(IDataReader read)
        {
            return new ClassificationParameterToPriceInfo
            {
                CFID = read["CFID"].ToInt(),
                CFParaPriceID = read["CFParaPriceID"].ToInt(),
                FatherCFID = read["FatherCFID"].ToInt(),
                CFParaPriceName = read["CFParaPriceName"].ToString(),
                CFParaPriceProp = read["CFParaPriceProp"].ToString(),
                CFParaPriceValue = read["CFParaPriceValue"].ToString(),
                Sort = read["Sort"].ToShort()
            };
        }

        #endregion
    }
}
