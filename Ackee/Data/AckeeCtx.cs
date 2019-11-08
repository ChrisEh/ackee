using System;
using System.Collections.Generic;
using System.Text;
using AckeeDb.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ackee.Data
{
    public partial class AckeeCtx : IdentityDbContext
    {
        public AckeeCtx() : base()
        {
        }

        public AckeeCtx(DbContextOptions<AckeeCtx> options)
            : base(options)
        {
        }
        
        // TODO: Make fields for context work
        //public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        //public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        //public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        //public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        //public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        //public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        //public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //    }
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

        //    modelBuilder.Entity<AspNetRoleClaims>(entity =>
        //    {
        //        entity.HasIndex(e => e.RoleId);
        //    });

        //    modelBuilder.Entity<AspNetRoles>(entity =>
        //    {
        //        entity.HasIndex(e => e.NormalizedName)
        //            .HasName("RoleNameIndex")
        //            .IsUnique()
        //            .HasFilter("([NormalizedName] IS NOT NULL)");

        //        entity.Property(e => e.Id).ValueGeneratedNever();
        //    });

        //    modelBuilder.Entity<AspNetUserClaims>(entity =>
        //    {
        //        entity.HasIndex(e => e.UserId);
        //    });

        //    modelBuilder.Entity<AspNetUserLogins>(entity =>
        //    {
        //        entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

        //        entity.HasIndex(e => e.UserId);
        //    });

        //    modelBuilder.Entity<AspNetUserRoles>(entity =>
        //    {
        //        entity.HasKey(e => new { e.UserId, e.RoleId });

        //        entity.HasIndex(e => e.RoleId);
        //    });

        //    modelBuilder.Entity<AspNetUserTokens>(entity =>
        //    {
        //        entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
        //    });

        //    modelBuilder.Entity<AspNetUsers>(entity =>
        //    {
        //        entity.HasIndex(e => e.NormalizedEmail)
        //            .HasName("EmailIndex");

        //        entity.HasIndex(e => e.NormalizedUserName)
        //            .HasName("UserNameIndex")
        //            .IsUnique()
        //            .HasFilter("([NormalizedUserName] IS NOT NULL)");

        //        entity.Property(e => e.Id).ValueGeneratedNever();
        //    });
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
