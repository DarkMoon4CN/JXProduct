using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.IBLL
{
    public interface IOrdersRelatedBLL
    {
        DataTable GetAddList(int ID, string updateTime,int start, int pageSize, string tableName);

        DataTable GetUpdateList(int ID, string updateTime, int start, int pageSize, string tableName);
    }
}
