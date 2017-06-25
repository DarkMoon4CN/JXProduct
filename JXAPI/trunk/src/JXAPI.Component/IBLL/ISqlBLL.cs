using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.IBLL
{
    public interface ISqlBLL
    {
        DataTable GetAddList(int ID, string updateTime, int pageSize, string tableName);

        DataTable GetUpdateList(int MaxID, int NowID, string updateTime, int pageSize, string tableName);
    }
}
