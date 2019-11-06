using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMigrations
{
    [Table("Users")]
    public class Users
    {
        public int ID { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StudentNumber { get; set; }
        public int PCN { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastSeen { get; set; }
        public DateTime SessionExpiresAt { get; set; }
    }
}
