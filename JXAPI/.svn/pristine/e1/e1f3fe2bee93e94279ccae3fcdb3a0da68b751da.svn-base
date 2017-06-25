using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.DataAccess
{
    public class JXProductOthersData
    {
        private static ILog myLog = log4net.LogManager.GetLogger(typeof(JXProductOthersData));
        public static Database Writer
        {
            get
            {
                try
                {
                    return DatabaseFactory.CreateDatabase("JXProductOthersWriter");
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("JXProductOthersData 获取sql数据库Writer对象失败，异常信息:{0}", ex.Message);
                    return null;
                }

            }
        }
        public static Database Reader
        {
            get
            {
                try
                {
                    return DatabaseFactory.CreateDatabase("JXProductOthersReader");
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("JXProductOthersData 获取sql数据库Reader对象失败，异常信息:{0}", ex.Message);
                    return null;
                }
            }
        }
    }
}
