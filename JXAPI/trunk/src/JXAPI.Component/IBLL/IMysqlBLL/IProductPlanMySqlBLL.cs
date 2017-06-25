using JXAPI.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.IBLL.IMysqlBLL
{
    public interface IProductPlanMySqlBLL
    {
        int GetMaxID(string tableName, string primaryId);

        OperationResult<bool> Add(string tableName, string[] colName, string[] sqlServerColName, DataRow dr);

        OperationResult<bool> Update(string tableName, string[] colName, string[] sqlServerColName, DataRow dr);

        OperationResult<bool> Delete(DataRow dr);
    }
}
