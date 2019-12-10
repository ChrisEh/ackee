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
        public DbSet<AspNetProjects> Projects { get; set; }
        public DbSet<AspNetMilestones> Milestones { get; set; }           

        public AckeeCtx() : base()
        {
        }

        public AckeeCtx(DbContextOptions<AckeeCtx> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-many project/ApplicartionUser
            modelBuilder.Entity<UserProject>()
                .HasKey(u => new { u.UserId, u.ProjectId });

            modelBuilder.Entity<UserProject>()
            .HasOne(pt => pt.User)
            .WithMany(u => u.UserProjects)
            .HasForeignKey(pt => pt.UserId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserProject>()
            .HasOne(pt => pt.Project)
            .WithMany(t => t.UserProjects)
            .HasForeignKey(pt => pt.ProjectId).OnDelete(DeleteBehavior.Cascade);

            // One-to-many project-milestones
            modelBuilder.Entity<AspNetMilestones>()
                .HasOne(m => m.Project)
                .WithMany(p => p.Milestones);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=tcp:ackeedbserver.database.windows.net,1433;Initial Catalog=Ackee_db;" +
                "User Id=bestAdmin@ackeedbserver;Password=AckeeWins1!");
        }
    }
}
