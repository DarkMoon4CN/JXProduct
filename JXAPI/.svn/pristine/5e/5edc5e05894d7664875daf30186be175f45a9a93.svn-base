using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.DataAccess
{
    public class JXUsersBaseData
    {
        private static ILog myLog = log4net.LogManager.GetLogger(typeof(JXUsersBaseData));
        public static Database Reader
        {
            get
            {
                try
                {
                    return DatabaseFactory.CreateDatabase("JXUsersReader");
                }
                catch(Exception ex)
                {
                    myLog.ErrorFormat("JXUsersBaseData 获取mysql数据库Reader对象失败，异常信息:{0}", ex.Message);
                    return null;
                }
            }
        }
    }
}
