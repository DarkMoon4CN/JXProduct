using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JXProduct.Component.DataAccess;
using JXProduct.Component.Model;

namespace JXProduct.Component.SQLServerDAL
{
    public class KeywordDAL
    {
        private static Database dbr = JXProductData.Reader;
        private static Database dbw = JXProductData.Writer;

        #region 查询

        internal KeywordInfo Keyword_Get(int keywordid)
        {
            var sql = "Keyword_Get";
            var cmd = dbr.GetStoredProcCommand(sql);
            dbr.AddInParameter(cmd, "keywordid", DbType.Int32, keywordid);
            using (var read = dbr.ExecuteReader(cmd))
            {
                if (read.Read())
                {
                    return ConvertModel(read);
                }
            }
            return null;
        }
        internal Dictionary<int, string> Keyword_GetList(string keywordids)
        {
            var dic = new Dictionary<int, string>();
            string sql = "SELECT * FROM [Keyword] Where 1=1 ";
            if (!string.IsNullOrEmpty(keywordids))
            {
                sql += " AND KeywordID in (" + keywordids + ")";
            }

            using (var read = dbr.ExecuteReader(CommandType.Text, sql))
            {
                while (read.Read())
                {
                    dic.Add(read.GetInt32(0), read.GetString(1));
                }
            }
            return dic;
        }

        internal List<KeywordInfo> Keyword_GetByName(string name, bool isLike = false)
        {

            string sql = "SELECT * FROM [Keyword] Where";
            if (isLike)
            {
                sql += string.Format(" ChineseName LIKE '%{0}%' ", name);
            }
            else
            {
                sql += string.Format(" ChineseName ='{0}' ", name);
            }
            var list = new List<KeywordInfo>();
            using (var read = dbr.ExecuteReader(CommandType.Text, sql))
            {

                while (read.Read())
                {
                    list.Add(ConvertModel(read));
                }
            }
            return list;
        }

        private KeywordInfo ConvertModel(IDataReader read)
        {
            return new KeywordInfo
            {
                KeywordID = int.Parse(read["KeywordID"].ToString()),
                TypeID = short.Parse(read["TypeID"].ToString()),
                ChineseName = read["ChineseName"].ToString(),
                FirstLetter = read["FirstLetter"].ToString(),
                Status = short.Parse(read["Status"].ToString())
            };
        }
        #endregion

        #region Save

        internal int Keyword_Insert(KeywordInfo info)
        {
            var sql = "INSERT INTO [dbo].[Keyword]([ChineseName], [PingyingName], [FirstLetter], [Status], [TypeID], [Creator], [CreateTime], [Updater], [UpdateTime]) VALUES (@ChineseName, @PingyingName, @FirstLetter, @Status, @TypeID, @Creator, @CreateTime, @Creator, @CreateTime);select SCOPE_IDENTITY();";

            var cmd = dbw.GetSqlStringCommand(sql);
            dbw.AddInParameter(cmd, "ChineseName", DbType.String, info.ChineseName);
            dbw.AddInParameter(cmd, "PingYingName", DbType.String, info.PingyingName);
            dbw.AddInParameter(cmd, "Firstletter", DbType.String, info.FirstLetter);
            dbw.AddInParameter(cmd, "Status", DbType.Int32, info.Status);
            dbw.AddInParameter(cmd, "TypeID", DbType.Int32, info.TypeID);
            dbw.AddInParameter(cmd, "Creator", DbType.String, info.Creator);
            dbw.AddInParameter(cmd, "CreateTime", DbType.DateTime, info.CreateTime);
            var result = int.Parse(dbw.ExecuteScalar(cmd).ToString());

            return result;
        }
        internal bool Keyword_Update(KeywordInfo info)
        {
            var sql = string.Format("UPDATE k set k.ChineseName = '{0}' FROM Keyword AS k WHERE k.KeywordID ={1}", info.ChineseName, info.KeywordID);
            var cmd = dbw.GetSqlStringCommand(sql);
            return dbw.ExecuteNonQuery(cmd) > 0;
        }

        #endregion
    }
}