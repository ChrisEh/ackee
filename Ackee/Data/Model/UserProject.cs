using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ackee.Data.Model
{
    public class UserProject
    {
        public string ProjectId { get; set; }
        public AspNetProjects Project { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
