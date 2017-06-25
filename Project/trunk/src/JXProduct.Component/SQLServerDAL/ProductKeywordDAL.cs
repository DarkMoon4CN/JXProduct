namespace JXProduct.Component.SQLServerDAL
{
	using System;
	using System.Collections.Generic;
	using System.Data.Common;
	using System.Data;
	using Microsoft.Practices.EnterpriseLibrary.Data;

	using JXProduct.Component.Model;
	using JXProduct.Component.DataAccess;
	public class ProductKeywordDAL
	{
		private static Database dbr = JXProductData.Writer;
		private static Database dbw = JXProductData.Reader;

		internal IList<ProductKeywordInfo> ProductKeyword_Get(int productid)
		{

			// kp.ProductID, kprc.CFID,k.KeywordID,k.ChineseName
			var sql = "KeywordProductRelationCF_Get";
			var cmd = dbr.GetStoredProcCommand(sql);

			dbr.AddInParameter(cmd, "ProductID", DbType.Int32, productid);
			var list = new List<ProductKeywordInfo>();
			using (var read = dbr.ExecuteReader(cmd))
			{
				while (read.Read())
				{
					list.Add(new ProductKeywordInfo
					{
						ProductID = read.GetInt32(0),
						CFID = read.GetInt32(1),
						KeywordID = read.GetInt32(2),
						KeywordName = read.GetString(3)

					});
				}
			}
			return list;
		}

		//internal Boolean ProductKeyword_Del(int productid,int keywordid)
		//{
		//    var sql = "KeywordProductRelationCF_Del";
		//    var cmd = dbw.GetSqlStringCommand(sql);

		//}


		/*
		 @productid INT,
	@keywordid INT,
	@cfid INT,
	@creator NVARCHAR(32)
		 */
		internal bool ProductKeyword_Insert(int productid, int keywordid, int cfid, string creator)
		{
			var sql = "KeywordProductRelationCF_Insert";
			var cmd = dbw.GetStoredProcCommand(sql);
			dbw.AddInParameter(cmd, "ProductID", DbType.Int32, productid);
			dbw.AddInParameter(cmd, "KeywordID", DbType.Int32, keywordid);
			dbw.AddInParameter(cmd, "CFID", DbType.Int32, cfid);
			dbw.AddInParameter(cmd, "Creator", DbType.String, creator);

			var result = dbw.ExecuteNonQuery(cmd);
			return result > 0;
		}
	}
}