﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ackee.Data.Model
{
    public class AspNetMilestones
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string MilestoneID { get; set; }//PK

        [StringLength(128)]
        public string MilestoneName { get; set; }

        public DateTime EndDate { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public string ProjectId { get; set; }

        public AspNetProjects Project { get; set; }

        public bool Completed { get; set; }
    }
}
