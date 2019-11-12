using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ackee.Data.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int PCN { get; set; }

        public int StudentNumber { get; set; }

        //public IEnumerable<UserProjects> UserProjects { get; set; }
    }
}
