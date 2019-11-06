using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ackee.Models
{
    public class User
    {
        [Key]
        [MaxLength(50)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public int StudentNumber { get; set; }
        public DateTime? AccountCreatedAt { get; set; }
        public DateTime? SessionExpiresAt { get; set; }
        public DateTime? LastSeen { get; set; }
    }
}
