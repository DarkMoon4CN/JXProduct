using JXAPI.Component.DataAccess;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.Common;
using JXAPI.Component.Model;

namespace JXAPI.Component.SQLServerDAL
{
    public class ProductOutBookSqlDAL
    {
        private static Database dbw = JXProductOthersData.Writer;
        private ILog myLog = log4net.LogManager.GetLogger(typeof(OrderProductSqlDAL));

        /// <summary>
        /// 新增药师回拨
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int AddProductOutBook(ProductOutBookInfo info)
        {
            string sqlCommand = "ProductOutBook_Insert";
            DbCommand dbCommand = dbw.GetStoredProcCommand(sqlCommand);
            dbw.AddOutParameter(dbCommand, "OutID", DbType.Int32, 4);
            dbw.AddInParameter(dbCommand, "ProductID", DbType.Int32, info.ProductID);
            dbw.AddInParameter(dbCommand, "ProductName", DbType.String, info.ProductName);
            dbw.AddInParameter(dbCommand, "ProductCode", DbType.String, info.ProductCode);
            dbw.AddInParameter(dbCommand, "TradePrice", DbType.Decimal, info.TradePrice);
            dbw.AddInParameter(dbCommand, "Name", DbType.String, info.Name);
            dbw.AddInParameter(dbCommand, "UID", DbType.Int32, info.UID);
            dbw.AddInParameter(dbCommand, "Mobile", DbType.String, info.Mobile);
            dbw.AddInParameter(dbCommand, "Message", DbType.String, info.Message);
            dbw.AddInParameter(dbCommand, "Quantity", DbType.Int32, info.Quantity);
            dbw.AddInParameter(dbCommand, "CreateTime", DbType.DateTime, info.CreateTime);
            dbw.AddInParameter(dbCommand, "UpdateTime", DbType.DateTime, info.UpdateTime);
            dbw.AddInParameter(dbCommand, "Status", DbType.Int16, info.Status);
            dbw.AddInParameter(dbCommand, "HasBuy", DbType.Int16, info.HasBuy);
            dbw.ExecuteNonQuery(dbCommand);
            return int.Parse(dbw.GetParameterValue(dbCommand, "OutID").ToString());
        }
    }
}
