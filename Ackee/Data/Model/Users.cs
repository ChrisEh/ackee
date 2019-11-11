﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AckeeDb.Data.Model
{
    public partial class Users
    {
        public Users()
        {
            AspNetUserClaims = new HashSet<UserClaims>();
            AspNetUserLogins = new HashSet<UserLogins>();
            AspNetUserRoles = new HashSet<UserRoles>();
            AspNetUserTokens = new HashSet<UserTokens>();
        }

        public string Id { get; set; }
        [StringLength(256)]
        public string UserName { get; set; }
        [StringLength(256)]
        public string NormalizedUserName { get; set; }
        [StringLength(256)]
        public string Email { get; set; }
        [StringLength(256)]
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserClaims> AspNetUserClaims { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserLogins> AspNetUserLogins { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserRoles> AspNetUserRoles { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserTokens> AspNetUserTokens { get; set; }
    }
}