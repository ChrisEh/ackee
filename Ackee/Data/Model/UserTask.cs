using System;
using System.ComponentModel.DataAnnotations;

namespace Ackee.Data.Model
{
    public class UserTask
    {
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
        public string TaskID { get; set; }
        public AspNetTasks Task { get; set; }
    }
}
