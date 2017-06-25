using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.DataAccess
{
    public class JXCouponBaseData
    {
        private static ILog myLog = log4net.LogManager.GetLogger(typeof(JXCouponBaseData));

        public static Database Reader
        {
            get
            {
                try
                {
                    return DatabaseFactory.CreateDatabase("JXCouponBaseReader");
                }
                catch (Exception ex)
                {
                    myLog.ErrorFormat("JXCouponBaseReader 获取mysql数据库Reader对象失败，异常信息:{0}", ex.Message);
                    return null;
                }
            }
        }
    }
}
