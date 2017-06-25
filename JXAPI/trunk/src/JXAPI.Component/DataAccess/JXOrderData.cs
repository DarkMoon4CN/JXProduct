using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.DataAccess
{
    public class JXOrderData
    {
        private static ILog myLog = log4net.LogManager.GetLogger(typeof(JXOrderData));
        
        public static Database Reader
        {
            get
            {
                try
                {
                    return DatabaseFactory.CreateDatabase("JXOrderReader");
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("JXOrderData 获取sql server数据库Reader对象失败，异常信息:{0}", ex.Message);
                    return null;
                }
            }
        }
    }
}
