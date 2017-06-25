using JXProduct.Component.DataAccess;
using JXProduct.Component.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXProduct.Component.SQLServerDAL
{
    public class ProductParameterPriceDAL
    {
        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;

        /// <summary>
        /// 添加一组属性,0 失败, -1 已经存在  其他正常返回 autoid
        /// </summary>
        /// <param name="info"></param>
        internal int ProductParameterPrice_Insert(ProductParameterPriceInfo info)
        {
            var sql = "ProductParameterPrice_Insert";
            var cmd = dbw.GetStoredProcCommand(sql);
            dbw.AddInParameter(cmd, "MainProductID", DbType.Int32, info.MainProductID);
            dbw.AddInParameter(cmd, "ChildProductID", DbType.Int32, info.ChildProductID);
            dbw.AddInParameter(cmd, "Prop1", DbType.String, info.Prop1);
            dbw.AddInParameter(cmd, "Prop2", DbType.String, info.Prop2);
            dbw.AddInParameter(cmd, "Prop3", DbType.String, info.Prop3);
            var result = int.Parse(dbw.ExecuteScalar(cmd).ToString());
            return result;
        }


        /// <summary>
        /// 根据PriceParaID,修改Prop属性
        /// </summary>
        internal bool ProductParameterPrice_Update(ProductParameterPriceInfo info)
        {
            var sql = "ProductParameterPrice_Update";
            var cmd = dbw.GetStoredProcCommand(sql);
            dbw.AddInParameter(cmd, "PriceParaID", DbType.Int32, info.PriceParaID);
            dbw.AddInParameter(cmd, "Prop1", DbType.String, info.Prop1);
            dbw.AddInParameter(cmd, "Prop2", DbType.String, info.Prop2);
            dbw.AddInParameter(cmd, "Prop3", DbType.String, info.Prop3);
            var result = dbw.ExecuteNonQuery(cmd);
            return result > 0;
        }

        internal List<ProductParameterPriceInfo> ProductParameterPrice_GetList(int productid)
        {
            var sql = "ProductParameterPrice_GetList";
            var cmd = dbr.GetStoredProcCommand(sql);
            dbr.AddInParameter(cmd, "ProductID", DbType.Int32, productid);
            using (var read = dbr.ExecuteReader(cmd))
            {
                var pricelist = new List<ProductParameterPriceInfo>();
                while (read.Read())
                {
                    pricelist.Add(ReaderModel(read));
                }
                return pricelist;
            }
        }

        private ProductParameterPriceInfo ReaderModel(IDataReader read)
        {
            return new ProductParameterPriceInfo
            {
                PriceParaID = read["PriceParaID"].ToInt(),
                MainProductID = read["MainProductID"].ToInt(),
                MainName = read["MainName"].ToString(),
                ChildProductID = read["ChildProductID"].ToInt(),
                ChildName = read["ChildName"].ToString(),
                Prop1 = read["Prop1"].ToString(),
                Prop2 = read["Prop2"].ToString(),
                Prop3 = read["Prop3"].ToString()
            };
        }
    }
}
