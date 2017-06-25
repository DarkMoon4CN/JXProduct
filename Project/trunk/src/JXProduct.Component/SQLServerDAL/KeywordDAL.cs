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
            string sqlCommand = "Keyword_Insert";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddOutParameter(dbCommand, "KeywordID", DbType.Int32, 4);
            dbw.AddInParameter(dbCommand, "ChineseName", DbType.String, info.ChineseName);
            dbw.AddInParameter(dbCommand, "PinyinName", DbType.String, info.PinyinName);
            dbw.AddInParameter(dbCommand, "FirstLetter", DbType.String, info.FirstLetter);
            dbw.AddInParameter(dbCommand, "Status", DbType.Int16, info.Status);
            dbw.AddInParameter(dbCommand, "TypeID", DbType.Int16, info.TypeID);
            dbw.AddInParameter(dbCommand, "Creator", DbType.String, info.Creator);

            dbw.ExecuteNonQuery(dbCommand);
            return int.Parse(dbw.GetParameterValue(dbCommand, "KeywordID").ToString());

        }
        internal bool Keyword_Update(KeywordInfo info)
        {
            string sqlCommand = "Keyword_Update";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddInParameter(dbCommand, "KeywordID", DbType.Int32, info.KeywordID);
            dbw.AddInParameter(dbCommand, "ChineseName", DbType.String, info.ChineseName);
            dbw.AddInParameter(dbCommand, "PinyinName", DbType.String, info.PinyinName);
            dbw.AddInParameter(dbCommand, "FirstLetter", DbType.String, info.FirstLetter);
            dbw.AddInParameter(dbCommand, "Status", DbType.Int16, info.Status);
            dbw.AddInParameter(dbCommand, "TypeID", DbType.Int16, info.TypeID);
            dbw.AddInParameter(dbCommand, "Updater", DbType.String, info.Updater);

            dbw.ExecuteNonQuery(dbCommand);
            return true;
        }

        #endregion

        /// <summary>
        /// 查询商品关联标签
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<KeywordInfo> KeywordProduct_ByProductID(int productId)
        {
            var list = new List<KeywordInfo>();
            string sqlCommand = "select k.* from dbo.KeywordProduct AS kp INNER JOIN dbo.Keyword AS k ON k.KeywordID=kp.KeywordID WHERE kp.ProductID=@ProductID ORDER BY kp.Sort";
            DbCommand dbCommand = dbr.GetSqlStringCommand(sqlCommand);
            dbr.AddInParameter(dbCommand, "ProductID", DbType.Int32, productId);
            using (IDataReader dataReader = dbr.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    list.Add(ConvertModel(dataReader));
                }
            }
            return list;
        }

        /// <summary>
        /// 处理关键词关联商品
        /// </summary>
        /// <param name="keywordID">关键词ID</param>
        /// <param name="productID">商品ID</param>
        /// <param name="sort">排序</param>
        /// <param name="inputTypeID">1=删除 2=排序 0=新增</param>
        /// <returns>返回数据：0=正常</returns>
        public string KeywordProduct_Save(int keywordID, int productID, int sort, int inputTypeID)
        {
            string sqlCommand = "KeywordProduct_Save";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);
            dbw.AddInParameter(dbCommand, "KeywordID", DbType.Int32, keywordID);
            dbw.AddInParameter(dbCommand, "ProductID", DbType.Int32, productID);
            dbw.AddInParameter(dbCommand, "Sort", DbType.Int16, sort);
            dbw.AddInParameter(dbCommand, "InputTypeID", DbType.Int16, inputTypeID);
            dbw.AddOutParameter(dbCommand, "Error", DbType.String, 100);
            dbw.ExecuteNonQuery(dbCommand);

            return dbw.GetParameterValue(dbCommand, "Error").ToString();
        }

    }
}