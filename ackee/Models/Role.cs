using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ackee.Models
{
    public enum RoleName
    {
        Administrator,
        Student,
        Teacher
    }

    public class Role
    {
        public int ID { get; set; }
        public RoleName RoleName {get; set;}
        [MaxLength(50)]
        public string Description { get; set; }
    }
}
