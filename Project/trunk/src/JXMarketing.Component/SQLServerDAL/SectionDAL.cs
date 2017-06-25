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
        /// <returns>true:�ɹ� false:ʧ��</returns>
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
        /// <returns>true:�ɹ� false:ʧ��</returns>	
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
        /// <param name="pageIndex">��ʼҳ��</param>
        /// <param name="pageSize">ÿҳ������</param>
        /// <param name="orderType">��������'':û������Ҫ�� 0���������� 1���������� �ַ������û��Զ���������� �磺��SubmitTime DESC , ID DESC��</param>
        /// <param name="strWhere">��ѯ����(ע��: ��Ҫ�� WHERE)</param>
        /// <param name="recordCount">�ܼ�¼��</param>
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
        /// �� IDataReader �лָ�Section����
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

        #region keyword����

        /// <summary>
        /// ��ȡ����ID
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
        /// ��ӿ���->�ؼ��ʶ�Ӧ��ϵ
        /// </summary>
        internal bool Section_AddKeyword(int sectionid, int keywordid)
        {
            string sql = string.Format("INSERT INTO SectionKeyword ( SectionID, KeywordID ) VALUES ({0},{1})", sectionid, keywordid);
            var cmd = dbw.GetSqlStringCommand(sql);
            var result = dbw.ExecuteNonQuery(cmd);
            return result > 0;
        }

        /// <summary>
        ///  ������ӹؼ��ֹ���
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