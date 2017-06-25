using JXProduct.Component.DataAccess;
using JXProduct.Component.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace JXProduct.Component.SQLServerDAL
{
    public class ProductParameterTypeDAL
    {
        private static Database dbr = JXProductData.Reader;
        private static Database dbw = JXProductData.Writer;

        public List<ProductParameterTypeDetailInfo> ProductParameterType_GetDetail(int typeid, int productid = 0)
        {
            var sql = "ProductParameterType_GetDetail";
            var cmd = dbr.GetStoredProcCommand(sql);
            dbr.AddInParameter(cmd, "typeid", DbType.Int32, typeid);
            dbr.AddInParameter(cmd, "productid", DbType.Int32, productid);
            var list = new List<ProductParameterTypeDetailInfo>();
            using (var read = dbr.ExecuteReader(cmd))
            {
                while (read.Read())
                {
                    list.Add(new ProductParameterTypeDetailInfo
                    {
                        ParaID = read.GetInt32(0),
                        TypeID = read.GetInt32(1),
                        Name = read.GetString(2),
                        Value = read["Value"].ToString()
                    });
                }
            }
            return list;
        }

        internal bool ProductParameterType_Insert(int productid, List<ProductParameterTypeDetailInfo> list)
        {
            var sql = new StringBuilder();
            sql.AppendFormat("DELETE ppdv FROM ProductParameterDetailValue AS ppdv WHERE ppdv.ProductID ={0};", productid);
            foreach (var l in list)
            {
                if (string.IsNullOrWhiteSpace(l.Value))
                    continue;
                sql.AppendFormat("INSERT INTO ProductParameterDetailValue(ProductID,ParaID,[Value]) VALUES({0},{1},'{2}');", productid, l.ParaID, l.Value);
            }
            var cmd = dbw.GetSqlStringCommand(sql.ToString());
            var result = dbw.ExecuteNonQuery(cmd);
            return result > 0;
        }
    }
}