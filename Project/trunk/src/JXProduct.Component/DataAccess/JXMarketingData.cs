﻿using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JXProduct.Component.DataAccess
{
    public class JXMarketingData
    {
        public static Database Writer
        {
            get
            {
                return DatabaseFactory.CreateDatabase("JXMarketingWriter"); ;
            }
        }
        public static Database Reader
        {
            get
            {
                return DatabaseFactory.CreateDatabase("JXMarketingReader"); ;
            }
        }
    }
}