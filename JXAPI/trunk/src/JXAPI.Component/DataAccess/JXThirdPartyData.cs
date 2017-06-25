using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.DataAccess
{
    public class JXThirdPartyData
    {
        private static ILog myLog = log4net.LogManager.GetLogger(typeof(JXThirdPartyData));
        public static Database Writer
        {
            get
            {
                try
                {
                    return DatabaseFactory.CreateDatabase("JXThirdPartyWriter");
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("JXThirdPartyData 获取mysql数据库Writer对象失败，异常信息:{0}", ex.Message);
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
                    return DatabaseFactory.CreateDatabase("JXThirdPartyReader");
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("JXThirdPartyData 获取mysql数据库Reader对象失败，异常信息:{0}", ex.Message);
                    return null;
                }
            }
        }

        public static Database JXUnionBaseDB
        {
            get
            {
                try
                {
                    return DatabaseFactory.CreateDatabase("JXUnionBaseDB");
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
