namespace JXAPI.Component.SQLServerDAL.SqlServerDAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Text;
    using JXAPI.Component.Model;
    using JXAPI.Component.DataAccess;
    using log4net;
    using JXAPI.Common.Utils;
    using JXAPI.Common;
    public class JXProductDAL
    {
        private static Database dbr_Product = JXProductData.Reader;
        private static Database dbr_ProductStock = JXProductStockData.Reader;
        private static Database dbr_Marketing = JXMarketingData.Reader;
        private static Database dbr_YX = JXYXData.Reader;
        DatabaseType dbType = DatabaseType.dbr_Product;

        #region CURD

        /// <summary>
        /// 查询需要添加列表
        /// </summary>
        /// <param name="productID">ID</param>
        /// <param name="updateTime">更新时间</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        public DataTable GetList(string tableName,out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                StringBuilder sqlCommand = new StringBuilder("select ");
                switch (tableName.ToLower())
                {
                    case "keyword":
                    case "activity":
                    case "activityrule":
                    case "productcombo":
                    case "productgift":
                    case "productrelated":
                    case "manufacturer":
                    case "classification":
                    case "productquestion":
                    case "productevaluation":
                    case "productparameterprice":
                    case "productclassification":
                    case "classificationparametertoprice":
                        sqlCommand.AppendFormat(" * from {0} where IsPush = 1",tableName);
                        dbType = DatabaseType.dbr_Product;
                        break;
                    case "jxorderproductrelated":
                        sqlCommand.AppendFormat(" * from ProductRelated where IsPush = 1");
                        dbType = DatabaseType.dbr_Product;
                        break;
                    case "productstock":
                        sqlCommand.AppendFormat(" * from {0} where IsPush = 1", tableName);
                        dbType = DatabaseType.dbr_ProductStock;
                        break;
                    case "sale":
                    case "promotion":
                    case "productall":
                    case "expressnews":
                    case "searchkeyword":
                        sqlCommand.AppendFormat(" * from {0} where IsPush = 1", tableName);
                        dbType = DatabaseType.dbr_Marketing;
                        break;
                    case "jxyxproduct":
                        sqlCommand.AppendFormat(" * from product where IsPush = 1");
                        dbType = DatabaseType.dbr_YX;
                        break;
                    case "product":
                    case "jxorderproduct":
                        sqlCommand.Append(string.Format(@" p.*,m.Manufacturer,ISNULL((SELECT SUM(UsableStock) FROM JXProductStock.dbo.ProductStock WHERE ProductCode=p.ProductCode),0) AS Stock 
                                                           FROM Product AS p LEFT JOIN Manufacturer AS m ON m.ManuID=p.ManufacturerID WHERE 1=1 "));
                        sqlCommand.Append(string.Format(" AND p.IsPush = 1"));
                        dbType = DatabaseType.dbr_Product;
                        break;
                    case "productactivity":
                        sqlCommand.AppendFormat(@"a.*,p.ProductCode,p.MarketPrice,p.TradePrice,p.MobilePrice,p.CostPrice FROM 
                                                 ProductActivity as a inner join Product as p on a.ProductID = p.ProductID where 1=1 ");
                        sqlCommand.Append(string.Format(" AND a.IsPush = 1"));
                        dbType = DatabaseType.dbr_Product;
                        break;
                    case "keywordproduct":
                        sqlCommand.AppendFormat(@" * from {0} as p where exists 
                                                  (select k.KeywordID from Keyword as k where k.KeywordID = p.KeywordID ", tableName);
                        sqlCommand.AppendFormat(@"  ) AND p.IsPush = 1 ORDER BY p.RelationID");
                        dbType = DatabaseType.dbr_Product;
                        break;
                    case "keywordrelation":
                        sqlCommand.AppendFormat(@" * from {0} as r where exists 
                                                  (select k.KeywordID from Keyword as k where k.KeywordID = r.KeywordID ", tableName);
                        sqlCommand.AppendFormat(@"  ) AND r.IsPush = 1 ORDER BY r.ID");
                        dbType = DatabaseType.dbr_Product;
                        break;
                }
                DataSet dSet = new DataSet();
                switch (dbType)
                {
                    case DatabaseType.dbr_Product:
                        var dbCommand1 = dbr_Product.GetSqlStringCommand(sqlCommand.ToString());
                        dSet = dbr_Product.ExecuteDataSet(dbCommand1);
                        break;
                    case DatabaseType.dbr_Marketing:
                        var dbCommand2 = dbr_Marketing.GetSqlStringCommand(sqlCommand.ToString());
                        dSet = dbr_Marketing.ExecuteDataSet(dbCommand2);
                        break;
                    case DatabaseType.dbr_YX:
                        var dbCommand3 = dbr_YX.GetSqlStringCommand(sqlCommand.ToString());
                        dSet = dbr_YX.ExecuteDataSet(dbCommand3);
                        break;
                    case DatabaseType.dbr_ProductStock:
                        var dbCommand4 = dbr_ProductStock.GetSqlStringCommand(sqlCommand.ToString());
                        dSet = dbr_ProductStock.ExecuteDataSet(dbCommand4);
                        break;
                }
                return (dSet != null && dSet.Tables.Count > 0) ? dSet.Tables[0] : null;
            }
            catch (Exception ex)
            {
                errorMsg = string.Format("GetList 查询{0}表失败,异常信息:{1}", tableName, ex.Message);
                return null;
            }
        }

        public OperationResult<bool> UpdateProduct(string tableName, string primaryKey, int primaryValue)
        {
            try
            {
                switch (tableName.ToLower())
                {
                    case "product":
                    case "productactivity":
                    case "productclassification":
                    case "productcombo":
                    case "productgift":
                    case "productparameterprice":
                    case "productrelated":
                    case "manufacter":
                    case "productevaluation":
                    case "productquestion":
                    case "classificationparametertoprice":
                    case "brand":
                    case "classification":
                    case "keyword":
                    case "keywordproduct":
                    case "keywordrelation":
                    case "activity":
                    case "activityrule":
                        dbType = DatabaseType.dbr_Product;
                        break;
                    case "productstock":
                        dbType = DatabaseType.dbr_ProductStock;
                        break;
                    case "sale":
                    case "productall":
                    case "promotion":
                    case "expressnews":
                    case "searchkeyword":
                        dbType = DatabaseType.dbr_Marketing;
                        break;
                    case "jxyxproduct":
                        dbType = DatabaseType.dbr_YX;
                        tableName = "Product";
                        break;
                    case "jxorderproduct":
                        dbType = DatabaseType.dbr_Product;
                        tableName = "Product";
                        break;
                    case "jxorderproductrelated":
                        dbType = DatabaseType.dbr_Product;
                        tableName = "ProductRelated";
                        break;
                }
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.AppendFormat("update {0} set IsPush = 0 where {1} = {2}", tableName, primaryKey, primaryValue);
                switch (dbType)
                {
                    case DatabaseType.dbr_Product:
                        var dbCommand1 = dbr_Product.GetSqlStringCommand(sqlCommand.ToString());
                        dbr_Product.ExecuteNonQuery(dbCommand1);
                        break;
                    case DatabaseType.dbr_Marketing:
                        var dbCommand2 = dbr_Marketing.GetSqlStringCommand(sqlCommand.ToString());
                        dbr_Marketing.ExecuteNonQuery(dbCommand2);
                        break;
                    case DatabaseType.dbr_YX:
                        var dbCommand3 = dbr_YX.GetSqlStringCommand(sqlCommand.ToString());
                        dbr_YX.ExecuteNonQuery(dbCommand3);
                        break;
                    case DatabaseType.dbr_ProductStock:
                        var dbCommand4 = dbr_ProductStock.GetSqlStringCommand(sqlCommand.ToString());
                        dbr_ProductStock.ExecuteNonQuery(dbCommand4);
                        break;
                }
                return new OperationResult<bool>(OperationResultType.Success, null, true);
            }
            catch (Exception ex)
            {
                return new OperationResult<bool>(OperationResultType.Error, ex.Message, false);
            }
        }

        public DataTable GetProductDeleteItemRecord(out string errorMsg)
        {
            DataSet dSet = new DataSet();
            errorMsg = string.Empty;
            try
            {
                var sqlCommand = string.Format(@"select * from DeleteItemRecord as d where d.DbName != null or d.DbName != ''");
                var dbCommand = dbr_Product.GetSqlStringCommand(sqlCommand.ToString());
                dSet = dbr_Product.ExecuteDataSet(dbCommand);
                return (dSet != null && dSet.Tables.Count > 0) ? dSet.Tables[0] : null;
            }
            catch (Exception ex)
            {
                errorMsg = string.Format("GetProductDeleteItemRecord 查询DeleteItemRecord表失败,异常信息:{0}", ex.Message);
                return null;
            }
        }

        public OperationResult<bool> DeleteProductDeleteItemRecord(int id)
        {
            try
            {
                var sqlCommand = string.Format(@"delete from DeleteItemRecord where ID = {0}",id);
                var dbCommand = dbr_Product.GetSqlStringCommand(sqlCommand.ToString());
                dbr_Product.ExecuteNonQuery(dbCommand);
                return new OperationResult<bool>(OperationResultType.Success, null, true);
            }
            catch (Exception ex)
            {
                return new OperationResult<bool>(OperationResultType.Error, ex.Message, false);
            }
        }

        #endregion

        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public DataTable Product_ListForSearch(string strWhere)
        {
            string sqlCommand = "Product_GetListForSearch";
            DbCommand dbCommand = dbr_Product.GetStoredProcCommand(sqlCommand);
            dbr_Product.AddInParameter(dbCommand, "strWhere", DbType.String, strWhere);
            DataSet dSet = dbr_Product.ExecuteDataSet(dbCommand);
            return (dSet != null && dSet.Tables.Count > 0) ? dSet.Tables[0] : null;
        }
    }

    enum DatabaseType
    {
        dbr_Product = 1,
        dbr_Marketing,
        dbr_YX,
        dbr_ProductStock
    }
}