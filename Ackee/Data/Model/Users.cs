using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ackee.Data.Model
{
    public partial class Users : AspNetUsers
    {
        public int PCN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
