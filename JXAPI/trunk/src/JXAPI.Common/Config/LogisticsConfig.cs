using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace JXAPI.Common.Config
{
    public class LogisticsConfig : ConfigurationSection
    {
        private static LogisticsConfig _Instance;
        public static LogisticsConfig Instance
        {
            get
            {
                if (null == _Instance)
                    _Instance = (ConfigurationSection)ConfigurationManager.GetSection("Logistics") as LogisticsConfig;
                return _Instance;
            }
        }

        [ConfigurationProperty("JDLogistics", IsRequired = false)]
        public JDLogisticsCollection JDLogistics
        {
            get
            {
                return this["JDLogistics"] as JDLogisticsCollection;
            }
        }

        [ConfigurationProperty("ZTOLogistics", IsRequired = false)]
        public ZTOLogisticsCollection ZTOLogistics
        {
            get
            {
                return this["ZTOLogistics"] as ZTOLogisticsCollection;
            }
        }
    }

    public class JDLogisticsCollection : ConfigurationElement
    {
        [ConfigurationProperty("url", IsRequired = true)]
        public string URL
        {
            get { return this["url"].ToString(); }
        }
    }

    public class ZTOLogisticsCollection : ConfigurationElement
    {
        [ConfigurationProperty("username", IsRequired = true)]
        public string UserName
        {
            get { return this["username"].ToString(); }
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get { return this["password"].ToString(); }
        }
    }
}
