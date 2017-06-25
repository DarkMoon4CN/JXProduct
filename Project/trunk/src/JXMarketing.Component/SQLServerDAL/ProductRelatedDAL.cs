using JXProduct.Component.DataAccess;
using JXProduct.Component.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Collections.Generic;

namespace JXProduct.Component.SQLServerDAL
{
    public class ProductRelatedDAL
    {
        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;

        #region GetList + Delete

        internal List<ProductRelatedInfo> ProductRelated_GetList(int productid, int childproductid, Enums.RelatedType t)
        {
            string sql = "ProductRelated_GetList";
            var cmd = dbr.GetStoredProcCommand(sql);
            dbw.AddInParameter(cmd, "ProductID", DbType.Int32, productid);
            dbw.AddInParameter(cmd, "ChildProductID", DbType.Int32, childproductid);
            dbw.AddInParameter(cmd, "Type", DbType.Int32, (int)t);
            var list = new List<ProductRelatedInfo>();
            using (var read = dbr.ExecuteReader(cmd))
            {
                while (read.Read())
                {
                    list.Add(ReaderModel(read));
                }
            }
            return list;
        }

        internal List<ProductRelatedInfo> ProductRelated_GetList(int productid, Enums.RelatedType t)
        {
            return this.ProductRelated_GetList(productid, 0, t);
            //string sql = "ProductRelated_Get";
            //var cmd = dbr.GetStoredProcCommand(sql);
            //dbw.AddInParameter(cmd, "ProductID", DbType.Int32, productid);
            //dbw.AddInParameter(cmd, "Type", DbType.Int32, (int)t);
            //var list = new List<ProductRelatedInfo>();
            //using (var read = dbr.ExecuteReader(cmd))
            //{
            //    while (read.Read())
            //    {
            //        list.Add(ReaderModel(read));
            //    }
            //}
            //return list;
        }

        internal int ProductRelated_Delete(int productid, int childproductid, int type)
        {
            var sql = "ProductRelated_Delete";
            var cmd = dbw.GetStoredProcCommand(sql);
            dbw.AddInParameter(cmd, "ProductID", DbType.Int32, productid);
            dbw.AddInParameter(cmd, "ChildProductID", DbType.Int32, childproductid);
            dbw.AddInParameter(cmd, "Type", DbType.Int16, type);
            var result = dbw.ExecuteScalar(cmd);

            return int.Parse(result.ToString());
        }

        private ProductRelatedInfo ReaderModel(IDataReader read)
        {
            return new ProductRelatedInfo
            {
                ProductID = read["ProductID"].ToInt(),
                ChildProductID = read["ChildProductID"].ToInt(),

                Name = read["Name"].ToString(),
                Type = read["Type"].ToShort(),

                MainName = read["MainName"].ToString(),
                ChildName = read["ChildName"].ToString(),

                MainPrice = read["MainPrice"].ToDecimal(),
                ChildPrice = read["ChildPrice"].ToDecimal(),

                Quantity = read["Quantity"].ToInt(),

                Specifications = read["Specifications"].ToString(),
                Creator = read["Creator"].ToString(),
                CreateTime = read["CreateTime"].ToDateTime(),
                Updater = read["Updater"].ToString(),
                UpdateTime = read["UpdateTime"].ToDateTime()
            };
        }

        #endregion


        #region Insert  重载

        /// <summary>
        /// 大包装生成
        /// </summary>
        internal bool ProductRelated_SavePackaging(int productid, int childproductid, int quantity, decimal price, string name, string creator)
        {
            /*
             @productid INT,@name NVARCHAR(128),@quantity INT,@price DECIMAL(10,2),@creator NVARCHAR(32)
             */
            var sql = "ProductRelated_SavePackaging";
            var cmd = dbw.GetStoredProcCommand(sql);

            dbw.AddInParameter(cmd, "ProductID", DbType.Int32, productid);
            dbw.AddInParameter(cmd, "Childproductid", DbType.Int32, childproductid);
            dbw.AddInParameter(cmd, "Name", DbType.String, name);
            dbw.AddInParameter(cmd, "Quantity", DbType.Int32, quantity);
            dbw.AddInParameter(cmd, "Price", DbType.Decimal, price);
            dbw.AddInParameter(cmd, "Creator", DbType.String, creator);

            var result = int.Parse(dbw.ExecuteScalar(cmd).ToString());
            return result > 0;
        }

        /// <summary>
        /// 最佳推荐 Type=1
        /// </summary>
        internal bool ProductRelated_SaveBestRecommend(int productid, string childProductIDs, string creator)
        {

            /*
                @productid INT ,@childProductIDs VARCHAR(500),@creator NVARCHAR(32)
             */
            var sql = "ProductRelated_SaveBestRecommend";
            var cmd = dbw.GetStoredProcCommand(sql);

            dbw.AddInParameter(cmd, "ProductID", DbType.Int32, productid);
            dbw.AddInParameter(cmd, "childProductIDs", DbType.String, childProductIDs);
            dbw.AddInParameter(cmd, "Creator", DbType.String, creator);

            return dbw.ExecuteNonQuery(cmd) > 0;
        }
        /// <summary>
        /// 添加不同规格  Type=2
        /// </summary>
        internal int ProductRelated_SaveDifferentSpec(ProductRelatedInfo info)
        {

            /*
             @productid INT ,@childproductid INT ,@name NVARCHAR(64),@creator NVARCHAR(32)
             */

            var sql = "ProductRelated_SaveDifferentSpec";
            var cmd = dbw.GetStoredProcCommand(sql);

            dbw.AddInParameter(cmd, "ProductID", DbType.Int32, info.ProductID);
            dbw.AddInParameter(cmd, "ChildProductID", DbType.Int32, info.ChildProductID);
            dbw.AddInParameter(cmd, "Name", DbType.String, info.Name);
            dbw.AddInParameter(cmd, "Creator", DbType.String, info.Creator);
            var result = dbw.ExecuteScalar(cmd);
            return int.Parse(result.ToString());
        }

        #endregion
    }
}