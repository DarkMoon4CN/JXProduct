
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCommon.Context
{
    public partial class MySqlProvider : AppDbContext
    {
        public MySqlProvider(string configKey = "MySqlUsersContext")
            : base(configKey)
        {
           Database.SetInitializer<MySqlProvider>(null);
        }
    }
}
