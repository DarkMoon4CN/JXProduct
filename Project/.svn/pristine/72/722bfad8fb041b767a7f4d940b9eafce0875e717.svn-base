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
    public class OperateLogDAL
    {
        private static Database dbr = JXMarketingData.Writer;
        private static Database dbw = JXMarketingData.Reader;

        public OperateLogInfo OperateLog_Insert(OperateLogInfo info)
        {
            var sql = "OperateLog_Insert";
            var cmd = dbw.GetStoredProcCommand(sql);

            dbw.AddInParameter(cmd, "UID", DbType.Int32, info.UID);
            //dbw.AddInParameter(cmd, "UserType", DbType.Int32, info.UserType);
            dbw.AddInParameter(cmd, "UserName", DbType.String, info.UserName);
            dbw.AddInParameter(cmd, "Title", DbType.String, info.Title);
            dbw.AddInParameter(cmd, "Content", DbType.String, info.Content);

            dbw.AddOutParameter(cmd, "LogID", DbType.Int32, 4);

            var result = dbw.ExecuteNonQuery(cmd);
            info.LogID = int.Parse(dbr.GetParameterValue(cmd, "LogID").ToString());

            return info;
        }
    }
}