namespace JXProduct.Component.SQLServerDAL
{
    using JXProduct.Component.DataAccess;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    internal class KeywordProductDAL
    {
        private static Database dbr = JXProductData.Reader;
        private static Database dbw = JXProductData.Writer;

        /// <summary>
        /// 检索出商品相关“关键字ID”
        /// </summary>
        internal List<int> KeywordProduct_GetList(int productid)
        {
            var sql = string.Format("SELECT kp.KeywordID FROM KeywordProduct AS kp WHERE kp.ProductID = {0} ORDER BY kp.Sort DESC", productid);
            var cmd = dbr.GetSqlStringCommand(sql);
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
        /// 添加关键字
        /// </summary>
        internal bool KeywordProduct_Insert(int productID, int keywordID, int sort)
        {
            var sql = "INSERT INTO KeywordProduct ( KeywordID, ProductID, Sort ) VALUES ( @keywordID,@productID,@sort );";
            var cmd = dbw.GetSqlStringCommand(sql);
            dbw.AddInParameter(cmd, "ProductID", DbType.Int32, productID);
            dbw.AddInParameter(cmd, "KeywordID", DbType.Int32, keywordID);
            dbw.AddInParameter(cmd, "Sort", DbType.Int32, sort);
            var result = dbw.ExecuteNonQuery(cmd);
            return result > 0;
        }
    }
}
