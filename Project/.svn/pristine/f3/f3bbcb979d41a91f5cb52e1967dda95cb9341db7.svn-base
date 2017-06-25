using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

using JXProduct.Component.Model;
using JXProduct.Component.DataAccess;
namespace JXProduct.Component.SQLServerDAL
{
    public class ProductGiftDAL
    {
        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;

        #region CURD

        /// <summary>
        ///  商品赠品
        /// </summary>
        /// <param name="productGiftInfo"></param>
        /// <returns></returns>
        internal bool ProductGift_Save(ProductGiftInfo productGiftInfo)
        {
            string sqlCommand = "ProductGift_Save";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddInParameter(dbCommand, "ProductID", DbType.Int32, productGiftInfo.ProductID);
            dbw.AddInParameter(dbCommand, "ProductGiftID", DbType.Int32, productGiftInfo.ProductGiftID);
            dbw.AddInParameter(dbCommand, "Quantity", DbType.Int32, productGiftInfo.Quantity);
            dbw.AddInParameter(dbCommand, "Creator", DbType.String, productGiftInfo.Creator);
            dbw.ExecuteNonQuery(dbCommand);
            return true;
        }


        /// <summary>
        /// 查询商品赠品,通过拆分productIDs(1,2,3,4,5)
        /// </summary>
        /// <param name="productIDs"></param>
        /// <returns></returns>
        internal List<ProductGiftInfo> ProductGift_GetList(string productIDs)
        {
            var productlist = new List<ProductGiftInfo>();
            string sqlCommand = "ProductGift_GetList";
            DbCommand dbCommand = dbr.GetStoredProcCommand(sqlCommand);
            dbr.AddInParameter(dbCommand, "ProductIDs", DbType.String, productIDs);
            using (var read = dbr.ExecuteReader(dbCommand))
            {
                while (read.Read())
                {
                    productlist.Add(RecoverModel(read));
                }
            }
            return productlist;
        }

        private ProductGiftInfo RecoverModel(IDataReader dataReader)
        {
            return new ProductGiftInfo
            {
                ProductID = dataReader["ProductID"].ToInt(),
                ProductGiftID = dataReader["ProductGiftID"].ToInt(),
                ProductGiftName= dataReader["ProductGiftName"].ToString(),
                Quantity = dataReader["Quantity"].ToInt(),
                Status = dataReader["Status"].ToShort()
            };
        }

        #endregion
    }
}