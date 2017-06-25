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
            dbw.AddInParameter(dbCommand, "Creator", DbType.String, sectionInfo.Creator);
            dbw.AddInParameter(dbCommand, "CreateTime", DbType.DateTime, sectionInfo.CreateTime);

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
            dbw.AddInParameter(dbCommand, "Updater", DbType.String, sectionInfo.Updater);
            dbw.AddInParameter(dbCommand, "UpdateTime", DbType.DateTime, sectionInfo.UpdateTime);

            var result = Convert.ToInt32(dbw.ExecuteScalar(dbCommand));
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
        public IList<SectionInfo> Section_GetList(int parentid)
        {
            var sectionlist = new List<SectionInfo>();
            string sql = "SELECT section.* FROM [dbo].[Section] WHERE parentid= " + parentid;
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
        public List<SectionInfo> Section_GetList(string name)
        {
            var sectionlist = new List<SectionInfo>();
            string sql = string.Format("SELECT section.* FROM [dbo].[Section] WHERE SectionName like '%{0}%' ", name);
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
        public SectionInfo Section_Get(int sectionid)
        {
            string sql = "SELECT section.* FROM [dbo].[Section] WHERE sectionid= " + sectionid;
            var dbcmd = dbr.GetSqlStringCommand(sql);
            using (var read = dbr.ExecuteReader(dbcmd))
            {
                if (read.Read())
                {
                    return RecoverModel(read);
                }
            }
            return null;
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
            model.Level = dataReader["Level"].ToInt();
            model.KeywordID = dataReader["KeywordID"].ToInt();
            return model;
        }

        #endregion
    }
}