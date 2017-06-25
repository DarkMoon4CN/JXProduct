using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JXProduct.Component.DataAccess
{
    public class JXUsersMySqlData
    {
        public static Database Writer
        {
            get
            {
                return DatabaseFactory.CreateDatabase("JXUsersMySqlWriter"); ;
            }
        }
        public static Database Reader
        {
            get
            {
                return DatabaseFactory.CreateDatabase("JXUsersMySqlReader"); ;
            }
        }
    }
}
