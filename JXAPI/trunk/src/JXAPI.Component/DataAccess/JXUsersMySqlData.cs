﻿using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXAPI.Component.DataAccess
{
    public class JXUsersMySqlData
    {
        private static ILog myLog = log4net.LogManager.GetLogger(typeof(JXUsersMySqlData));
        public static Database Writer
        {
            get
            {
                try
                {
                    return DatabaseFactory.CreateDatabase("MySqlUsersWrite");
                }
                catch(Exception ex)
                {
                    myLog.ErrorFormat("JXUsersMySqlData 获取mysql数据库Writer对象失败，异常信息:{0}", ex.Message);
                    return null;
                }
            }
        }
    }
}