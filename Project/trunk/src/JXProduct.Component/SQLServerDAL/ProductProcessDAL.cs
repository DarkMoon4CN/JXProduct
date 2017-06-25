using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JXProduct.Component.Model;
using JXProduct.Component.DataAccess;

namespace JXProduct.Component.SQLServerDAL
{
    public class ProductProcessDAL
    {
        private static Database dbr = JXProductData.Writer;
        private static Database dbw = JXProductData.Reader;

        public ProductProcessInfo Get(int productid)
        {
            var sql = "SELECT * FROM ProductProcess pp WHERE pp.ProductID =" + productid;
            var cmd = dbr.GetSqlStringCommand(sql);
            using (var read = dbr.ExecuteReader(cmd))
            {
                if (read.Read())
                {
                    return new ProductProcessInfo(
                        read["ProductID"].ToInt(),
                        Convert.ToByte(read["Process1"]),
                        Convert.ToByte(read["Process2"]),
                        Convert.ToByte(read["Process3"]),
                        Convert.ToByte(read["Process4"]),
                        read["Updater"].ToString());
                }
            }
            return null;
        }
        public bool Save(ProductProcessInfo info)
        {
            var sql = "ProductProcess_Save";
            var cmd = dbw.GetStoredProcCommand(sql);
            dbw.AddInParameter(cmd, "productid", DbType.Int32, info.ProductID);
            dbw.AddInParameter(cmd, "p1", DbType.Byte, info.Process1);
            dbw.AddInParameter(cmd, "p2", DbType.Byte, info.Process2);
            dbw.AddInParameter(cmd, "p3", DbType.Byte, info.Process3);
            dbw.AddInParameter(cmd, "p4", DbType.Byte, info.Process4);
            dbw.AddInParameter(cmd, "updater", DbType.String, info.Updater);
            var n = dbw.ExecuteNonQuery(cmd);
            return n > 0;
        }
    }
}