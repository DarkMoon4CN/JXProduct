using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXBigData.Component.DataAccess
{
    public class ThirdPartyData
    {
        public static Database Writer
        {
            get
            {
                return DatabaseFactory.CreateDatabase("ThirdPartyReader"); ;
            }
        }
        public static Database Reader
        {
            get
            {
                return DatabaseFactory.CreateDatabase("ThirdPartyReader"); ;
            }
        }
    }
}
