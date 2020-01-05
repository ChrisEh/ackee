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
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<AspNetTasks> Tasks { get; set; }
        public DbSet<AspNetLabels> Labels { get; set; }
        public DbSet<MilestoneTask> MilestoneTasks { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<TaskLabel> TaskLabels { get; set; }

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

            // Many-to-many Task/Milestone
            modelBuilder.Entity<MilestoneTask>()
                .HasKey(m => new { m.MilestoneID, m.TaskID });

            modelBuilder.Entity<MilestoneTask>()
            .HasOne(mt => mt.Task)
            .WithMany(t => t.MilestoneTasks)
            .HasForeignKey(mt => mt.TaskID).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MilestoneTask>()
            .HasOne(mt => mt.Milestone)
            .WithMany(m => m.MilestoneTasks)
            .HasForeignKey(mt => mt.MilestoneID).OnDelete(DeleteBehavior.NoAction);

            // Many-to-many Task/User
            modelBuilder.Entity<UserTask>()
                .HasKey(u => new { u.UserID, u.TaskID });

            modelBuilder.Entity<UserTask>()
            .HasOne(ut => ut.Task)
            .WithMany(t => t.UserTasks)
            .HasForeignKey(ut => ut.TaskID).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserTask>()
            .HasOne(ut => ut.User)
            .WithMany(u => u.UserTasks)
            .HasForeignKey(ut => ut.UserID).OnDelete(DeleteBehavior.Cascade);

            // Many-to-many Task/Label
            modelBuilder.Entity<TaskLabel>()
                .HasKey(tl => new { tl.LabelID, tl.TaskID });

            modelBuilder.Entity<TaskLabel>()
            .HasOne(tl => tl.Task)
            .WithMany(t => t.TaskLabels)
            .HasForeignKey(tl => tl.TaskID).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskLabel>()
            .HasOne(tl => tl.Label)
            .WithMany(l => l.TaskLabels)
            .HasForeignKey(tl => tl.LabelID).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:ackeeservicedbserver.database.windows.net,1433;Initial Catalog=AckeeService2_db;Persist Security Info=False;User ID=alexlievense;Password=AckeeDBPass123!;");
        }
    }
}
