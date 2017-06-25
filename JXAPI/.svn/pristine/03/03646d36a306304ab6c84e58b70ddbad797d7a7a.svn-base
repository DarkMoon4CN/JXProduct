using JXAPI.Component.DataAccess;
using JXAPI.Component.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace JXAPI.Component.SQLServerDAL
{
    public partial class ProductStockMySqlDAL
    {
        private static Database hadbr = JXHealthActivityMySqlData.Reader;

        /// <summary>
        ///  商品到货通知 
        /// </summary>
        /// <returns></returns>
        public IList<ProductOutBookMySqlInfo> ProductOutBookMySql_GetAll()
        {
            IList<ProductOutBookMySqlInfo> productOutBookInfo = new List<ProductOutBookMySqlInfo>();
            string sql = string.Empty;
            sql += " SELECT b.OutID,b.UID,b.ProductID,b.ProductName FROM jxproduct.productoutbook b,jxactivity.productstock  s ";
            sql += " WHERE  b.ProductCode=s.ProductCode AND  b.Type=2 AND b.IsPush=0 ";
            sql += " AND s.UsableStock>0 ";
            sql += " GROUP BY b.UID ";
            DbCommand cmd = hadbr.GetSqlStringCommand(sql);
            using (IDataReader dataReader = hadbr.ExecuteReader(cmd))
            {
                while (dataReader.Read())
                {
                    productOutBookInfo.Add(RecoverModel(dataReader));
                }
            }
            return productOutBookInfo;
        }

        /// <summary>
        /// 从 IDataReader 中恢复OrdersInfo对象
        /// </summary>
        /// <param name="IDataReader"></param>
        /// <returns></returns>
        public ProductOutBookMySqlInfo RecoverModel(IDataReader dataReader)
        {
            ProductOutBookMySqlInfo productOutBookInfo = new ProductOutBookMySqlInfo();
            productOutBookInfo.OutID = dataReader["OutID"].ToInt();
            productOutBookInfo.UID = dataReader["UID"].ToInt();
            productOutBookInfo.ProductID = dataReader["ProductID"].ToInt();
            productOutBookInfo.ProductName = dataReader["ProductName"].ToString();
            return productOutBookInfo;
        }

        /// <summary>
        ///  更新推送状态
        /// </summary>
        /// <param name="outID"> productoutbook ID</param>
        /// <param name="isPush">0 未推送 1已推送</param>
        /// <returns>true 成功 false　失败</returns>
        public bool ProductOutBookMySql_UpIsPush(int outID,int isPush) 
        {
            try
            {
                string sql = string.Empty;
                sql += "UPDATE jxproduct.productoutbook SET IsPush={0} WHERE OutID={1}";
                sql = string.Format(sql, isPush, outID);
                DbCommand dbCommand = dbw.GetSqlStringCommand(sql);
                int result = dbw.ExecuteNonQuery(dbCommand);
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch 
            {
                return false;
            }
        }
    }
}
