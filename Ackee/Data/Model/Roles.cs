using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AckeeDb.Data.Model
{
    public partial class Roles
    {
        public Roles()
        {
            AspNetRoleClaims = new HashSet<RoleClaims>();
            AspNetUserRoles = new HashSet<UserRoles>();
        }

        public string Id { get; set; }
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(256)]
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<RoleClaims> AspNetRoleClaims { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<UserRoles> AspNetUserRoles { get; set; }
    }
}