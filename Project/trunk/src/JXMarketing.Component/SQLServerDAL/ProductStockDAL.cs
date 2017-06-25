using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

using JXProduct.Component.Model;
using JXProduct.Component.DataAccess;

namespace JXProduct.Component.SQLServerDAL
{
	public class ProductStockDAL
	{
		private static Database dbr = JXProductData.Writer;
		private static Database dbw = JXProductData.Reader;


		/*
		@StockID INT OUTPUT,
		@ProductCode VARCHAR(32),
		@HouseID INT,
		@TypeID SMALLINT,
		@Stock INT,
		@ChangeTime SMALLDATETIME,
		@LogType INT
		 */
		internal int ProductStock_Deal(ProductStockInfo info)
		{
			var sql = "ProductStock_Deal";
			var cmd = dbw.GetStoredProcCommand(sql);
			dbw.AddOutParameter(cmd, "StockID", DbType.Int32, 4);
			dbw.AddInParameter(cmd,"ProductCode",DbType.String,info.ProductCode);
			dbw.AddInParameter(cmd,"HouseID",DbType.Int32,info.HouseID);
			dbw.AddInParameter(cmd, "TypeID", DbType.Int32, info.TypeID);
			dbw.AddInParameter(cmd, "Stock", DbType.Int32, info.Stock);
			dbw.AddInParameter(cmd, "ChangeTime", DbType.DateTime, info.ChangeTime);
			dbw.AddInParameter(cmd, "LogType", DbType.Int32, info.LogType);

            dbw.ExecuteNonQuery(cmd);
            return int.Parse(dbw.GetParameterValue(cmd, "StockID").ToString());
 

		}


		internal List<ProductStockInfo> ProductStock_GetList(string productCode)
		{
			var sql = string.Format("SELECT [StockID],[ProductCode],[HouseID],[TypeID],[Stock],[OutStock],[FreezedStock],[UsableStock],[ChangeTime] FROM [ProductStock] WHERE ProductCode='{0}'", productCode);
			var cmd = dbr.GetSqlStringCommand(sql);
			var list = new List<ProductStockInfo>();
			using (var read = dbr.ExecuteReader(cmd))
			{
				while (read.Read())
				{
					list.Add(ConvertStockModel(read));
				}
			}
			return list;
		}

		private ProductStockInfo ConvertStockModel(IDataReader read)
		{
			return new ProductStockInfo()
			{
				StockID = read["StockID"].ToInt(),
				ProductCode = read["ProductCode"].ToString(),
				HouseID = read["HouseID"].ToInt(),
				TypeID = read["typeID"].ToShort(),
				Stock = read["stock"].ToInt(),
				OutStock = read["OutStock"].ToInt(),
				FreezedStock = read["FreezedStock"].ToInt(),
				UsableStock = read["UsableStock"].ToInt(),
				ChangeTime = read["ChangeTime"].ToDateTime()
			};
		}
	}
}