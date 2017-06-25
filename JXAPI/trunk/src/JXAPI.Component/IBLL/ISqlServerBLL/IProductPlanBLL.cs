using JXAPI.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.IBLL.ISqlServerBLL
{
    public interface IProductPlanBLL
    {
        DataTable GetList(string tableName, out string errorMsg);

        OperationResult<bool> UpdateProduct(string tableName, string primaryKey, int primaryValue);
    }
}
