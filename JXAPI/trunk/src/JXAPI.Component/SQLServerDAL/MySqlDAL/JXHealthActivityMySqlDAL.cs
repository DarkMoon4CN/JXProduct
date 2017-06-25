using JXAPI.Common;
using JXAPI.Component.DataAccess;
using JXAPI.Component.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace JXAPI.Component.SQLServerDAL.MySqlDAL
{
    public class JXHealthActivityMySqlDAL
    {
        private static Database dbw_Health = JXHealthActivityMySqlData.Writer;

        #region 更新JXHealth库中的Activity表中的LikeUserID，LikeFavor字段

        public OperationResult<bool> UpdateHealthActivity(string createTime)
        {
            try
            {
                string time = string.Empty;
                if (!string.IsNullOrEmpty(createTime))
                {
                    time = string.Format(@" where g.CreateTime >= '{0}'", createTime);
                }
                string strPlaceholder = string.Empty;
                StringBuilder sqlCommand = new StringBuilder(@"update jxhealth.activity as g set g.LikeUserID = 
                                         jxhealth.f_ActivityLikeUserID(g.ActID),
                                         g.LikeFavor = (
                                         select sum(p.SellCount + p.FavorCount) as FavorCount from jxhealth.activityproduct as a
                                         INNER join jxproduct.product as p on a.ProductID = p.ProductID where a.ActID = g.ActID
                                         and g.Status = 0)");
                sqlCommand.Append(time);
                var cmd = dbw_Health.GetSqlStringCommand(sqlCommand.ToString());
                cmd.CommandTimeout = 400;
                dbw_Health.ExecuteNonQuery(cmd);
                return new OperationResult<bool>(OperationResultType.Success, null, true);
            }
            catch (Exception ex)
            {
                return new OperationResult<bool>(OperationResultType.Error, ex.Message, false);
            }
        }

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
            DbCommand cmd = dbw_Health.GetSqlStringCommand(sql);
            using (IDataReader dataReader = dbw_Health.ExecuteReader(cmd))
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

        #endregion
    }
}
