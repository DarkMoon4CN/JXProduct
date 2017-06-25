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
   public class MarketBrandDAL
    {
        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;

        public List<MarketBrandInfo> Brand_GetByCFID(int cfId, int brandId = 0)
        {
            var sql = " SELECT * FROM [JXMarketing].[dbo].[Brand] AS c WHERE c.CFID = " + cfId;
            if (brandId != 0)
            {
                sql += "AND  c.BrandID= " + brandId;
            }
            var cmd = dbr.GetSqlStringCommand(sql);
            var list = new List<MarketBrandInfo>();

            using (var read = dbr.ExecuteReader(CommandType.Text, sql))
            {
                while (read.Read())
                {
                    list.Add(this.ReaderJXParaModel(read));
                }
            }
            return list;
        }

        public bool Brand_Insert(MarketBrandInfo brandInfo)
        {
            try
            {
                var sql = " INSERT INTO [JXMarketing].[dbo].[Brand](BrandID,CFID) ";
                sql += " VALUES( " + brandInfo.BrandID + "," + brandInfo.CFID + ")";
                DbCommand dbCommand = dbw.GetSqlStringCommand(sql);
                int result=dbw.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool Brand_Delete(MarketBrandInfo brandInfo) 
        {
            try
            {
                var sql = " DELETE  FROM [JXMarketing].[dbo].[Brand] WHERE BrandID="+brandInfo.BrandID+" AND CFID="+brandInfo.CFID+" ";
                DbCommand dbCommand = dbw.GetSqlStringCommand(sql);
                int result = dbw.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Reader->Brand对象
        /// </summary>
        private MarketBrandInfo ReaderJXParaModel(IDataReader dataReader)
        {
            var brandInfo = new MarketBrandInfo();
            brandInfo.CFID = int.Parse(dataReader["CFID"].ToString());
            brandInfo.BrandID = dataReader["BrandID"].ToInt();
            brandInfo.CreateTime = dataReader["CreateTime"].ToDateTime();
            brandInfo.Creator = dataReader["Creator"].ToString();
            brandInfo.HotSalled = dataReader["HotSalled"].ToString();
            brandInfo.Recommend = dataReader["Recommend"].ToString();
            brandInfo.Preferential = dataReader["Preferential"].ToString();
            brandInfo.Salled = dataReader["Salled"].ToString();
            brandInfo.Updater = dataReader["Updater"].ToString();
            brandInfo.UpdateTime = dataReader["UpdateTime"].ToDateTime();
            brandInfo.WeekSalled = dataReader["WeekSalled"].ToString();
            return brandInfo;
        }
    }
}
