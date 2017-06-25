using JXProduct.Component.DataAccess;
using JXProduct.Component.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace JXProduct.Component.SQLServerDAL
{
    internal class ProductActivityDAL
    {

        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;

        #region CURD


        /// <summary>
        /// ProductActivity_Update Method
        /// </summary>
        public bool ProductActivity_Update(ProductActivityInfo model)
        {
            string sqlCommand = "ProductActivity_Update";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);

            dbw.AddInParameter(dbCommand, "ProductID", DbType.Int32, model.ProductID);
            dbw.AddInParameter(dbCommand, "ActDesc", DbType.String, model.ActDesc);
            dbw.AddInParameter(dbCommand, "ActQuantity", DbType.Int32, model.ActQuantity);
            dbw.AddInParameter(dbCommand, "ActPrice", DbType.Decimal, model.ActPrice);
            dbw.AddInParameter(dbCommand, "Type", DbType.Int16, model.Type);
            dbw.AddInParameter(dbCommand, "CouponBatchNo", DbType.String, model.CouponBatchNo);
            dbw.AddInParameter(dbCommand, "CouponName", DbType.String, model.CouponName);
            dbw.AddInParameter(dbCommand, "Discount", DbType.Decimal, model.Discount);
            dbw.AddInParameter(dbCommand, "ProductGiftID", DbType.Int32, model.ProductGiftID);
            dbw.AddInParameter(dbCommand, "IsFreeShip", DbType.Int16, model.IsFreeShip);
            dbw.AddInParameter(dbCommand, "ExtType", DbType.String, model.ExtType);
            dbw.AddInParameter(dbCommand, "Status", DbType.Int16, model.Status);
            dbw.AddInParameter(dbCommand, "StartDate", DbType.DateTime, model.StartDate);
            dbw.AddInParameter(dbCommand, "EndDate", DbType.DateTime, model.EndDate);
            dbw.AddInParameter(dbCommand, "Updater", DbType.String, model.Updater);

            dbw.ExecuteNonQuery(dbCommand);
            return true;

        }

        /// <summary>
        /// 活动获取 商品ID 获取活动
        /// </summary>
        internal ProductActivityInfo ProductActivity_Get(int productID)
        {
            ProductActivityInfo model = null;

            string sqlCommand = "ProductActivity_Get";
            DbCommand dbCommand = dbr.GetStoredProcCommand(sqlCommand);
            dbr.AddInParameter(dbCommand, "ProductID", DbType.Int32, productID);
            using (IDataReader dataReader = dbr.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    model = RecoverModel(dataReader);
                }
            }
            return model;
        }

        /// <summary>
        /// 获取多个商品活动
        /// </summary>
        /// <param name="productIDs"></param>
        /// <returns></returns>
        internal List<ProductActivityInfo> ProductActivity_GetList(string productIDs)
        {
            var activityList = new List<ProductActivityInfo>();
            var sql = "ProductActivity_GetList";
            var dbcmd = dbr.GetStoredProcCommand(sql);
            dbr.AddInParameter(dbcmd, "productIDs", DbType.String, productIDs);
            using (var read = dbr.ExecuteReader(dbcmd))
            {
                while (read.Read())
                {
                    activityList.Add(RecoverModel(read));
                }
            }
            return activityList;
        }

        /// <summary>
        /// 从 IDataReader 中恢复ProductActivity对象
        /// </summary>
        /// <param name="IDataReader"></param>
        /// <returns></returns>
        private ProductActivityInfo RecoverModel(IDataReader dataReader)
        {
            ProductActivityInfo model = new ProductActivityInfo();

            model.ProductID = dataReader["ProductID"].ToInt();
            model.ActID = dataReader["ActID"].ToInt();
            model.ActName = dataReader["ActName"].ToString();
            model.ActDesc = dataReader["ActDesc"].ToString();
            model.ActStock = dataReader["ActStock"].ToInt();
            model.ActQuantity = dataReader["ActQuantity"].ToInt();
            model.ActPrice = dataReader["ActPrice"].ToDecimal();
            model.Type = dataReader["Type"].ToShort();
            //model.Stock = dataReader["Stock"].ToInt();
            model.CouponBatchNo = dataReader["CouponBatchNo"].ToString();
            model.CouponName = dataReader["CouponName"].ToString();
            model.Discount = dataReader["Discount"].ToDecimal();
            model.ProductGiftID = dataReader["ProductGiftID"].ToInt();
            model.ProductGiftName = dataReader["ProductGiftName"].ToString();
            model.IsFreeShip = dataReader["IsFreeShip"].ToShort();
            model.ExtType = dataReader["ExtType"].ToString();
            model.Status = dataReader["Status"].ToShort();
            model.StartDate = dataReader["StartDate"].ToDateTime();
            if (model.StartDate.Year < 2000)
                model.StartDate = DateTime.Now;
            model.EndDate = dataReader["EndDate"].ToDateTime();
            if (model.EndDate.Year < 2000)
                model.EndDate = DateTime.Now.AddDays(3);
            return model;
        }

        #endregion
    }
}
