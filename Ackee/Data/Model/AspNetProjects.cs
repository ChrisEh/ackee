using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ackee.Data.Model
{
    public class AspNetProjects
    {
        [Key]
        public string ProjectID { get; set; }//PK

        [StringLength(128)]
        public string ProjectName { get; set; }

        [StringLength(256)]
        public string ProjectDescription { get; set; }

        [Required]
        public ApplicationUser Owner { get; set; }

        [Required]
        public List<AspNetMilestones> Milestone { get; set; }

        public IEnumerable<UserProject> UserProjects { get; set; }
    }
}
