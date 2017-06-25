﻿using JXAPI.Component.DataAccess;
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
    public partial class ProductMySqlDAL
    {
        private static Database hadbr = JXHealthActivityMySqlData.Reader;

        /// <summary>
        /// 商品降价数据 
        /// </summary>
        /// <returns></returns>
        public IList<ProductFavoriteMySqlInfo> ProductMySql_GetAll()
        {
            IList<ProductFavoriteMySqlInfo> productInfoList = new List<ProductFavoriteMySqlInfo>();
            string sql = string.Empty;
            sql += " SELECT  p.ProductID,p.ChineseName,p.TradePrice,f.Price,f.UserID FROM  jxproduct.product p,jxyx.favorite f  ";
            sql += " WHERE p.ProductID=f.ProductID  AND p.TradePrice < f.Price  AND p.Selling=1 AND p.Status=0  AND f.IsPush=0 ";
            sql += "  GROUP BY f.UserID ";
            DbCommand cmd = hadbr.GetSqlStringCommand(sql);
            using (IDataReader dataReader = hadbr.ExecuteReader(cmd))
            {
                while (dataReader.Read())
                {
                    productInfoList.Add(RecoverModel(dataReader));
                }
            }
            return productInfoList;
        }

        /// <summary>
        /// 从 IDataReader 中恢复ProductInfo对象
        /// </summary>
        /// <param name="IDataReader"></param>
        /// <returns></returns>
        public ProductFavoriteMySqlInfo RecoverModel(IDataReader dataReader)
        {
            ProductFavoriteMySqlInfo productInfo = new ProductFavoriteMySqlInfo();
            productInfo.ProductID = dataReader["ProductID"].ToInt();
            productInfo.Price = dataReader["Price"].ToDecimal();
            productInfo.UserID = dataReader["UserID"].ToInt();
            productInfo.TradePrice = dataReader["TradePrice"].ToDecimal();
            productInfo.ProductName = dataReader["ChineseName"].ToString();
            return productInfo;
        }

        /// <summary>
        /// 更新推送状态
        /// </summary>
        /// <param name="productID">商品ID</param>
        /// <param name="userID">用户ID</param>
        /// <param name="isPush">0 未推送 1已推送</param>
        /// <returns>true 成功 false　失败</returns>
        public bool ProductMySql_UpIsPush(int productID, int userID, int isPush)
        {
            try
            {
                string sql = string.Empty;
                sql += "UPDATE jxyx.favorite SET IsPush={0} WHERE ProductID={1} AND UserID={2}";
                sql = string.Format(sql, isPush, productID, userID);
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