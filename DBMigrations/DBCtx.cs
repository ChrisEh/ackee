using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMigrations
{
    public class DBCtx : DbContext
    {
        public DBCtx() : base("name=DBCtx")
        {
        }

        public virtual DbSet<Users> Users { get; set; }
    }
}
