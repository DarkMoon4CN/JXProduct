using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.IBLL
{
    public interface IOrdersRelatedMySqlBLL
    {
        int GetMaxID();

        bool Update(DataTable table, out int errorCount);

        bool Add(DataTable table, out int errorCount);
    }
}
