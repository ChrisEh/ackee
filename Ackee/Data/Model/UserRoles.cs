using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AckeeDb.Data.Model
{
    public partial class UserRoles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        [ForeignKey("RoleId")]
        [InverseProperty("UserRoles")]
        public virtual Roles Role { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("UserRoles")]
        public virtual Users User { get; set; }
    }
}