using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ackee.Data.Model
{
    public class AspNetProject
    {

        public string ProjectID { get; set; }//PK
        [StringLength(128)]

        public string ProjectName { get; set; }
        [StringLength(256)]

        public string ProjectTitle { get; set; }
        [StringLength(256)]

        public string Comments { get; set; }
        [StringLength(256)]

        public string UserRole { get; set; }
        [StringLength(128)]

        public string UserId { get; set; }


        [ForeignKey("UserId")]//FK
        [InverseProperty("AspNetProject")]
        public virtual AspNetProject User { get; set; }
        
    }
}
