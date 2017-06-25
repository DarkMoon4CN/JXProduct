using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EFCommon.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(string configKey)
            : base(configKey)
        {
           
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //规则定义
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
