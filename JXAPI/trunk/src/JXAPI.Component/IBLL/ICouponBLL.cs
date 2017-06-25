using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.IBLL
{
    public interface ICouponBLL
    {
        DataTable GetAddList(int ID, string updateTime, int pageSize);

        DataTable GetUpdateList(int MaxID, int NowID, string updateTime, int pageSize);
    }
}
