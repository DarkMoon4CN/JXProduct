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
    public class ProductClassificationDAL
    {
        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;

        public List<ProductClassificationInfo> ProductClassification_GetList(int productid)
        {
            var sql = "SELECT * FROM ProductClassification WHERE productid=" + productid + " order by ID ASc";
            var cmd = dbr.GetSqlStringCommand(sql);
            using (var read = dbr.ExecuteReader(cmd))
            {
                var list = new List<ProductClassificationInfo>();
                while (read.Read())
                {
                    list.Add(new ProductClassificationInfo()
                    {
                        ID = int.Parse(read["ID"].ToString()),
                        ProductID = int.Parse(read["ProductID"].ToString()),
                        CFID = int.Parse(read["CFID"].ToString()),
                        CFPath = read["CFPath"].ToString()
                    });
                }
                return list;
            }
        }
        public bool ProductClassification_Insert(int productid, List<int> cflist, string Updater)
        {
            var sql = "ProductClassification_Insert";
            var cmd = dbw.GetStoredProcCommand(sql);
            dbw.AddInParameter(cmd, "productid", DbType.Int32, productid);
            dbw.AddInParameter(cmd, "cflist", DbType.String, string.Join(",", cflist));
            dbw.AddInParameter(cmd, "updater", DbType.String, Updater);
            var result = dbw.ExecuteNonQuery(cmd);
            return result > 0;
        }
        public bool ProductClassification_Insert(string productIDs, string cfIDs, string Updater)
        {
            var sql = "ProductClassification_Save";
            var cmd = dbw.GetStoredProcCommand(sql);
            dbw.AddInParameter(cmd, "ProductIDs", DbType.String, productIDs);
            dbw.AddInParameter(cmd, "cfIDs", DbType.String, cfIDs);
            dbw.AddInParameter(cmd, "Updater", DbType.String, Updater);
            var result = dbw.ExecuteNonQuery(cmd);
            return result > 0;
        }
    }
}
