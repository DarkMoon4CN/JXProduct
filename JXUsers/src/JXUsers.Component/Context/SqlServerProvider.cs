using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCommon.Context
{
    public partial class SqlServerProvider : AppDbContext
    {
        public SqlServerProvider(string configKey="JXProductContext")
            : base(configKey)
        {
           Database.SetInitializer<SqlServerProvider>(null);
        }
    }
}
