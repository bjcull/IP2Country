using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP2Country
{
    public class IpDbContext : DbContext
    {
        public DbSet<IpRecord> IpRecords { get; set; }

        public IpDbContext()
        {            
        }

        public IpDbContext(string connectionString)
            : base(connectionString)
        {            
        }
    }
}
