using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

using JXProduct.Component.Model;
using JXProduct.Component.DataAccess;
namespace JXProduct.Component.SQLServerDAL
{
    public class SectionDAL
    {
        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;

        #region CURD

        /// <summary>
        /// Section_Insert Method		
        /// </summary>
        /// <param name="SectionInfo">Section object</param>
        /// <returns></returns>
        public int Section_Insert(SectionInfo sectionInfo)
        {
            string sqlCommand = "Section_Insert";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddInParameter(dbCommand, "SectionName", DbType.String, sectionInfo.SectionName);
            dbw.AddInParameter(dbCommand, "SpellName", DbType.String, sectionInfo.SpellName);
            dbw.AddInParameter(dbCommand, "Path", DbType.String, sectionInfo.Path);
            dbw.AddInParameter(dbCommand, "ParentID", DbType.Int32, sectionInfo.ParentID);
            dbw.AddInParameter(dbCommand, "Sort", DbType.Int32, sectionInfo.Sort);
            dbw.AddInParameter(dbCommand, "Status", DbType.Int16, sectionInfo.Status);

            var result = Convert.ToInt32(dbw.ExecuteScalar(dbCommand));
            return result;
        }

        /// <summary>
        /// Section_Update Method
        /// </summary>
        /// <param name="SectionInfo">Section object</param>
        /// <returns>true:成功 false:失败</returns>
        public bool Section_Update(SectionInfo sectionInfo)
        {
            string sqlCommand = "Section_Update";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddInParameter(dbCommand, "SectionID", DbType.Int32, sectionInfo.SectionID);
            dbw.AddInParameter(dbCommand, "SectionName", DbType.String, sectionInfo.SectionName);
            dbw.AddInParameter(dbCommand, "SpellName", DbType.String, sectionInfo.SpellName);
            dbw.AddInParameter(dbCommand, "Path", DbType.String, sectionInfo.Path);
            dbw.AddInParameter(dbCommand, "ParentID", DbType.Int32, sectionInfo.ParentID);
            dbw.AddInParameter(dbCommand, "Sort", DbType.Int32, sectionInfo.Sort);
            dbw.AddInParameter(dbCommand, "Status", DbType.Int16, sectionInfo.Status);
            dbw.ExecuteNonQuery(dbCommand);
            return true;

        }

        /// <summary>
        /// Section_Delete Method
        /// </summary>
        /// <param name="sectionID">Section Main ID</param>
        /// <returns>true:成功 false:失败</returns>	
        public bool Section_Delete(int sectionID)
        {
            string sqlCommand = "Section_Delete";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);
            dbw.AddInParameter(dbCommand, "SectionID", DbType.Int32, sectionID);
            dbw.ExecuteNonQuery(dbCommand);
            return true;
        }

        /// <summary>
        /// Section_GetList Method
        /// </summary>
        /// <param name="pageIndex">起始页码</param>
        /// <param name="pageSize">每页数据数</param>
        /// <param name="orderType">设置排序，'':没有排序要求 0：主键升序 1：主键降序 字符串：用户自定义排序规则 如：‘SubmitTime DESC , ID DESC’</param>
        /// <param name="strWhere">查询条件(注意: 不要加 WHERE)</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>A Generic List of SectionInfo</returns>
        public IList<SectionInfo> Section_GetList(int sectionid, int parentid, string sectionname)
        {
            var sectionlist = new List<SectionInfo>();
            string sql = "SELECT [SectionID],[SectionName], [SpellName], [Path], [ParentID], [Sort], [Status] FROM [dbo].[Section] WHERE 1=1 ";
            if (sectionid > 0)
            {
                sql += "  AND SectionID = " + sectionid;
            }
            else if (parentid >= 0)
            {
                sql += " AND ParentID =" + parentid;
            }
            else if (!string.IsNullOrEmpty(sectionname))
            {
                sql += " AND sectionname LIKE '%" + sectionname + "%'";
            }
            var dbcmd = dbr.GetSqlStringCommand(sql);
            using (var read = dbr.ExecuteReader(dbcmd))
            {
                while (read.Read())
                {
                    sectionlist.Add(RecoverModel(read));
                }
            }
            return sectionlist;
        }

        /// <summary>
        /// 从 IDataReader 中恢复Section对象
        /// </summary>
        /// <param name="IDataReader"></param>
        /// <returns></returns>
        private SectionInfo RecoverModel(IDataReader dataReader)
        {
            var model = new SectionInfo();

            model.SectionID = dataReader["SectionID"].ToInt();
            model.SectionName = dataReader["SectionName"].ToString();
            model.SpellName = dataReader["SpellName"].ToString();
            model.Path = dataReader["Path"].ToString();
            model.ParentID = dataReader["ParentID"].ToInt();
            model.Sort = dataReader["Sort"].ToInt();
            model.Status = dataReader["Status"].ToShort();
            return model;
        }

        #endregion

        #region keyword关联

        /// <summary>
        /// 获取科室ID
        /// </summary>
        /// <param name="sectionid"></param>
        /// <returns></returns>
        internal List<int> Section_GetKeywordIDsBySectionID(int sectionid)
        {
            var sql = "SELECT SK.KeywordID FROM SectionKeyword AS sk where sectionID = " + sectionid;
            DbCommand cmd = dbr.GetSqlStringCommand(sql);

            var list = new List<int>();
            using (var read = dbr.ExecuteReader(cmd))
            {
                while (read.Read())
                {
                    list.Add(read.GetInt32(0));
                }
            }
            return list;
        }

        /// <summary>
        /// 添加科室->关键词对应关系
        /// </summary>
        internal bool Section_AddKeyword(int sectionid, int keywordid)
        {
            string sql = string.Format("INSERT INTO SectionKeyword ( SectionID, KeywordID ) VALUES ({0},{1})", sectionid, keywordid);
            var cmd = dbw.GetSqlStringCommand(sql);
            var result = dbw.ExecuteNonQuery(cmd);
            return result > 0;
        }

        /// <summary>
        ///  分类添加关键字关联
        /// </summary>
        internal bool Section_AddKeyword(int sectionid, string ChineseName, string PinyinName, string FirstLetter, string Creator)
        {
            var sql = "Section_AddKeyword";
            var cmd = dbw.GetStoredProcCommand(sql);
            dbw.AddInParameter(cmd, "Sectionid", DbType.Int32, sectionid);
            dbw.AddInParameter(cmd, "ChineseName", DbType.String, ChineseName);
            dbw.AddInParameter(cmd, "PinyinName", DbType.String, PinyinName);
            dbw.AddInParameter(cmd, "FirstLetter", DbType.String, FirstLetter);
            dbw.AddInParameter(cmd, "Creator", DbType.String, Creator);
            var result = int.Parse(dbw.ExecuteScalar(cmd).ToString());
            return result == 1;
        }
        internal bool Section_DelKeyword(int sectionid, int keywordid)
        {

            string sql = string.Format("DELETE FROM SectionKeyword WHERE SectionID = {0}  AND KeywordID = {1}", sectionid, keywordid);
            var cmd = dbw.GetSqlStringCommand(sql);
            var result = dbw.ExecuteNonQuery(cmd);
            return result > 0;
        }
        #endregion
    }
}