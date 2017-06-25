using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.DataAccess
{
    public class JXYXData
    {
        private static ILog myLog = log4net.LogManager.GetLogger(typeof(JXYXData));
        
        public static Database Reader
        {
            get
            {
                try
                {
                    return DatabaseFactory.CreateDatabase("JXYXReader");
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("JXYXData 获取sql数据库Reader对象失败，异常信息:{0}", ex.Message);
                    return null;
                }
            }
        }
    }
}
