using System;
using System.Collections.Generic;
using System.Text;
using Ackee.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ackee.Data
{
    public partial class AckeeCtx : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        //public DbSet<Projects> Projects { get; set; }
        //public DbSet<UserProjects> UserProjects { get; set; }
        public DbSet<AspNetProjects> Project { get; set; }

        public AckeeCtx() : base()
        {
        }

        public AckeeCtx(DbContextOptions<AckeeCtx> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<UserProjects>().HasKey(up =>new { up.OwnerId, up.ProjectId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
