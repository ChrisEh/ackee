﻿using System;
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
            // modelBuilder.Entity<UserProjects>().HasKey(up =>new { up.OwnerId, up.ProjectId });
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=tcp:ackeedbserver.database.windows.net,1433;Initial Catalog=Ackee_db;" +
                "User Id=bestAdmin@ackeedbserver;Password=AckeeWins1!");
        }
    }
}
