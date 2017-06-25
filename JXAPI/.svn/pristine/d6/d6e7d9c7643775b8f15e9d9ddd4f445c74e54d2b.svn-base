using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXAPI.Component.DataAccess
{
    public class JXProductMySqlData
    {
        private static ILog myLog = log4net.LogManager.GetLogger(typeof(JXProductMySqlData));
        public static Database Writer
        {
            get
            {
                try
                {
                    return DatabaseFactory.CreateDatabase("MySqlProductWriter");
                }
                catch(Exception ex)
                {
                    myLog.ErrorFormat("JXProductMySqlData 获取mysql数据库Writer对象失败，异常信息:{0}", ex.Message);
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
                    return DatabaseFactory.CreateDatabase("MySqlProductReader");
                }
                catch(Exception ex)
                {
                    myLog.ErrorFormat("JXProductMySqlData 获取mysql数据库Reader对象失败，异常信息:{0}", ex.Message);
                    return null;
                }
                
            }
        }
    }
}